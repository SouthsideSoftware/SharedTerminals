﻿using System;
using System.Xml.Serialization;

namespace Terminals.Data
{
    /// <summary>
    /// Container of stored user authentication.
    /// </summary>
    [Serializable]
    public class CredentialSet : CredentialBase, ICredentialSet
    {
        private Guid id = Guid.NewGuid();
        [XmlAttribute("id")]
        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    return;

                this.name = value;
            }
        }

        // next two properties are only to simplify presentation in grids using SortableList
        // otherwise sorting fails, see ICredentialSet
        string ICredentialSet.UserName
        {
            get { return this.UserName; }
            set { this.UserName = value; }
        }

        string ICredentialSet.Domain
        {
            get { return this.Domain; }
            set { this.Domain = value; }
        }

        public override string ToString()
        {
            return String.Format(@"{0}:{1}\{2}", this.Name, "", "");
        }
    }
}
