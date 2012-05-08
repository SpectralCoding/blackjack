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
using System.Globalization;
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
using BlackJack.Utilities;
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

	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BoolToVisibility : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value != null) {
				bool IsActive = (bool)value;
				if (IsActive) {
					return Visibility.Visible;
				}
			}
			return Visibility.Hidden;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new Exception("The method or operation is not implemented.");
		}
	}

	[ValueConversion(typeof(double), typeof(TransformGroup))]
	public class DoubleToTransformGroup : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			TransformGroup returnTG = new TransformGroup();
			////returnTG.Children.Add(new SkewTransform());
			////returnTG.Children.Add(new RotateTransform());
			////returnTG.Children.Add(new TranslateTransform());
			if (value != null) {
				double Scale = (double)value;
				returnTG.Children.Add(new ScaleTransform(Scale, Scale));
			} else {
				returnTG.Children.Add(new ScaleTransform(1.0, 1.0));
			}
			return returnTG;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
