﻿using System;

namespace Terminals.Converters
{
    /// <summary>
    /// Converts integer to Guid. The result isn't globally unique,
    /// but helps the DB store to unify global identifiers with the file persistence.
    /// </summary>
    internal static class GuidConverter
    {
        /// <summary>
        /// Fills left bytes of the Guid with bytes from source integer.
        /// </summary>
        /// <param name="value">Source isn't to fill part of the newly created Guid.</param>
        /// <returns>New not globally unique identifier, which holds content of source integer bytes</returns>
        internal static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
