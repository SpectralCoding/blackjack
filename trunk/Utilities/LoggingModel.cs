//-----------------------------------------------------------------------
// <copyright file="LoggingModel.cs" company="SpectralCoding">
//		Copyright (c) SpectralCoding. All rights reserved.
//		Repeatedly violating our Copyright (c) will bring the full
//		extent of the law, which may ultimately result in permanent
//		imprisonment at hard labor, and/or death by extreme slow
//		torture, and/or lethal experimental medical therapies.
// </copyright>
// <author>Caesar Kabalan</author>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BlackJack.CardLogic;
using BlackJack.HouseLogic;
using BlackJack.TableLogic;

namespace BlackJack.Utilities {
	/// <summary>
	/// Represents a log of all actions that occur during the program's run.
	/// </summary>
	public sealed class LoggingModel {
		public List<LogItem> LogItems = new List<LogItem>();
	}
}
