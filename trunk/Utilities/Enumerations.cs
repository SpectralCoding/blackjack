//-----------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="SpectralCoding">
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

namespace BlackJack.Utilities {
	/// <summary>
	/// Represents a mode a hand can be in.
	/// </summary>
	public enum HandMode {
		/// <summary>
		/// Represents a hand that is not being played.
		/// </summary>
		NotPlaying,
		/// <summary>
		/// Represents a normal hand.
		/// </summary>
		Normal,
		/// <summary>
		/// Represents a hand that is in Double Down mode.
		/// </summary>
		DoubleDown
	}

	/// <summary>
	/// Represents the rule regarding the number of times you can split Aces.
	/// </summary>
	public enum SplitAcesLimit {
		/// <summary>
		/// Represents the rule that Aces can only be split once.
		/// </summary>
		Once,
		/// <summary>
		/// Represents the rule that Aces can only be split twice.
		/// </summary>
		Twice,
		/// <summary>
		/// Represents the rule that Aces can only be split thrice.
		/// </summary>
		Thrice
	}

	/// <summary>
	/// Represents the rule regarding the point in which a dealer stop hitting.
	/// </summary>
	public enum DealerHitMode {
		/// <summary>
		/// Represents the rule that the dealer must hit on sixteen.
		/// </summary>
		OnSixteen,
		/// <summary>
		/// Represents the rule that the dealer must hit on soft seventeen.
		/// </summary>
		OnSoftSeventeen
	}

	/// <summary>
	/// Represents a mode in which a player can be in.
	/// </summary>
	public enum PlayerMode {
		/// <summary>
		/// Represents that a player is currently not playing this round.
		/// </summary>
		NotPlaying,
		/// <summary>
		/// Represents the a player is currently playing a normal round.
		/// </summary>
		Normal,
		/// <summary>
		/// Represents that a player is currently playing a hand that has been split once.
		/// </summary>
		SplitOnce,
		/// <summary>
		/// Represents that a player is currently playing a hand that has been split twice.
		/// </summary>
		SplitTwice,
		/// <summary>
		/// Represents that a player is currently playing a hand that has been split thrice.
		/// </summary>
		SplitThrice,
		/// <summary>
		/// Represents that a player is currently playing a double down.
		/// </summary>
		DoubleDown
	}

	/// <summary>
	/// Represents a type of Log Action.
	/// </summary>
	public enum LogActionType {
		/// <summary>
		/// Represents an Information type of log item
		/// </summary>
		Information,

		/// <summary>
		/// Represents an DealCard type of log item
		/// </summary>
		DealCard,

		/// <summary>
		/// Represents an ShoeShuffle type of log item
		/// </summary>
		ShoeShuffle,

		/// <summary>
		/// Represents an PlayerAction type of log item
		/// </summary>
		PlayerAction,

		/// <summary>
		/// Represents an Benchmark type of log item
		/// </summary>
		Benchmark
	}

	/// <summary>
	/// Represents a type of Benchmark.
	/// </summary>
	public enum BenchmarkType {
		/// <summary>
		/// Represents a Shoe Creation benchmark using the Fisher-Yates shuffle method.
		/// </summary>
		CreateShoeFisherYates,

		/// <summary>
		/// Represents a Shoe Creation benchmark using no shuffle method.
		/// </summary>
		NoShuffle,

		/// <summary>
		/// This is an unknown BenchmarkType.
		/// </summary>
		Resolved,

		/// <summary>
		/// Represents no benchmark type.
		/// </summary>
		None
	}

	/// <summary>
	/// Types of Card Suits.
	/// </summary>
	public enum CardSuit : int {
		/// <summary>
		/// Represents a Diamond.
		/// </summary>
		Diamond,

		/// <summary>
		/// Represents a Club.
		/// </summary>
		Club,

		/// <summary>
		/// Represents a Heart.
		/// </summary>
		Heart,
		
		/// <summary>
		/// Represents a Spade.
		/// </summary>
		Spade
	}

	/// <summary>
	/// Types of Cards.
	/// </summary>
	public enum CardType : int {
		/// <summary>
		/// Represents a cut card.
		/// </summary>
		CutCard = -1,

		/// <summary>
		/// Represents a Two.
		/// </summary>
		Two = 2,
		
		/// <summary>
		/// Represents a Three.
		/// </summary>
		Three = 3,
		
		/// <summary>
		/// Represents a Four.
		/// </summary>
		Four = 4,
		
		/// <summary>
		/// Represents a Five.
		/// </summary>
		Five = 5,
		
		/// <summary>
		/// Represents a Six.
		/// </summary>
		Six = 6,
		
		/// <summary>
		/// Represents a Seven.
		/// </summary>
		Seven = 7,
		
		/// <summary>
		/// Represents a Eight.
		/// </summary>
		Eight = 8,
		
		/// <summary>
		/// Represents a Nine.
		/// </summary>
		Nine = 9,
		
		/// <summary>
		/// Represents a Ten.
		/// </summary>
		Ten = 10,
		
		/// <summary>
		/// Represents a Jack.
		/// </summary>
		Jack = 11,
		
		/// <summary>
		/// Represents a Queen.
		/// </summary>
		Queen = 12,
		
		/// <summary>
		/// Represents a King.
		/// </summary>
		King = 13,
		
		/// <summary>
		/// Represents a Ace.
		/// </summary>
		Ace = 14
	}

	/// <summary>
	/// Represents a card shuffling method.
	/// </summary>
	public enum ShuffleMode {
		/// <summary>
		/// Represents the Fisher-Yates shuffling method.
		/// </summary>
		FisherYates,
		
		/// <summary>
		/// Represents no shufflinf method.
		/// </summary>
		NoShuffle
	}
}
