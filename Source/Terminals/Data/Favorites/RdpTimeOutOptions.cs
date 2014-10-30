﻿using System;

namespace Terminals.Data
{
    [Serializable]
    public class RdpTimeOutOptions
    {
        private int shutdownTimeout = 10;
        /// <summary>
        /// Gets or sets the value in range 10 - 600
        /// </summary>
        public Int32 ShutdownTimeout
        {
            get
            {
                return CorrectValueToInterval(10, 600, shutdownTimeout);
            }
            set
            {
                shutdownTimeout = CorrectValueToInterval(10, 600, value);
            }
        }

        private int overallTimeout = 600;
        /// <summary>
        /// Gets or sets the value in range 10 - 600
        /// </summary>
        public Int32 OverallTimeout
        {
            get
            {
                return CorrectValueToInterval(10, 600, overallTimeout);
            }
            set
            {
               overallTimeout = CorrectValueToInterval(10, 600, value);
            }
        }

        private int connectionTimeout = 600;
        /// <summary>
        /// Gets or sets the value in range 10 - 600
        /// </summary>
        public Int32 ConnectionTimeout
        {
            get
            {
                return CorrectValueToInterval(10, 600, connectionTimeout);
            }
            set
            {
                connectionTimeout = CorrectValueToInterval(10, 600, value);
            }
        }

        private int idleTimeout = 240;
        /// <summary>
        /// Gets or sets the value in range 10 - 600, default is 240
        /// </summary>
        public Int32 IdleTimeout
        {
            get
            {
                return CorrectValueToInterval(10, 240, idleTimeout);
            }
            set
            {
                idleTimeout = CorrectValueToInterval(10, 240, value);
            }
        }

        private static int CorrectValueToInterval(int minimum, int maximum, int value)
        {
            if (value > maximum)
                return maximum;

            if (value < minimum)
                return minimum;

            return value;
        }

        internal RdpTimeOutOptions Copy()
        {
            return new RdpTimeOutOptions
                {
                    IdleTimeout = this.IdleTimeout,
                    ConnectionTimeout = this.ConnectionTimeout,
                    OverallTimeout = this.OverallTimeout,
                    ShutdownTimeout = this.ShutdownTimeout
                };
        }

        internal void FromConfigFavorite(FavoriteConfigurationElement favorite)
        {
            this.IdleTimeout = favorite.IdleTimeout;
            this.ConnectionTimeout = favorite.ConnectionTimeout;
            this.OverallTimeout = favorite.OverallTimeout;
            this.ShutdownTimeout = favorite.ShutdownTimeout;
        }

        internal void ToConfigFavorite(FavoriteConfigurationElement favorite)
        {
            favorite.IdleTimeout = this.IdleTimeout;
            favorite.ConnectionTimeout = this.ConnectionTimeout;
            favorite.OverallTimeout = this.OverallTimeout;
            favorite.ShutdownTimeout = this.ShutdownTimeout;
        }
    }
}
