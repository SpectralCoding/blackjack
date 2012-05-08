//-----------------------------------------------------------------------
// <copyright file="ResourcesModel.cs" company="SpectralCoding">
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
using System.Windows.Media.Imaging;

namespace BlackJack.Utilities {
	/// <summary>
	/// Represents all the resources available to the application.
	/// </summary>
	public class ResourcesModel {
		public Dictionary<string, BitmapSource> CardImages = new Dictionary<string, BitmapSource>();
	}
}
