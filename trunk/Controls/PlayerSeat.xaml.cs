//-----------------------------------------------------------------------
// <copyright file="PlayerSeat.xaml.cs" company="SpectralCoding">
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
	/// Interaction logic for PlayerSeat.xaml
	/// </summary>
	public partial class PlayerSeat : UserControl {
		private PlayerViewModel m_PlayerViewModel;

		/// <summary>
		/// Initializes a new instance of the PlayerSeat class.
		/// </summary>
		public PlayerSeat() {
			InitializeComponent();
		}

		private void PlayerSeat_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			m_PlayerViewModel = (PlayerViewModel)DataContext;
			PlayerTableHand1.DataContext = m_PlayerViewModel.PlayerHandVM[0];
			PlayerTableHand2.DataContext = m_PlayerViewModel.PlayerHandVM[1];
			PlayerTableHand3.DataContext = m_PlayerViewModel.PlayerHandVM[2];
			PlayerTableHand4.DataContext = m_PlayerViewModel.PlayerHandVM[3];
		}
	}
}
