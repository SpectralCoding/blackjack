//-----------------------------------------------------------------------
// <copyright file="PlayerModel.cs" company="SpectralCoding">
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
using BlackJack.Utilities;

namespace BlackJack.PlayerLogic {
	/// <summary>
	/// Represents a Player.
	/// </summary>
	public class PlayerModel {
		public string Name { get; set; }
		public decimal Cash { get; set; }
		public PlayerMode PlayerMode { get; set; }
	}
}
