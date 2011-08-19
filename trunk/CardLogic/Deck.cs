//-----------------------------------------------------------------------
// <copyright file="Deck.cs" company="SpectralCoding">
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
using BlackJack.Utilities;

namespace BlackJack.CardLogic {
	/// <summary>
	/// Represents a deck of cards.
	/// </summary>
	public class Deck {
		private Card[] m_Card = new Card[52];

		/// <summary>
		/// Initializes a new instance of the Deck class.
		/// </summary>
		public Deck() {
			ResetDeck();
		}

		/// <summary>
		/// Resets a deck to a brand new deck (2-A of each suit). No Jokers.
		/// </summary>
		public void ResetDeck() {
			int CardIndex = 0;
			for (int CardSuit = 0; CardSuit < 4; CardSuit++) {
				for (int CardType = 2; CardType < 15; CardType++) {
					m_Card[CardIndex] = new Card((CardType)CardType, (CardSuit)CardSuit);
					CardIndex++;
				}
			}
		}

		/// <summary>
		/// Returns a card at the current position in the deck.
		/// </summary>
		/// <param name="CardPosition">Position of card object to retrieve.</param>
		/// <returns>Card object</returns>
		public Card Card(int CardPosition) {
			return m_Card[CardPosition];
		}
	}
}
