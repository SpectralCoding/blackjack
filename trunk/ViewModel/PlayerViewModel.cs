//-----------------------------------------------------------------------
// <copyright file="PlayerViewModel.cs" company="SpectralCoding">
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
using BlackJack.PlayerLogic;
using BlackJack.Utilities;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Player.
	/// </summary>
	public class PlayerViewModel : ViewModelBase {
		#region Private Fields
		private TableViewModel m_Parent;
		private PlayerModel m_PlayerModel;
		private PlayerHandViewModel[] m_PlayerHandViewModel;
		private int AceSplitCount = 0;
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets a value indicating the name of the player.
		/// </summary>
		public string Name {
			get {
				return m_PlayerModel.Name;
			}
		}
		public PlayerHandViewModel[] PlayerHandVM {
			get {
				return m_PlayerHandViewModel;
			}
			private set {
				m_PlayerHandViewModel = value;
				OnPropertyChanged("PlayerHandVM");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the mode that the player is in.
		/// </summary>
		public PlayerMode PlayerMode {
			get {
				return m_PlayerModel.PlayerMode;
			}
			set {
				m_PlayerModel.PlayerMode = value;
				OnPropertyChanged("PlayerMode");
			}
		}
		public bool IsActive {
			get {
				return m_PlayerModel.IsActive;
			}
			set {
				m_PlayerModel.IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the PlayerViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		/// <param name="PlayerNum">Indicates the player number.</param>
		public PlayerViewModel(TableViewModel Parent, int PlayerNum) {
			m_Parent = Parent;
			m_PlayerModel = new PlayerModel();
			PlayerHandVM = new PlayerHandViewModel[4];
			PlayerHandVM[0] = new PlayerHandViewModel(m_Parent, this);
			PlayerHandVM[1] = new PlayerHandViewModel(m_Parent, this);
			PlayerHandVM[2] = new PlayerHandViewModel(m_Parent, this);
			PlayerHandVM[3] = new PlayerHandViewModel(m_Parent, this);
			m_PlayerModel.Name = String.Format("Player {0}", PlayerNum);
		}

		/// <summary>
		/// Resets a player's hands.
		/// </summary>
		public void ResetHands() {
			IsActive = false;
			PlayerMode = PlayerMode.Normal;
			m_PlayerHandViewModel[0].ResetHand();
			m_PlayerHandViewModel[1].ResetHand();
			m_PlayerHandViewModel[2].ResetHand();
			m_PlayerHandViewModel[3].ResetHand();
			m_PlayerHandViewModel[0].HandMode = HandMode.Normal;
			//m_PlayerHandViewModel[1].HandMode = HandMode.Normal;
			//m_PlayerHandViewModel[2].HandMode = HandMode.Normal;
			//m_PlayerHandViewModel[3].HandMode = HandMode.Normal;

		}

		public bool CanSplit(int CardNum) {
			if (PlayerMode != PlayerMode.SplitThrice) {
				if (CardNum == 0) {
					if (m_Parent.HouseRulesVM.SplitAcesLimitEnum == SplitAcesLimit.Once) {
						if (AceSplitCount == 0) { return true; } else { return false; }
					} else if (m_Parent.HouseRulesVM.SplitAcesLimitEnum == SplitAcesLimit.Twice) {
						if (AceSplitCount < 2) { return true; } else { return false; }
					} else if (m_Parent.HouseRulesVM.SplitAcesLimitEnum == SplitAcesLimit.Thrice) {
						if (AceSplitCount < 3) { return true; } else { return false; }
					}
				} else {
					return true;
				}
			}
			return false;
		}

		public void Split(int HandIndex) {
			if (HandIndex != 3) {
				if (PlayerMode == PlayerMode.Normal) {
					PlayerMode = PlayerMode.SplitOnce;
				} else if (PlayerMode == PlayerMode.SplitOnce) {
					PlayerMode = PlayerMode.SplitTwice;
				} else if (PlayerMode == PlayerMode.SplitTwice) {
					PlayerMode = PlayerMode.SplitThrice;
				}
				for (int i = 0; i < 4; i++) {
					if (PlayerHandVM[i].HandMode == HandMode.NotPlaying) {
						PlayerHandVM[i].HandMode = HandMode.Normal;
						PlayerHandVM[i].RecieveCard(PlayerHandVM[HandIndex].SplitHand());
						PlayerHandVM[i].CanSurrender = false;
						return;
					}
				}
			} else {
				throw new Exception("Can't split on the last hand. How did we get here?");
			}
		}
	}
}
