//-----------------------------------------------------------------------
// <copyright file="GameStatisticsViewModel.cs" company="SpectralCoding">
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

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Game Statistics.
	/// </summary>
	public class GameStatisticsViewModel : ViewModelBase {
		private TableViewModel m_Parent;

		/// <summary>
		/// Initializes a new instance of the GameStatisticsViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public GameStatisticsViewModel(TableViewModel Parent) {
			m_Parent = Parent;
		}
	}
}
