//-----------------------------------------------------------------------
// <copyright file="PlayerTableHand.xaml.cs" company="SpectralCoding">
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
using BlackJack.ViewModel;

namespace BlackJack.Controls {
	/// <summary>
	/// Interaction logic for PlayerTableHand.xaml
	/// </summary>
	public partial class PlayerTableHand : UserControl {
		private PlayerHandViewModel m_PlayerHandViewModel;

		/// <summary>
		/// Initializes a new instance of the PlayerTableHand class.
		/// </summary>
		public PlayerTableHand() {
			InitializeComponent();
		}

		private void PlayerTableHandUC_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			m_PlayerHandViewModel = (PlayerHandViewModel)DataContext;
		}

	}

	[ValueConversion(typeof(bool), typeof(int))]
	public class BoolToStrokeThickness : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value != null) {
				bool IsActive = (bool)value;
				if (IsActive) {
					return 4;
				}
			}
			return 2;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new Exception("The method or operation is not implemented.");
		}
	}


	/// <summary>
	/// Class for converting a System.Drawing.Point to a Thickness for usage in a Margin property.
	/// </summary>
	[ValueConversion(typeof(Point), typeof(Thickness))]
	public class PointToMarginConverter : IValueConverter {
		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			System.Drawing.Point tempPoint = (System.Drawing.Point)value;
			////				 t, l, r, b
			return new Thickness(0, tempPoint.X, 0, tempPoint.Y);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}

	[ValueConversion(typeof(int), typeof(SolidColorBrush))]
	public class CountToSolidColorBrush : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value != null) {
				int Count = (int)value;
				if (Count == 0) {
					return new SolidColorBrush(Color.FromRgb(255, 255, 255));		// White
				} else if (Count < 14) {
					return new SolidColorBrush(Color.FromRgb(255, 128, 128));		// Light Red
				} else if (Count < 18) {
					return new SolidColorBrush(Color.FromRgb(255, 210, 128));		// Light Orange
				} else if (Count < 22) {
					return new SolidColorBrush(Color.FromRgb(144, 238, 144));		// Light Green
				} else if (Count > 21) {
					return new SolidColorBrush(Color.FromRgb(255, 128, 128));		// Light Red
				}
			}
			return Color.FromRgb(255, 255, 255);		// White
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new Exception("The method or operation is not implemented.");
		}
	}

}
