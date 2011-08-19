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
		}
	}
}
