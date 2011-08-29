//-----------------------------------------------------------------------
// <copyright file="HouseRulesModel.cs" company="SpectralCoding">
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using BlackJack.Utilities;

namespace BlackJack.HouseLogic {
	/// <summary>
	/// Represents the rules by which the game of Blackjack is played.
	/// </summary>
	public class HouseRulesModel {
		public int DecksInShoe { get; set; }
		public int MaxDecksInShoe { get; set; }
		public int MinDecksInShoe { get; set; }
		public double ShoePenetration { get; set; }
		public ShuffleMode ShuffleMode { get; set; }
		public DealerHitMode DealerHitMode { get; set; }
		public SplitAcesLimit SplitAcesLimit { get; set; }
		public int BlackJackPayNumerator { get; set; }
		public int BlackJackPayDenominator { get; set; }
		public bool CasinoMode { get; set; }
		public AutoPlaySpeed AutoPlaySpeed { get; set; }
		public bool FastMode { get; set; }

		/// <summary>
		/// Initializes a new instance of the HouseRulesModel class.
		/// </summary>
		public HouseRulesModel() {
			DecksInShoe = 7;
			MaxDecksInShoe = 20;
			MinDecksInShoe = 1;
			ShoePenetration = 0.75;
			ShuffleMode = ShuffleMode.FisherYates;
			DealerHitMode = DealerHitMode.OnSixteen;
			SplitAcesLimit = SplitAcesLimit.Thrice;
			AutoPlaySpeed = AutoPlaySpeed.BlazingFast;
			BlackJackPayNumerator = 3;
			BlackJackPayDenominator = 2;
			CasinoMode = false;
			FastMode = false;
		}
	}
}
