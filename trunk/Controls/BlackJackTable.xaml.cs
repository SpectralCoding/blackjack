//-----------------------------------------------------------------------
// <copyright file="BlackJackTable.xaml.cs" company="SpectralCoding">
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlackJack.ViewModel;

namespace BlackJack.Controls {
	/// <summary>
	/// Interaction logic for BlackJackTable.xaml
	/// </summary>
	public partial class BlackJackTable : UserControl {
		private TableViewModel MasterViewModel;

		/// <summary>
		/// Initializes a new instance of the BlackJackTable class.
		/// </summary>
		public BlackJackTable() {
			InitializeComponent();
		}

		private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			MasterViewModel = (TableViewModel)DataContext;
			DealerSeat.DataContext = MasterViewModel.DealerVM;
			Player1Seat.DataContext = MasterViewModel.PlayerVM[0];
			Player2Seat.DataContext = MasterViewModel.PlayerVM[1];
			Player3Seat.DataContext = MasterViewModel.PlayerVM[2];
			Player4Seat.DataContext = MasterViewModel.PlayerVM[3];
			Player5Seat.DataContext = MasterViewModel.PlayerVM[4];
			Player6Seat.DataContext = MasterViewModel.PlayerVM[5];
			Player7Seat.DataContext = MasterViewModel.PlayerVM[6];
		}
	}
}
