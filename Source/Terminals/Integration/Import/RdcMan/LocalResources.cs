using System;
using System.Xml.Linq;

namespace Terminals.Integration.Import.RdcMan
{
    internal class LocalResources : Settings<LocalResources>
    {
        internal int AudioRedirect
        {
            get
            {
                Func<int> getParentValue = () => this.Parent.AudioRedirect;
                Func<int> getElementValue = this.PropertiesElement.GetAudioRedirection;
                return this.ResolveValue(getParentValue, getElementValue);
            }
        }

        internal int KeyboardHook
        {
            get
            {
                Func<int> getParentValue = () => this.Parent.KeyboardHook;
                Func<int> getElementValue = this.PropertiesElement.GetKeyboardHook;
                return this.ResolveValue(getParentValue, getElementValue, 2);
            }
        }

        internal bool RedirectClipboard
        {
            get
            {
                Func<bool> getParentValue = () => this.Parent.RedirectClipboard;
                Func<bool> getElementValue = this.PropertiesElement.GetRedirectClipboard;
                return this.ResolveValue(getParentValue, getElementValue, true);
            }
        }

        internal bool RedirectPorts
        {
            get
            {
                Func<bool> getParentValue = () => this.Parent.RedirectPorts;
                Func<bool> getElementValue = this.PropertiesElement.GetRedirectPorts;
                return this.ResolveValue(getParentValue, getElementValue);
            }
        }

        internal bool RedirectPrinters
        {
            get
            {
                Func<bool> getParentValue = () => this.Parent.RedirectPrinters;
                Func<bool> getElementValue = this.PropertiesElement.GetRedirectPrinters;
                return this.ResolveValue(getParentValue, getElementValue);
            }
        }

        internal bool RedirectSmartCards
        {
            get
            {
                Func<bool> getParentValue = () => this.Parent.RedirectSmartCards;
                Func<bool> getElementValue = this.PropertiesElement.GetRedirectSmartCards;
                return this.ResolveValue(getParentValue, getElementValue);
            }
        }

        public LocalResources(XElement settingsElement, LocalResources parent = null)
            : base(settingsElement, parent)
        {
        }
    }
}