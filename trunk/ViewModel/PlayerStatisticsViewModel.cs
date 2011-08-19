//-----------------------------------------------------------------------
// <copyright file="PlayerStatisticsViewModel.cs" company="SpectralCoding">
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
using BlackJack.Statistics;

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Player Statistics.
	/// </summary>
	public class PlayerStatisticsViewModel : ViewModelBase {
		#region Private Fields
		private TableViewModel m_Parent;
		private PlayerStatisticsModel m_PlayerStatisticsModel;
		#endregion

		/// <summary>
		/// Initializes a new instance of the PlayerStatisticsViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		/// <param name="PlayerNum">Indicates which player number this is.</param>
		public PlayerStatisticsViewModel(TableViewModel Parent, int PlayerNum) {
			m_Parent = Parent;
			m_PlayerStatisticsModel = new PlayerStatisticsModel();
			m_PlayerStatisticsModel.Name = String.Format("Player {0}", PlayerNum);
		}
	}
}
