﻿using System;
using Terminals.History;

namespace Terminals.Data
{
    /// <summary>
    /// Represents collection of visited favorite items and their time stamps.
    /// </summary>
    internal interface IConnectionHistory
    {
        /// <summary>
        /// Reports visited favorite immediately, when connection 
        /// is opened and its time stamp is added to the history.
        /// </summary>
        event HistoryRecorded HistoryRecorded;

        /// <summary>
        /// Reports that all entries from history were released.
        /// </summary>
        event Action HistoryClear;

        /// <summary>
        /// Gets not null distinct collection of favorites, which were visited in selected time interval
        /// represented by historyDateKey. Keys can be found in ConnectionHistory as constants.
        /// </summary>
        /// <param name="historyDateKey">One of time interval constants</param>
        SortableList<IFavorite> GetDateItems(string historyDateKey);

        /// <summary>
        /// Adds new time stamp to the favorite history collection and fires OnHistoryRecorded.
        /// And saves the history to persistence.
        /// </summary>
        /// <param name="favorite">favorite to remember</param>
        void RecordHistoryItem(IFavorite favorite);

        /// <summary>
        /// Removes all entries for all favorites from connection history.
        /// </summary>
        void Clear();
    }
}