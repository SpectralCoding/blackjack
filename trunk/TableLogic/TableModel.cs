//-----------------------------------------------------------------------
// <copyright file="TableModel.cs" company="SpectralCoding">
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using BlackJack.CardLogic;
using BlackJack.HouseLogic;
using BlackJack.PlayerLogic;
using BlackJack.Statistics;
using BlackJack.Utilities;
using BlackJack.ViewModel;

namespace BlackJack.TableLogic {
	/// <summary>
	/// Represents the current status of the Game.
	/// </summary>
	public sealed class TableModel {
		public bool CanDealCards { get; set; }
		public bool CanStartGame { get; set; }
		public PlayerViewModel CurrentPlayerVM { get; set; }
		public PlayerHandViewModel CurrentPlayerHandVM { get; set; }
	}
}
