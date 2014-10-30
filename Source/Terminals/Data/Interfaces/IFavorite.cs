﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Terminals.Data
{
    /// <summary>
    /// Connection properties persisted for future reuse
    /// </summary>
    internal interface IFavorite : IStoreIdEquals<IFavorite>, INamedItem
    {
        /// <summary>
        /// Gets the unique identifier of this instance in associated store
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets not null name of an item. This is usually validated against persistence to case sensitive unique.
        /// Hides InamedItem because ob findings and sorting, we need it to be explicitly defined here
        /// </summary>
        new string Name { get; set; }

        /// <summary>
        /// Gets or sets the protocol type. Accepted value is one of ConnectionManager values.
        /// Set this value also updates the ProtocolProperties property to provide extra options.
        /// </summary>
        String Protocol { get; set; }

        /// <summary>
        /// Gets or sets positive number in range 0 - 65535
        /// of port on which server service defined by protocol is listening.
        /// </summary>
        Int32 Port { get; set; }

        /// <summary>
        /// Gets or sets the hostname, IP address or URL of the server.
        /// </summary>
        String ServerName { get; set; }

        /// <summary>
        /// Gets or sets the absolute path to the tool bar icon file, if custom icon was assigned.
        /// To directly access the icon image use <see cref="ToolBarIconImage"/>
        /// </summary>
        String ToolBarIconFile { get; set; }

        /// <summary>
        /// Gets the image loaded from assigned icon file.
        /// </summary>
        Image ToolBarIconImage { get; }

        /// <summary>
        /// Gets or sets the flag identifying, if the connection should be opened in new window or in TabControl.
        /// False by default.
        /// </summary>
        Boolean NewWindow { get; set; }

        String DesktopShare { get; set; }

        String Notes { get; set; }

        /// <summary>
        /// Gets collection of groups, where this favorite appears. 
        /// This is navigation property only and shouldn't be used for changes.
        /// </summary>
        List<IGroup> Groups { get; }

        /// <summary>
        /// Gets not null label, which lists all comma separated group names listed in Groups property.
        /// </summary>
        string GroupNames { get; }

        IDisplayOptions Display { get; }
        ISecurityOptions Security { get; }
        IBeforeConnectExecuteOptions ExecuteBeforeConnect { get; }

        /// <summary>
        /// Depending on selected protocol, this should contain the protocol detailed options.
        /// Because default protocol is RDP, also this properties are RdpOptions by default.
        /// This property should be always updated by changing Protocol property value
        /// </summary>
        ProtocolOptions ProtocolProperties { get; }

        /// <summary>
        /// Creates new deep copy of this instance. The only property which isn't copied is Id.
        /// </summary>
        /// <returns>Not null newly created copy of this instance</returns>
        IFavorite Copy();

        /// <summary>
        /// Updates this instance from source instance using deep copy. The only property which isn't updated is Id.
        /// </summary>
        /// <param name="source">Not null item, which properties should be use to update this instance</param>
        void UpdateFrom(IFavorite source);

        /// <summary>
        /// Gets label, which represents this instance detail information.
        /// </summary>
        string GetToolTipText();

        /// <summary>
        /// Returns text compare to method values selecting property to compare
        /// depending on Settings default sort property value
        /// </summary>
        /// <param name="target">not null favorite to compare with</param>
        /// <returns>result of String CompareTo method</returns>
        int CompareByDefaultSorting(IFavorite target);
    }
}
