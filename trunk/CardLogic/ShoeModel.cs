//-----------------------------------------------------------------------
// <copyright file="ShoeModel.cs" company="SpectralCoding">
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
using System.Windows.Media.Imaging;

namespace BlackJack.CardLogic {
	/// <summary>
	/// Represents a shoe of decks.
	/// </summary>
	public class ShoeModel {
		public Card[] ShoeContents;
		public int ShoePosition { get; set; }
		public BitmapSource ShoeBitmapSource { get; set; }
	}
}
