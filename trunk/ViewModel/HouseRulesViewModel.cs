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
		private TableViewModel m_ParentMasterViewModel;

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
				m_ParentMasterViewModel.ShoeVM.ResetShoe();
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
		public DealerHitMode DealerHitModeEnum {
			get {
				return m_HouseRulesModel.DealerHitMode;
			}
			set {
				m_HouseRulesModel.DealerHitMode = value;
				OnPropertyChanged("DealerHitModeEnum");
				OnPropertyChanged("DealerHitMode");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating split limit for aces
		/// </summary>
		public SplitAcesLimit SplitAcesLimitEnum {
			get {
				return m_HouseRulesModel.SplitAcesLimit;
			}
			set {
				m_HouseRulesModel.SplitAcesLimit = value;
				OnPropertyChanged("SplitAcesLimitEnum");
				OnPropertyChanged("SplitAcesLimit");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the string representation of the dealer's hit mode.
		/// </summary>
		public string DealerHitMode {
			get {
				if (m_HouseRulesModel.DealerHitMode == Utilities.DealerHitMode.OnSixteen) {
					return "16";
				} else if (m_HouseRulesModel.DealerHitMode == Utilities.DealerHitMode.OnSoftSeventeen) {
					return "Soft 17";
				} else {
					return null;
				}
			}
			set {
				if (value == "16") {
					m_HouseRulesModel.DealerHitMode = Utilities.DealerHitMode.OnSixteen;
				} else if (value == "Soft 17") {
					m_HouseRulesModel.DealerHitMode = Utilities.DealerHitMode.OnSoftSeventeen;
				}
				OnPropertyChanged("DealerHitModeEnum");
				OnPropertyChanged("DealerHitMode");
			}
		}
		/// <summary>
		/// Gets or sets a value indicating the string representation of the split limit for aces.
		/// </summary>
		public string SplitAcesLimit {
			get {
				if (m_HouseRulesModel.SplitAcesLimit == Utilities.SplitAcesLimit.Once) {
					return "One Time";
				} else if (m_HouseRulesModel.SplitAcesLimit == Utilities.SplitAcesLimit.Twice) {
					return "Two Times";
				} else {
					return null;
				}
			}
			set {
				if (value == "One Time") {
					m_HouseRulesModel.SplitAcesLimit = Utilities.SplitAcesLimit.Once;
				} else if (value == "Two Times") {
					m_HouseRulesModel.SplitAcesLimit = Utilities.SplitAcesLimit.Twice;
				} else if (value == "Three Times") {
					m_HouseRulesModel.SplitAcesLimit = Utilities.SplitAcesLimit.Thrice;
				}
				OnPropertyChanged("SplitAcesLimitEnum");
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
		public bool CasinoMode {
			get {
				return m_HouseRulesModel.CasinoMode;
			}
			set {
				m_HouseRulesModel.CasinoMode = value;
				OnPropertyChanged("CasinoMode");
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the HouseRulesViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public HouseRulesViewModel(TableViewModel Parent) {
			m_ParentMasterViewModel = Parent;
			m_HouseRulesModel = new HouseRulesModel();
		}
	}
}
