﻿using System;
using Terminals.Converters;

namespace Terminals.Data.DB
{
    internal partial class DbCredentialSet : DbCredentialBase, ICredentialSet, IIntegerKeyEnityty
    {
        // for backward compatibility with the file persistence only
        private Guid guid;
        
        internal Guid Guid
        {
            get
            {
                if (this.guid == Guid.Empty)
                    this.guid = GuidConverter.ToGuid(this.Id);

                return this.guid;
            }
        }

        Guid ICredentialSet.Id
        {
            get { return this.Guid; }
        }

        internal DbCredentialSet Copy()
        {
            var copy = new DbCredentialSet
                {
                    Name = this.Name
                };
            CopyTo(copy);
            return copy;
        }

        public override string ToString()
        {
            return string.Format("CredentialSet:Name={0},Id={1}", this.Name, this.Guid);
        }
    }
}
