//-----------------------------------------------------------------------
// <copyright file="Structs.cs" company="SpectralCoding">
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

namespace BlackJack.Utilities {
	/// <summary>
	/// Contains benchmark results.
	/// </summary>
	public struct BenchmarkResults {
		public BenchmarkType Type;
		public string Name;
		public long Iterations;
		public double RunTime;
		public double IterationsPerSecond;
	}

	/// <summary>
	/// Contains DateTime, Type, and Message information for the LogItem.
	/// </summary>
	public struct LogItem {
		public DateTime Time;
		public LogActionType Type;
		public string Message;
	}
}
