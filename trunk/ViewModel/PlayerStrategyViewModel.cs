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

namespace BlackJack.ViewModel {
	/// <summary>
	/// Provides logic for Player Strategy.
	/// </summary>
	public class PlayerStrategyViewModel : ViewModelBase {
		#region Private Fields
		private TableViewModel m_Parent;
		#endregion

		/// <summary>
		/// Initializes a new instance of the PlayerStrategyViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for the parent object.</param>
		public PlayerStrategyViewModel(TableViewModel Parent) {
			m_Parent = Parent;
		}
	}
}
