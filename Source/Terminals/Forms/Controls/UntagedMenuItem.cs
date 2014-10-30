﻿using System;
using System.Collections.Generic;
using Terminals.Data;

namespace Terminals.Forms.Controls
{
    /// <summary>
    /// Virtual menu item used to collect favorites without assigned any group
    /// </summary>
    internal class UntagedMenuItem : GroupMenuItem
    {
        /// <summary>
        /// Gets the default tag name for favorites without any tag
        /// </summary>
        private const String UNTAGGED_NODENAME = "Not grouped";

        private readonly IPersistence persistence;

        internal override List<IFavorite> Favorites
        {
            get
            {
                IFavorites favorites = this.persistence.Favorites;
                return FavoriteTreeListLoader.GetUntaggedFavorites(favorites);
            }
        }

        internal UntagedMenuItem(IPersistence persistence)
            : base(UNTAGGED_NODENAME, true)
        {
            this.persistence = persistence;
        }
    }
}
