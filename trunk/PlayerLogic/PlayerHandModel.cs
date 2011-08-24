//-----------------------------------------------------------------------
// <copyright file="PlayerHandModel.cs" company="SpectralCoding">
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

namespace BlackJack.PlayerLogic {
	/// <summary>
	/// Represents a Player's Hand.
	/// </summary>
	public class PlayerHandModel {
		public ObservableCollection<CardInHand> Hand { get; set; }
		public decimal Bet { get; set; } 
		public HandMode HandMode { get; set; }
		public int Count { get; set; }
		public bool IsActive { get; set; }
		
		/// <summary>
		/// Initializes a new instance of the PlayerHandModel class.
		/// </summary>
		public PlayerHandModel() {
			Count = 0;
			Hand = new ObservableCollection<CardInHand>();
			HandMode = HandMode.Normal;
			IsActive = false;
		}
	}
}
