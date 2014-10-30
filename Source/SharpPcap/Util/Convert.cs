/*
Copyright (c) 2005 Tamir Gal, http://www.tamirgal.com, All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

	1. Redistributions of source code must retain the above copyright notice,
		this list of conditions and the following disclaimer.

	2. Redistributions in binary form must reproduce the above copyright 
		notice, this list of conditions and the following disclaimer in 
		the documentation and/or other materials provided with the distribution.

	3. The names of the authors may not be used to endorse or promote products
		derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR
OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Text;

namespace Tamir.IPLib.Util
{
	/// <summary>
	/// Summary description for Convert.
	/// </summary>
	/// <author>Tamir Gal</author>
	/// <version>  $Revision: 1.3 $ </version>
	/// <lastModifiedBy>  $Author: tamirgal $ </lastModifiedBy>
	/// <lastModifiedAt>  $Date: 2007/07/16 08:49:15 $ </lastModifiedAt>
	public class Convert
	{
		public static byte[] GetBytes(string str)
		{
			return Encoding.Default.GetBytes(str);
		}

		public static string GetString(byte[] bytes)
		{
			return Encoding.Default.GetString(bytes, 0, bytes.Length);
		}

		//From: http://www.ip2location.com/README-IP-COUNTRY.htm
		public static uint IpStringToInt32(string DottedIP)
		{
			int i;
			string [] arrDec;
			double num = 0;
			if (DottedIP == "")
			{
				return 0;
			}
			else
			{
				arrDec = DottedIP.Split('.');
				for(i = arrDec.Length - 1; i >= 0 ; i --)
				{
					num += ((int.Parse(arrDec[i])%256) * Math.Pow(256 ,(3 - i )));
				}
				return (uint)num;
			}
		}

		public static string IpInt32ToString(uint ip)
		{
			uint w =  ( ip / 16777216 ) % 256;
			uint x =  ( ip / 65536    ) % 256;
			uint y =  ( ip / 256      ) % 256;
			uint z =  ( ip            ) % 256;
			return (w+"."+x+"."+y+"."+z);
		}

		public static string IpInt32ToString(int ip)
		{
			return IpInt32ToString((uint)ip);
		}

		/// <summary>
		/// Converts a network mask string represntation into an integer representing the number of network bits
		/// </summary>
		public static Int32 MaskStringToBits( string mask )
		{
			uint m = IpStringToInt32( mask );
			int zeros = 0;
			uint mod = (m%2);
			while(mod==0)
			{
				m=m/2;
				mod=m%2;
				zeros++;
			}
			return 32-zeros;
		}

		public static string BytesToHex(byte[] bytes, int start, int len)
		{
			string hex = "";
			string byte_hex;

			for(int i=start; i<len; i++)
			{
				byte_hex = bytes[i].ToString("X");
				if (byte_hex.Length==1) 
					byte_hex="0"+byte_hex;
				hex += byte_hex;
			}
			return hex;
		}

		public static string BytesToHex(byte[] bytes)
		{
			if(bytes !=null)
				return BytesToHex(bytes, 0, bytes.Length);
			else
				return "";
		}

		/// <summary>
		/// Converts a time_t to DateTime
		/// </summary>
		public static DateTime Time_T2DateTime(uint time_t) 
		{
			long win32FileTime = 10000000*(long)time_t + 116444736000000000;
			return DateTime.FromFileTimeUtc(win32FileTime).ToLocalTime();
		}

	}
}
