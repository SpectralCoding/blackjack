//-----------------------------------------------------------------------
// <copyright file="DealerViewModel.cs" company="SpectralCoding">
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

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for a Dealer.
	/// </summary>
	public class DealerViewModel : ViewModelBase {
		private TableViewModel m_Parent;
		private DealerModel m_DealerModel;
		private DealerHandViewModel m_DealerHandViewModel;

		#region Public Properties
		/// <summary>
		/// Gets a value indicating the name of the player.
		/// </summary>
		public DealerHandViewModel DealerHandVM {
			get {
				return m_DealerHandViewModel;
			}
			private set {
				m_DealerHandViewModel = value;
				OnPropertyChanged("PlayerHandVM");
			}
		}
		public bool IsActive {
			get {
				return m_DealerModel.IsActive;
			}
			set {
				m_DealerModel.IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		#endregion


		/// <summary>
		/// Initializes a new instance of the DealerViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for parent object.</param>
		public DealerViewModel(TableViewModel Parent) {
			m_Parent = Parent;
			m_DealerModel = new DealerModel();
			m_DealerHandViewModel = new DealerHandViewModel(m_Parent, this);
		}

		/// <summary>
		/// Resets a player's hands.
		/// </summary>
		public void Reset() {
			IsActive = false;
			m_DealerHandViewModel.Reset();
		}

		public void Play() {
			DealerHandVM.ShowAll();
			while (DealerHandVM.Count < 17) {
				DealerHandVM.RecieveCard(m_Parent.ShoeVM.DrawCard(), true);
			}
		}
	}
}
