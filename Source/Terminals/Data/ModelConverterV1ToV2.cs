﻿namespace Terminals.Data
{
    /// <summary>
    /// Converts favorites from data model used in version 1.X (FavoriteConfigurationElement)
    /// to the model used in version 2.0 (Favorite).
    /// Temporary used also to support imports and export using old data model, 
    /// before they will be updated.
    /// </summary>
    internal class ModelConverterV1ToV2
    {
        private readonly IPersistence persistence;

        private ModelConverterV1ToV2(IPersistence persistence)
        {
            this.persistence = persistence;
        }

        /// <summary>
        /// Doesn't convert Tags to groups, it has to be handled manually, 
        /// when adding Favorite into Persistence
        /// </summary>
        internal static IFavorite ConvertToFavorite(FavoriteConfigurationElement sourceFavorite, IPersistence persistence)
        {
            var converter = new ModelConverterV1ToV2(persistence);
            return converter.Convert(sourceFavorite);
        }

        private IFavorite Convert(FavoriteConfigurationElement sourceFavorite)
        {
            IFavorite result = persistence.Factory.CreateFavorite();
            ConvertGeneralProperties(result, sourceFavorite);
            ConvertSecurity(result, sourceFavorite);
            ConvertBeforeConnetExecute(result, sourceFavorite);
            ConvertDisplay(result, sourceFavorite);
            result.ProtocolProperties.FromCofigFavorite(result, sourceFavorite);

            return result;
        }

        private static void ConvertGeneralProperties(IFavorite result, FavoriteConfigurationElement sourceFavorite)
        {
            result.Protocol = sourceFavorite.Protocol;
            result.Name = sourceFavorite.Name;
            result.Port = sourceFavorite.Port;
            result.ServerName = sourceFavorite.ServerName;
            result.ToolBarIconFile = sourceFavorite.ToolBarIcon;
            result.NewWindow = sourceFavorite.NewWindow;
            result.DesktopShare = sourceFavorite.DesktopShare;
            result.Notes = sourceFavorite.Notes;
        }

        private void ConvertSecurity(IFavorite result, FavoriteConfigurationElement sourceFavorite)
        {
            ISecurityOptions security = result.Security;
            // during upgrade don't use directly build-in property (without "Plain prefix"),
            // because it access the Persistence.Instance.
            // Because it is used also by upgrade from old version, persistence doesn't have to be initialized yet.
            security.Domain = sourceFavorite.PlainDomainName;
            security.UserName = sourceFavorite.PlainUserName;
            // because persistence and application masterpassword may differ,
            // we have to go through encryption without credential resolution
            security.Password = persistence.Security.DecryptPassword(sourceFavorite.EncryptedPassword);
            
            ICredentialSet credential = persistence.Credentials[sourceFavorite.Credential];
            if (credential != null)
                security.Credential = credential.Id;
        }

        private static void ConvertBeforeConnetExecute(IFavorite result, FavoriteConfigurationElement sourceFavorite)
        {
            IBeforeConnectExecuteOptions executeOptions = result.ExecuteBeforeConnect;
            executeOptions.Execute = sourceFavorite.ExecuteBeforeConnect;
            executeOptions.Command = sourceFavorite.ExecuteBeforeConnectCommand;
            executeOptions.CommandArguments = sourceFavorite.ExecuteBeforeConnectArgs;
            executeOptions.InitialDirectory = sourceFavorite.ExecuteBeforeConnectInitialDirectory;
            executeOptions.WaitForExit = sourceFavorite.ExecuteBeforeConnectWaitForExit;
        }

        private static void ConvertDisplay(IFavorite result, FavoriteConfigurationElement sourceFavorite)
        {
            IDisplayOptions display = result.Display;
            display.Colors = sourceFavorite.Colors;
            display.DesktopSize = sourceFavorite.DesktopSize;
            display.Width = sourceFavorite.DesktopSizeWidth;
            display.Height = sourceFavorite.DesktopSizeHeight;
        }
    }
}
