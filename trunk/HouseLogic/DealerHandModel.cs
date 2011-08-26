//-----------------------------------------------------------------------
// <copyright file="DealerHandModel.cs" company="SpectralCoding">
//		Copyright (c) SpectralCoding. All rights reserved.
//		Repeatedly violating our Copyright (c) will bring the full
//		extent of the law, which may ultimately result in permanent
//		imprisonment at hard labor, and/or death by extreme slow
//		torture, and/or lethal experimental medical therapies.
// </copyright>
// <author>Caesar Kabalan</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BlackJack.CardLogic;
using BlackJack.Utilities;

namespace BlackJack.HouseLogic {
	/// <summary>
	/// Represents a Player's Hand.
	/// </summary>
	public class DealerHandModel {
		public ObservableCollection<DealerCardInHand> Hand { get; set; }
		public int Count { get; set; }
		public string StringCount { get; set; }
		public bool IsActive { get; set; }
		public bool HasAce { get; set; }
		public bool IsBlackJack { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the DealerHandModel class.
		/// </summary>
		public DealerHandModel() {
			Count = 0;
			Hand = new ObservableCollection<DealerCardInHand>();
			IsActive = false;
			HasAce = false;
			IsBlackJack = false;
		}
	}
}
