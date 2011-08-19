//-----------------------------------------------------------------------
// <copyright file="Card.cs" company="SpectralCoding">
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BlackJack.Utilities;

namespace BlackJack.CardLogic {
	/// <summary>
	/// Represents a card.
	/// </summary>
	public class Card {
		private CardSuit m_Suit;
		private CardType m_Type;
		private bool m_IsHigh;
		private int m_InternalValue;

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether an Ace is treated as 11 (true), or is treated as 1 (false).
		/// </summary>
		public bool IsHigh {
			get { return m_IsHigh; }
			set { m_IsHigh = value; }
		}

		/// <summary>
		/// Gets the suit of the card.
		/// </summary>
		public CardSuit Suit {
			get { return m_Suit; }
		}

		/// <summary>
		/// Gets the type of the card.
		/// </summary>
		public CardType Type {
			get { return m_Type; }
		}

		/// <summary>
		/// Gets the play value of a card.
		/// </summary>
		public int PlayValue {
			get {
				if ((m_InternalValue == 11) || (m_InternalValue == 12) || (m_InternalValue == 13)) {
					return 10;
				} else if (m_InternalValue == 14) {
					if (m_IsHigh) {
						return 11;
					} else {
						return 1;
					}
				} else {
					return m_InternalValue;
				}
			}
		}

		/// <summary>
		/// Gets the full title of the card.
		/// </summary>
		public string Title {
			get {
				string EnglishNumber = string.Empty;
				string EnglishSuit = string.Empty;
				switch (m_Type) {
					case CardType.CutCard: EnglishNumber = "CutCard"; break;
					case CardType.Two: EnglishNumber = "Two"; break;
					case CardType.Three: EnglishNumber = "Three"; break;
					case CardType.Four: EnglishNumber = "Four"; break;
					case CardType.Five: EnglishNumber = "Five"; break;
					case CardType.Six: EnglishNumber = "Six"; break;
					case CardType.Seven: EnglishNumber = "Seven"; break;
					case CardType.Eight: EnglishNumber = "Eight"; break;
					case CardType.Nine: EnglishNumber = "Nine"; break;
					case CardType.Ten: EnglishNumber = "Ten"; break;
					case CardType.Jack: EnglishNumber = "Jack"; break;
					case CardType.Queen: EnglishNumber = "Queen"; break;
					case CardType.King: EnglishNumber = "King"; break;
					case CardType.Ace: EnglishNumber = "Ace"; break;
				}

				switch (m_Suit) {
					case CardSuit.Club: EnglishSuit = "Club"; break;
					case CardSuit.Spade: EnglishSuit = "Spade"; break;
					case CardSuit.Diamond: EnglishSuit = "Diamond"; break;
					case CardSuit.Heart: EnglishSuit = "Heart"; break;
				}

				return string.Format("{0} of {1}s", EnglishNumber, EnglishSuit);
			}
		}

		/// <summary>
		/// Gets the short title of the card. Example: As
		/// </summary>
		public string ShortTitle {
			get {
				string ShortNumber = string.Empty;
				string ShortSuit = string.Empty;
				switch (m_Type) {
					case CardType.CutCard: ShortNumber = "C"; break;
					case CardType.Two: ShortNumber = "2"; break;
					case CardType.Three: ShortNumber = "3"; break;
					case CardType.Four: ShortNumber = "4"; break;
					case CardType.Five: ShortNumber = "5"; break;
					case CardType.Six: ShortNumber = "6"; break;
					case CardType.Seven: ShortNumber = "7"; break;
					case CardType.Eight: ShortNumber = "8"; break;
					case CardType.Nine: ShortNumber = "9"; break;
					case CardType.Ten: ShortNumber = "T"; break;
					case CardType.Jack: ShortNumber = "J"; break;
					case CardType.Queen: ShortNumber = "Q"; break;
					case CardType.King: ShortNumber = "K"; break;
					case CardType.Ace: ShortNumber = "A"; break;
				}
				switch (m_Suit) {
					case CardSuit.Club: ShortSuit = "c"; break;
					case CardSuit.Spade: ShortSuit = "s"; break;
					case CardSuit.Diamond: ShortSuit = "d"; break;
					case CardSuit.Heart: ShortSuit = "h"; break;
				}
				return string.Format("{0}{1}", ShortNumber, ShortSuit);
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the Card class.
		/// </summary>
		/// <param name="CardType">Type of card to create.</param>
		/// <param name="CardSuit">Suit of card to create.</param>
		public Card(CardType CardType, CardSuit CardSuit) {
			m_Suit = CardSuit;
			m_Type = CardType;
			m_InternalValue = (int)CardType;
			m_IsHigh = true;
		}
	}
}
