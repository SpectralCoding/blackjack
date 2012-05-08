//-----------------------------------------------------------------------
// <copyright file="GameStatisticsModel.cs" company="SpectralCoding">
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
using System.Linq;
using System.Text;

namespace BlackJack.HouseLogic {
	/// <summary>
	/// Represents a Dealer.
	/// </summary>
	public class GameStatisticsModel {
		public long CardsDealt { get; set; }
		public long HandsPlayed { get; set; }
		public long RoundsPlayed { get; set; }
		public long ShoesShuffled { get; set; }

		/// <summary>
		/// Initializes a new instance of the GameStatisticsModel class.
		/// </summary>
		public GameStatisticsModel() {
			CardsDealt = 0;
			HandsPlayed = 0;
			RoundsPlayed = 0;
			ShoesShuffled = 0;
		}
	}
}
