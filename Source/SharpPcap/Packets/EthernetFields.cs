// $Id: EthernetFields.cs,v 1.1.1.1 2007/07/03 10:15:17 tamirgal Exp $

/// <summary>************************************************************************
/// Copyright (C) 2001, Patrick Charles and Jonas Lehmann                   *
/// Distributed under the Mozilla Public License                            *
/// http://www.mozilla.org/NPL/MPL-1.1.txt                                *
/// *************************************************************************
/// </summary>
using System;
namespace Tamir.IPLib.Packets
{
	
	
	/// <summary> Ethernet protocol field encoding information.
	/// 
	/// </summary>
	/// <author>  Patrick Charles and Jonas Lehmann
	/// </author>
	/// <version>  $Revision: 1.1.1.1 $
	/// </version>
	/// <lastModifiedBy>  $Author: tamirgal $ </lastModifiedBy>
	/// <lastModifiedAt>  $Date: 2007/07/03 10:15:17 $ </lastModifiedAt>
	public struct EthernetFields_Fields{
		/// <summary> Width of the ethernet type code in bytes.</summary>
		public readonly static int ETH_CODE_LEN = 2;
		/// <summary> Position of the destination MAC address within the ethernet header.</summary>
		public readonly static int ETH_DST_POS = 0;
		/// <summary> Position of the source MAC address within the ethernet header.</summary>
		public readonly static int ETH_SRC_POS;
		/// <summary> Position of the ethernet type field within the ethernet header.</summary>
		public readonly static int ETH_CODE_POS;
		/// <summary> Total length of an ethernet header in bytes.</summary>
		public readonly static int ETH_HEADER_LEN; // == 14
		static EthernetFields_Fields()
		{
			ETH_SRC_POS = MACAddress.WIDTH;
			ETH_CODE_POS = MACAddress.WIDTH * 2;
			ETH_HEADER_LEN = EthernetFields_Fields.ETH_CODE_POS + EthernetFields_Fields.ETH_CODE_LEN;
		}
	}
	public interface EthernetFields
	{
		//UPGRADE_NOTE: Members of interface 'EthernetFields' were extracted into structure 'EthernetFields_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		// field lengths
		
		
		// field positions
		
		
		// complete header length
		
	}
}