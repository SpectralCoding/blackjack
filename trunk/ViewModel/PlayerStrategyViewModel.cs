//-----------------------------------------------------------------------
// <copyright file="PlayerStrategyViewModel.cs" company="SpectralCoding">
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
using BlackJack.PlayerLogic;
using BlackJack.Utilities;
using BlackJack.CardLogic;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Player Strategy.
	/// </summary>
	public class PlayerStrategyViewModel : ViewModelBase {
		#region Private Fields
		private TableViewModel m_Parent;
		private PlayerHandViewModel m_ParentPlayerHandViewModel;
		private PlayerStrategyModel m_PlayerStrategyModel;
		#endregion

		/// <summary>
		/// Initializes a new instance of the PlayerStrategyViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public PlayerStrategyViewModel(TableViewModel Parent, PlayerHandViewModel ParentPHVM) {
			m_Parent = Parent;
			m_ParentPlayerHandViewModel = ParentPHVM;
			m_PlayerStrategyModel = new PlayerStrategyModel();
		}

		public PlayerAction GetSuggestedAction() {
			return PlayerAction.Stand;
			DealerCardInHand UpCard = m_Parent.DealerVM.DealerHandVM.Hand[1];
			bool HasAce = false;
			for (int i = 0; i < m_ParentPlayerHandViewModel.Hand.Count; i++) {
				if (m_ParentPlayerHandViewModel.Hand[i].Card.Type == CardType.Ace) {
					HasAce = true;
				}
			}
			switch (UpCard.Card.Type) {
				case CardType.Two:
					#region Two
					if (!HasAce) {
						if ((m_ParentPlayerHandViewModel.Count >= 5) && (m_ParentPlayerHandViewModel.Count <= 9)) {
							return PlayerAction.Hit;
						} else if ((m_ParentPlayerHandViewModel.Count == 10) || (m_ParentPlayerHandViewModel.Count == 11)) {
							return PlayerAction.DoubleDown;
						} else if (m_ParentPlayerHandViewModel.Count == 11) {
							return PlayerAction.Hit;
						} else if ((m_ParentPlayerHandViewModel.Count >= 13) && (m_ParentPlayerHandViewModel.Count <= 17)) {
							return PlayerAction.Stand;
						}
					} else {
						if ((m_ParentPlayerHandViewModel.Count >= 5) && (m_ParentPlayerHandViewModel.Count <= 9)) {
							return PlayerAction.Hit;
						} else if ((m_ParentPlayerHandViewModel.Count == 10) || (m_ParentPlayerHandViewModel.Count == 11)) {
							return PlayerAction.DoubleDown;
						} else if (m_ParentPlayerHandViewModel.Count == 11) {
							return PlayerAction.Hit;
						} else if ((m_ParentPlayerHandViewModel.Count >= 13) && (m_ParentPlayerHandViewModel.Count <= 17)) {
							return PlayerAction.Stand;
						}
					}
					break;
					#endregion
			}
		}
	}
}
