﻿using System;
using System.Diagnostics;
using System.Net;
using Terminals.Connections;

namespace Terminals.Data
{
    internal static class FavoritesFactory
    {
        private const string DISCOVERED_CONNECTIONS = "Discovered Connections";
        private static readonly String terminalsReleasesFavoriteName = Program.Resources.GetString("TerminalsNews");
        private static readonly String terminalsReleasesUrl = Program.Resources.GetString("TerminalsURL");

        internal static string TerminalsReleasesFavoriteName
        {
            get { return terminalsReleasesFavoriteName; }
        }

        internal static string TerminalsReleasesUrl
        {
            get { return terminalsReleasesUrl; }
        }

        private static IFavorites PersistedFavorites
        {
            get { return Persistence.Instance.Favorites; }
        }

        private static IFactory PersistenceFactory
        {
            get { return Persistence.Instance.Factory; }
        }

        internal static FavoriteConfigurationElement CreateNewFavorite(string favoriteName, string server, int port,
            string domain, string userName)
        {
            FavoriteConfigurationElement newFavorite = new FavoriteConfigurationElement();
            newFavorite.Name = favoriteName;
            newFavorite.ServerName = server;
            newFavorite.UserName = userName;
            newFavorite.DomainName = domain;
            newFavorite.Tags = DISCOVERED_CONNECTIONS;
            newFavorite.Port = port;
            newFavorite.Protocol = ConnectionManager.GetPortName(port, true);
            return newFavorite;
        }

        internal static FavoriteConfigurationElement CreateNewFavorite(string favoriteName, string server, int port)
        {
            string name = GetHostName(server, favoriteName, port);
            string domainName = GetCurrentDomainName(server);
            return CreateNewFavorite(name, server, port, domainName, Environment.UserName);
        }

        private static string GetCurrentDomainName(string server)
        {
            if (Environment.UserDomainName != Environment.MachineName)
                return Environment.UserDomainName;

            return server;
        }

        private static string GetHostName(string server, string name, int port)
        {
            try
            {
                IPAddress address;
                if (IPAddress.TryParse(server, out address))
                    name = Dns.GetHostEntry(address).HostName;

                string portName = ConnectionManager.GetPortName(port, true);
                return string.Format("{0}_{1}", name, portName);
            }
            catch // don't log dns lookups!
            {
                Debug.WriteLine("Unable to resolve '{0}' host name.", server);
                return name;
            }
        }


        /// <summary>
        /// Gets persisted favorite, if there is a favorite named by server parameter.
        /// If no favorite is found creates new favorite, which is configured by parameter properties
        /// and point to RDP server.
        /// </summary>
        /// <param name="server">the RDP server name</param>
        /// <param name="connectToConsole">Flag used for ConnectToConsole RDP option</param>
        /// <param name="port">Number of port, which RDP service is listening on server "server"</param>
        internal static IFavorite GetOrCreateQuickConnectFavorite(String server,
            Boolean connectToConsole, Int32 port)
        {
            IFavorite favorite = PersistedFavorites[server];
            if (favorite == null) //create a temporary favorite and connect to it
            {
                favorite = PersistenceFactory.CreateFavorite();
                favorite.ServerName = server;
                favorite.Name = server;

                if (port != 0)
                    favorite.Port = port;
            }

            var rdpProperties = favorite.ProtocolProperties as RdpOptions;
            if (rdpProperties != null)
                rdpProperties.ConnectToConsole = connectToConsole;

            return favorite;
        }

        /// <summary>
        /// Gets newly created connection favorite, with name of Terminals release constant.
        /// </summary>
        /// <returns>Not null, configured instance of connection favorite,
        /// which points to the terminals web site</returns>
        internal static IFavorite CreateReleaseFavorite()
        {
            IFavorite release = PersistenceFactory.CreateFavorite();
            release.Name = TerminalsReleasesFavoriteName;
            release.ServerName = TerminalsReleasesUrl;
            release.Protocol = ConnectionManager.HTTP;
            release.Port = ConnectionManager.HTTPPort;
            return release;
        }

        /// <summary>
        /// Gets group with required groupName or creates new group which is immediately added to the persistence.
        /// </summary>
        /// <param name="persistence">Not null persistence, where to search for groups</param>
        /// <param name="groupName">Name of the group to search in persistence.</param>
        /// <returns>Not null value of Group obtained from persistence or newly created group</returns>
        internal static IGroup GetOrAddNewGroup(IPersistence persistence, string groupName)
        {
            IGroups groups = persistence.Groups;
            IGroup group = groups[groupName];
            if (group == null)
            {
                group = persistence.Factory.CreateGroup(groupName);
                groups.Add(group); 
            }

            return group;
        }
    }
}
