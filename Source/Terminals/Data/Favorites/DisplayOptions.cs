﻿using System;

namespace Terminals.Data
{
    [Serializable]
    public class DisplayOptions : IDisplayOptions
    {
        public Int32 Height { get; set; }
        public Int32 Width { get; set; }

        private DesktopSize desktopSize = DesktopSize.FitToWindow;
        public DesktopSize DesktopSize
        {
            get
            {
                return desktopSize;
            }
            set
            {
                desktopSize = value;
            }
        }

        private Colors colors = Colors.Bits32;
        public Colors Colors
        {
            get
            {
                return colors;
            }
            set
            {
                colors = value;
            }
        }

        internal DisplayOptions Copy()
        {
            return new DisplayOptions
                {
                    Height = this.Height,
                    Width = this.Width,
                    DesktopSize = this.DesktopSize,
                    Colors = this.Colors
                };
        }
    }
}
