﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Terminals.Data;
using Terminals.Integration;

namespace Terminals.Forms.Controls
{
    internal class TreeViewDragDrop
    {
        /// <summary>
        /// Move for groups, Link for Favorites, Copy for FileDrop or Favorites
        /// </summary>
        internal const DragDropEffects SUPPORTED_DROPS = DragDropEffects.All | DragDropEffects.Link;

        private readonly IDataObject data;

        private readonly IPersistence persistence;

        internal CopyFavoriteCommand CopyCommnad { get; set; }

        private readonly IKeyModifiers keyModifiers;

        private readonly IGroup targetGroup;

        private readonly IFavorite targetFavorite;

        private IFavorite SourceFavorite
        {
            get
            {
                var favoriteNode = this.data.GetData(typeof(FavoriteTreeNode)) as FavoriteTreeNode;
                if (favoriteNode != null)
                    return favoriteNode.Favorite;

                return null;
            }
        }

        private IGroup SourceGroup
        {
            get
            {
                var sourceNode = this.data.GetData(typeof(GroupTreeNode)) as GroupTreeNode;
                if (sourceNode != null)
                    return sourceNode.Group;

                return null;
            }
        }

        private bool DontDropFavorite
        {
            get
            {
                return this.SourceFavorite == null || this.targetGroup == null;
            }
        }

        /// <summary>
        /// Gets drop effect to be performed for current situation, not the one currently set by arguments provided in constructor.
        /// </summary>
        internal DragDropEffects Effect { get; private set; }

        private Action<Form> tryDrop= form => { };

        internal TreeViewDragDrop(IPersistence persistence, DragEventArgs dragArguments,
            IKeyModifiers keyModifiers, IGroup targetGroup, IFavorite targetFavorite)
        {
            this.Effect = DragDropEffects.None;
            this.data = dragArguments.Data;
            this.persistence = persistence;
            this.CopyCommnad = new CopyFavoriteCommand(this.persistence);
            this.keyModifiers = keyModifiers;
            this.targetGroup = targetGroup;
            this.targetFavorite = targetFavorite;
            this.Configure(dragArguments.Effect);
        }

        private void Configure(DragDropEffects effect)
        {
            if (this.data.GetDataPresent(typeof(FavoriteTreeNode)))
                this.ConfigureFavoriteDrop(effect);

            if (this.data.GetDataPresent(typeof(GroupTreeNode)))
                this.ConfigureGroupDrop();

            if (this.data.GetDataPresent(DataFormats.FileDrop, false))
                this.ConfigureFilesDrop();
        }

        private void ConfigureFavoriteDrop(DragDropEffects dropEffect)
        {
            // not by selected favorite, because it can be already changed by mouse over another tree nodes
            this.Effect = this.FavoriteDragDropType();
            this.tryDrop = this.FavoriteDropAction(dropEffect);
        }

        private Action<Form> FavoriteDropAction(DragDropEffects dropEffect)
        {
            switch (dropEffect)
            {
                case DragDropEffects.Copy:
                    return form => this.CopyFavorite();
                case DragDropEffects.Link:
                    return form => this.AddFavoriteToGroup();
                default:
                    return form => this.MoveFavorite();
            }
        }

        private DragDropEffects FavoriteDragDropType()
        {
            // even, if target group is null, it means move to root
            if (this.targetFavorite != null)
                return DragDropEffects.None;

            return FavoriteDropTypeByModifier();
        }

        private DragDropEffects FavoriteDropTypeByModifier()
        {
            if (this.keyModifiers.WithControl)
                return DragDropEffects.Copy;

            if (this.keyModifiers.WithShift)
                return DragDropEffects.Link;

            return DragDropEffects.Move;
        }

        private void CopyFavorite()
        {
            if (this.DontDropFavorite)
                return;

            IFavorite copy = this.CopyCommnad.Copy(this.SourceFavorite);
            if (copy != null)
                this.persistence.Favorites.UpdateFavorite(copy, new List<IGroup>() { this.targetGroup });
        }

        private void MoveFavorite()
        {
            if (this.SourceFavorite == null) return;

            List<IGroup> targetGroups = new List<IGroup>();
            // target group can be null => move to root
            if (this.targetGroup != null)
                targetGroups.Add(this.targetGroup);

            this.persistence.Favorites.UpdateFavorite(this.SourceFavorite, targetGroups);
        }

        private void AddFavoriteToGroup()
        {
            if (this.DontDropFavorite)
                return;

            IFavorite toUpdate = this.SourceFavorite;
            List<IGroup> resultGroups = toUpdate.Groups.ToList();
            resultGroups.Add(this.targetGroup);
            this.persistence.Favorites.UpdateFavorite(toUpdate, resultGroups);
        }

        private void ConfigureGroupDrop()
        {
            this.Effect = this.GroupDropType();
            this.tryDrop = form => this.DropGroup();
        } 
        
        private DragDropEffects GroupDropType()
        {
            // target cant be favorite, but can be empty => move to root
            if (!this.GroupDropOnItSelf(this.SourceGroup) && this.targetFavorite == null)
                return DragDropEffects.Move;

            return DragDropEffects.None;
        }

        private void DropGroup()
        {
            // resolve source group only once
            IGroup sourceGroup = this.SourceGroup;
            // dont check target group, becaue if empty, than we mare moving to the root
            if (sourceGroup == null || this.GroupDropOnItSelf(sourceGroup))
                return;

            sourceGroup.Parent = this.targetGroup;
            this.persistence.Groups.Update(sourceGroup);
        }

        private bool GroupDropOnItSelf(IGroup sourceGroup)
        {
            if (sourceGroup != null && this.targetGroup != null)
                return sourceGroup.StoreIdEquals(this.targetGroup);

            return false;
        }

        private void ConfigureFilesDrop()
        {
            // draged files to import from another application
            this.Effect = DragDropEffects.All;
            this.tryDrop = this.DropFiles;
        }

        private void DropFiles(Form parentForm)
        {
            var files = this.data.GetData(DataFormats.FileDrop) as String[];
            if (files == null)
                return;

            var importers = Integrations.CreateImporters(this.persistence);
            List<FavoriteConfigurationElement> toImport = importers.ImportFavorites(files);
            this.ApplyTargetGroup(toImport);
            var managedImport = new ImportWithDialogs(parentForm, this.persistence);
            managedImport.Import(toImport);
        }

        private void ApplyTargetGroup(List<FavoriteConfigurationElement> favoritesToImport)
        {
            if (this.targetGroup == null)
                return;

            foreach (FavoriteConfigurationElement toImport in favoritesToImport)
            {
                toImport.Tags = this.targetGroup.Name;
            }
        }

        internal void Drop(Form parentForm)
        {
            this.tryDrop(parentForm);
        }
    }
}