﻿using Versioning = SqlScriptRunner.Versioning;

namespace Terminals.Data.DB
{
    /// <summary>
    /// Ensured connection, database master password and database content version
    /// </summary>
    internal class DatabaseValidationResult : TestConnectionResult
    {
        internal Versioning.Version CurrentVersion { get; set; }

        internal bool SuccessfulWithVersion
        {
            get
            {
               return this.Successful && !this.IsMinimalVersion; 
            }
        }

        internal bool IsMinimalVersion
        {
            get { return this.CurrentVersion == Versioning.Version.Min; }
        }

        internal DatabaseValidationResult(Versioning.Version currentVersion)
        {
            this.CurrentVersion = currentVersion;
        }

        internal DatabaseValidationResult(string errorMesasge)
            : base(errorMesasge)
        {
            this.CurrentVersion = Versioning.Version.Min;
        }

        internal DatabaseValidationResult(TestConnectionResult connectionResult, Versioning.Version version)
            : base(connectionResult)
        {
            CurrentVersion = version;
        }

        public override string ToString()
        {
            return string.Format("DatabaseValidataionResult:Successful={0},CurrentVersion={1},ErrorMessage={2}",
                this.Successful, this.CurrentVersion, this.ErroMessage);
        }
    }
}