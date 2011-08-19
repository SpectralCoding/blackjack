//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="SpectralCoding">
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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using BlackJack.ViewModel;

namespace BlackJack {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {
		/// <summary>
		/// Initializes the statup of the window.
		/// </summary>
		/// <param name="e">Startup Arguments</param>
		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);
			MainWindow MainWindow = new MainWindow(new MasterViewModel());
			MainWindow.Show();
		}
	}
}
