using System;
using System.Collections.Generic;

namespace Terminals.Data
{
    /// <summary>
    /// Provides collection of stored credential sets used to logon when opening new connection.
    /// </summary>
    internal interface ICredentials : IEnumerable<ICredentialSet>
    {
        /// <summary>
        /// Informs all listeners, that credentials collection was changed.
        /// </summary>
        event EventHandler CredentialsChanged;

        /// <summary>
        /// Gets a credential by its unique identifier from cached credentials.
        /// If no item matches, returns null.
        /// </summary>
        /// <param name="id">unique identifier of an item to search</param>
        ICredentialSet this[Guid id] { get; }

        /// <summary>
        /// Gets a credential by its name from cached credentials.
        /// This method isnt case sensitive. If no item matches, returns null.
        /// This is for backward compatibility. Use indexer by unique identifier instead.
        /// </summary>
        /// <param name="name">name of an item to search</param>
        ICredentialSet this[string name] { get; }

        /// <summary>
        /// Adds required item to stored collection
        /// </summary>
        /// <param name="toAdd">Not null item to add</param>
        void Add(ICredentialSet toAdd);

        /// <summary>
        /// Removes required item from credentials collection.
        /// </summary>
        /// <param name="toRemove">Not null item, which will be removed</param>
        void Remove(ICredentialSet toRemove);

        /// <summary>
        /// Updates required item in stored credentials collection.
        /// </summary>
        /// <param name="toUpdate">Not null item, which will be updated</param>
        void Update(ICredentialSet toUpdate);

        /// <summary>
        /// Updates password hashes for currently stored credentials updating their value
        /// by key material created from masterpassword hash.
        /// </summary>
        /// <param name="newKeyMaterial">new key used to updated password hashes</param>
        void UpdatePasswordsByNewKeyMaterial(string newKeyMaterial);
    }
}