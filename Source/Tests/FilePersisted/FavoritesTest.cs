﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Terminals.Connections;
using Terminals.Data;

namespace Tests.FilePersisted
{
    [TestClass]
    public class FavoritesTest : FilePersistedTestLab
    {
        private Guid addedFavoriteId;
        private Guid updatedFavoriteId;
        private Guid deletedFavoriteId;
        private Guid addedGroupId;
        private Guid updatedGroupId;
        private Guid deletedGroupId;

        /// <summary>
        /// Notes property can store special characters, so it is Base64 encoded.
        /// </summary>
        [TestCategory("NonSql")]
        [TestMethod]
        public void SaveLoadNotesTest()
        {
            IFavorite favorite = this.AddFavorite();
            const string SPECIAL_CHARACTERS = "čočka\r\nčočka"; // some example special characters
            favorite.Notes = SPECIAL_CHARACTERS;
            this.Persistence.Favorites.Update(favorite);
            var secondary = new FilePersistence();
            secondary.Initialize();
            IFavorite checkfavorite = secondary.Favorites.FirstOrDefault();
            Assert.AreEqual(SPECIAL_CHARACTERS, checkfavorite.Notes, "favorite notes were not saved properly");
        }

        /// <summary>
        /// Checks, if we are still able to manipulate passwords after protocol update.
        /// This is a special case for RdpOptions, which need persistence to handle Gateway credentials
        /// </summary>
        [TestCategory("NonSql")]
        [TestMethod]
        public void UpdateProtocolTest()
        {
            IFavorite favorite = this.AddFavorite();
            // now it has RdpOptions
            favorite.Protocol = ConnectionManager.VNC;
            this.Persistence.Favorites.Update(favorite);
            AssertRdpSecurity(favorite);
        }

        /// <summary>
        /// Checks, if Gateway has still assigned persistence security, to be able work with passwords.
        /// </summary>
        internal static void AssertRdpSecurity(IFavorite favorite)
        {
            favorite.Protocol = ConnectionManager.RDP;
            var rdpOptions = favorite.ProtocolProperties as RdpOptions;
            // next line shouldn't fail
            rdpOptions.TsGateway.Security.Password = "aaa";
        }

        /// <summary>
        /// Focus on both favorites changes (add, remove, delete) and group memberschip changes.
        /// </summary>
        [TestCategory("NonSql")]
        [TestMethod]
        public void FavoritesAndGroupsReloadTest()
        {
            IFavorite favToRemove = this.AddFavorite();
            IGroup groupToRemove = this.AddNewGroup("GroupToRemove");
            // not connected each other
            Tuple<IFavorite, IGroup> toRemove = new Tuple<IFavorite, IGroup>(favToRemove, groupToRemove);
            Tuple<IFavorite, IGroup> toUpdate = this.AddFavoriteWithGroup("GroupToUpdate");
            
            var testFileWatch = new TestFileWatch();
            var secondaryPersistence = this.InitializeSecondaryPersistence(testFileWatch);

            //do the next steps ot be able serialize otherwise paraller work of persistences refresh and dont relay on OS file changes
            //1. kick off the file changed event in background thread to let the secondary persistence reload in parallel
            ThreadPool.QueueUserWorkItem(new WaitCallback((state) => testFileWatch.FireFileChanged()));

            //2. do the changes in primary persistence, this is not noticed in secondary persistence yet
            Tuple<IFavorite, IGroup> added = this.UpdatePrimaryPersistenceFavorites(toUpdate, toRemove);

            testFileWatch.ObservationWatch.Set(); // signal secondary persistence, that changes are ready to reload

            //3. wait till secondary persistence is done with reload 
            testFileWatch.ReleaseWatch.WaitOne(6000);

            //4. assert the results in secondary persistence
            int secondaryFavoritesCount = secondaryPersistence.Favorites.Count();
            Assert.AreEqual(2, secondaryFavoritesCount); // one updated and one added
            Assert.AreEqual(this.addedFavoriteId, added.Item1.Id, "Didnt receive favorite add event");
            Assert.AreEqual(this.updatedFavoriteId, toUpdate.Item1.Id, "Didnt receive favorite update event");
            Assert.AreEqual(this.deletedFavoriteId, toRemove.Item1.Id, "Didnt receive favorite removed event");
            int secondaryGroupsCount = secondaryPersistence.Groups.Count();
            Assert.AreEqual(2, secondaryGroupsCount); // one updated and one added
            Assert.AreEqual(this.addedGroupId, ((Group)added.Item2).Id, "Didnt receive group added event");
            Assert.AreEqual(this.deletedGroupId, ((Group)toRemove.Item2).Id, "Didnt receive group removed event");
            Assert.AreEqual(this.updatedGroupId, ((Group)toUpdate.Item2).Id, "Didnt receive group updated event");
        }

        private FilePersistence InitializeSecondaryPersistence(TestFileWatch testFileWatch)
        {
            var secondaryPersistence = new FilePersistence(new PersistenceSecurity(), testFileWatch);
            // let the persistence load initial state
            secondaryPersistence.Initialize();
            secondaryPersistence.Dispatcher.FavoritesChanged += this.DispatcherOnFavoritesChanged;
            secondaryPersistence.Dispatcher.GroupsChanged += this.DispatcherOnGroupsChanged;
            return secondaryPersistence;
        }

        private void DispatcherOnFavoritesChanged(FavoritesChangedEventArgs args)
        {
            if (args.Added.Count == 1)
                this.addedFavoriteId = args.Added[0].Id;
            if (args.Removed.Count == 1)
                this.deletedFavoriteId = args.Removed[0].Id;
            if (args.Updated.Count == 1)
                this.updatedFavoriteId = args.Updated[0].Id;
        }

        private void DispatcherOnGroupsChanged(GroupsChangedArgs args)
        {
            if (args.Added.Count == 1)
                this.addedGroupId = ((Group)args.Added[0]).Id;
            if (args.Removed.Count == 1)
                this.deletedGroupId = ((Group)args.Removed[0]).Id;
            if (args.Updated.Count == 1)
                this.updatedGroupId = ((Group)args.Updated[0]).Id;
        }

        private Tuple<IFavorite, IGroup> UpdatePrimaryPersistenceFavorites(Tuple<IFavorite, IGroup> toUpdate,
            Tuple<IFavorite, IGroup> toRemove)
        {
            IFavorites primaryFavorites = this.Persistence.Favorites;
            var favoriteToUpdate = toUpdate.Item1;
            favoriteToUpdate.Name = "some other value";
            // simulates group update by removing its favorite
            primaryFavorites.UpdateFavorite(favoriteToUpdate, new List<IGroup>());
            primaryFavorites.Delete(toRemove.Item1);
            this.Persistence.Groups.Delete(toRemove.Item2);
            Tuple<IFavorite, IGroup> added = this.AddFavoriteWithGroup("GroupToAdd");
            return added;
        }
    }
}
