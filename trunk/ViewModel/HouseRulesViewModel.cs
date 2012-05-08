//-----------------------------------------------------------------------
// <copyright file="HouseRulesViewModel.cs" company="SpectralCoding">
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
using BlackJack.HouseLogic;
using BlackJack.Utilities;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for House Rules.
	/// </summary>
	public class HouseRulesViewModel : ViewModelBase {
		private HouseRulesModel m_HouseRulesModel;
		private TableViewModel m_MasterParent;

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating the number of decks in the shoe.
		/// </summary>
		public int DecksInShoe {
			get {
				return m_HouseRulesModel.DecksInShoe;
			}
			set {
				m_HouseRulesModel.DecksInShoe = value;
				OnPropertyChanged("DecksInShoe");
				m_MasterParent.ShoeVM.ResetShoe();
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the maximum number of decks in the shoe.
		/// </summary>
		public int MaxDecksInShoe {
			get {
				return m_HouseRulesModel.MaxDecksInShoe;
			}
			set {
				m_HouseRulesModel.MaxDecksInShoe = value;
				OnPropertyChanged("MaxDecksInShoe");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the minimum number of decks in the shoe.
		/// </summary>
		public int MinDecksInShoe {
			get {
				return m_HouseRulesModel.MinDecksInShoe;
			}
			set {
				m_HouseRulesModel.MinDecksInShoe = value;
				OnPropertyChanged("MinDecksInShoe");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the percentage of the way through the shoe the game is.
		/// </summary>
		public double ShoePenetration {
			get {
				return m_HouseRulesModel.ShoePenetration;
			}
			set {
				m_HouseRulesModel.ShoePenetration = value;
				OnPropertyChanged("ShoePenetration");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the shuffle mode for the game.
		/// </summary>
		public ShuffleMode ShuffleMode {
			get {
				return m_HouseRulesModel.ShuffleMode;
			}
			set {
				m_HouseRulesModel.ShuffleMode = value;
				OnPropertyChanged("ShuffleMode");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the dealer's hit mode
		/// </summary>
		public DealerHitMode DealerHitMode {
			get {
				return m_HouseRulesModel.DealerHitMode;
			}
			set {
				m_HouseRulesModel.DealerHitMode = value;
				OnPropertyChanged("DealerHitMode");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the string representation of the split limit for aces.
		/// </summary>
		public SplitAcesLimit SplitAcesLimit {
			get {
				return m_HouseRulesModel.SplitAcesLimit;
			}
			set {
				m_HouseRulesModel.SplitAcesLimit = value;
				OnPropertyChanged("SplitAcesLimit");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the numerator for the blackjack payout.
		/// </summary>
		public int BlackJackPayNumerator {
			get {
				return m_HouseRulesModel.BlackJackPayNumerator;
			}
			set {
				m_HouseRulesModel.BlackJackPayNumerator = value;
				OnPropertyChanged("BlackJackPayNumerator");
			}		
		}
		/// <summary>
		/// Gets or sets a value indicating the denominator for the blackjack payout.
		/// </summary>
		public int BlackJackPayDenominator {
			get {
				return m_HouseRulesModel.BlackJackPayDenominator;
			}
			set {
				m_HouseRulesModel.BlackJackPayDenominator = value;
				OnPropertyChanged("BlackJackPayDenominator");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the play speed of an automatic game.
		/// </summary>
		public AutoPlaySpeed AutoPlaySpeed {
			get {
				return m_HouseRulesModel.AutoPlaySpeed;
			}
			set {
				m_HouseRulesModel.AutoPlaySpeed = value;
				OnPropertyChanged("AutoPlaySpeed");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether or not table updates should happen. This speeds up processing time greatly.
		/// </summary>
		public bool FastMode {
			get {
				return m_HouseRulesModel.FastMode;
			}
			set {
				m_HouseRulesModel.FastMode = value;
				foreach (PlayerViewModel CurrentPVM in m_MasterParent.PlayerVM) {
					foreach (PlayerHandViewModel CurrentPHVM in CurrentPVM.PlayerHandVM) {
						CurrentPHVM.OnPropertyChanged("VisibleHand");
					}
				}
				m_MasterParent.DealerVM.DealerHandVM.OnPropertyChanged("VisibleHand");
				OnPropertyChanged("FastMode");
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the HouseRulesViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public HouseRulesViewModel(TableViewModel Parent) {
			m_MasterParent = Parent;
			m_HouseRulesModel = new HouseRulesModel();
		}
	}
}
