﻿using System;

namespace SqlScriptRunner.Versioning
{
    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public static Versioning.Version Max
        {
            get
            {
                return new Version() { Major = int.MaxValue, Minor = int.MinValue, Description = "Max Version" };
            }
        }

        public static Versioning.Version Min
        {
            get
            {
                return new Version() { Major = int.MinValue, Minor = int.MinValue, Description = "Min Version" };
            }
        }

        public static bool operator ==(Version c1, Version c2)
        {
            return (c1.Major == c2.Major && c1.Minor == c2.Minor && c1.Build == c2.Build);
        }

        public static bool operator !=(Version c1, Version c2)
        {
            return !(c1 == c2);
        }

        public static bool operator >(Version c1, Version c2)
        {
            if (c1.Major < c2.Major) return false;
            if (c1.Major > c2.Major) return true;

            if (c1.Minor < c2.Minor) return false;
            if (c1.Minor > c2.Minor) return true;

            if (c1.Build < c2.Build) return false;
            if (c1.Build > c2.Build) return true;
            return false;
        }
        public static bool operator <(Version c1, Version c2)
        {
            if (c1.Major > c2.Major) return false;
            if (c1.Major < c2.Major) return true;

            if (c1.Minor > c2.Minor) return false;
            if (c1.Minor < c2.Minor) return true;

            if (c1.Build > c2.Build) return false;
            if (c1.Build < c2.Build) return true;
            return false;
        }

        public static bool operator >=(Version c1, Version c2)
        {
            return (c1 == c2 || c1 > c2);
        }
        public static bool operator <=(Version c1, Version c2)
        {
            return (c1 == c2 || c1 < c2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var that = obj as Version;

            if (ReferenceEquals(that, null))
                return false;

            return that == this;
        }

        public override int GetHashCode()
        {
            return (Major * 37 + Minor) * 37 + Build;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Build);
        }
    }
}