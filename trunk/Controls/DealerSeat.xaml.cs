//-----------------------------------------------------------------------
// <copyright file="DealerSeat.xaml.cs" company="SpectralCoding">
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
using System.Globalization;
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
using BlackJack.CardLogic;
using BlackJack.ViewModel;

namespace BlackJack.Controls {
	/// <summary>
	/// Interaction logic for DealerSeat.xaml
	/// </summary>
	public partial class DealerSeat : UserControl {
		private DealerViewModel m_DealerViewModel;

		/// <summary>
		/// Initializes a new instance of the DealerSeat class.
		/// </summary>
		public DealerSeat() {
			this.InitializeComponent();
		}

		private void DealerSeatUC_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			m_DealerViewModel = (DealerViewModel)DataContext;
		}
	}

	[ValueConversion(typeof(ObservableCollection<DealerCardInHand>), typeof(Visibility))]
	public class CardsToVisibility : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value != null) {
				ObservableCollection<DealerCardInHand> DealerCards = (ObservableCollection<DealerCardInHand>)value;
				if (DealerCards.Count == 0) {
					return Visibility.Hidden;
				}
				foreach (DealerCardInHand currentDCIH in DealerCards) {
					if (currentDCIH.IsShowing == false) {
						return Visibility.Hidden;
					}
				}
			}
			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new Exception("The method or operation is not implemented.");
		}
	}
}