﻿using System.Collections.Generic;

namespace Terminals.Data
{
    /// <summary>
    /// Set of connection favorites used in similar meaning like directories in operation system.
    /// Allows logical organization of favorites.
    /// </summary>
    internal interface IGroup : IStoreIdEquals<IGroup>, INamedItem
    {
        /// <summary>
        /// Gets or sets the group in which this group is listed.
        /// By default null, which means, that it isn't listed anywhere. 
        /// and will appear as one of root folders in first level of favorites tree.
        /// </summary>
        IGroup Parent { get; set; }

        /// <summary>
        /// Gets or sets not null name of an item. This is usually validated against persistence to case sensitive unique.
        /// Hides InamedItem because ob findings and sorting, we need it to be explicitly defined here
        /// </summary>
        new string Name { get; set; }

        /// <summary>
        /// Gets list of connection favorites listed in this group. 
        /// To change this collection use AddFavorite or RemoveFavorite methods.
        /// </summary>
        List<IFavorite> Favorites { get; }

        /// <summary>
        /// Adds required favorite into this set. It would be added only, 
        /// if there is no favorite with the same id yet.
        /// </summary>
        /// <param name="favorite">Not null already persisted instance to add.</param>
        void AddFavorite(IFavorite favorite);

        /// <summary>
        /// Adds all required favorites into this set. It would be added only, 
        /// if there is no favorite with the same id yet.
        /// </summary>
        /// <param name="favorites">Not null already persisted instance to add.</param>
        void AddFavorites(List<IFavorite> favorites);

        /// <summary>
        /// Removes required favorite from set of favorites. If there is no favorite with the same Id,
        /// than nothing happens.
        /// </summary>
        /// <param name="favorite">Not null instance to remove. This instance doesn't have to be persisted</param>
        void RemoveFavorite(IFavorite favorite);

        /// <summary>
        /// Removes all required favorites from set of favorites. If there is no favorite with the same Id,
        /// than nothing happens.
        /// </summary>
        /// <param name="favorites">Not null instance to remove. This instance doesnt have to be peristed</param>
        void RemoveFavorites(List<IFavorite> favorites);
    }
}
