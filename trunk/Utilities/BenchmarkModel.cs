//-----------------------------------------------------------------------
// <copyright file="BenchmarkModel.cs" company="SpectralCoding">
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
using System.Windows;

namespace BlackJack.Utilities {
	/// <summary>
	/// Represents a benchmarking suite.
	/// </summary>
	public sealed class BenchmarkModel {
		public Dictionary<BenchmarkType, BenchmarkResults> BenchmarkData { get; set; }
		public bool CanStartBenchmark { get; set; }

		/// <summary>
		/// Initializes a new instance of the BenchmarkModel class.
		/// </summary>
		public BenchmarkModel() {
			BenchmarkData = new Dictionary<BenchmarkType, BenchmarkResults>();
			CanStartBenchmark = true;
		}
	}
}
