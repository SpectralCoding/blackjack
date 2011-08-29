//-----------------------------------------------------------------------
// <copyright file="ShoeViewer.xaml.cs" company="SpectralCoding">
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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJack
{
	/// <summary>
	/// Interaction logic for ShoeViewer.xaml
	/// </summary>
	public partial class ShoeViewer : UserControl, INotifyPropertyChanged {
		private static readonly DependencyProperty m_ShoeBitmapSource = DependencyProperty.Register("ShoeBitmapSource", typeof(BitmapSource), typeof(ShoeViewer), new FrameworkPropertyMetadata(null));
		private static readonly DependencyProperty m_ShoeBitmapWidth = DependencyProperty.Register("ShoeBitmapWidth", typeof(int), typeof(ShoeViewer), new FrameworkPropertyMetadata(0));

		#region INotifyPropertyChanged Members
		/// <summary>
		/// Raised when a property on this object has a new value.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises this object's PropertyChanged event.
		/// </summary>
		/// <param name="propertyName">The property that has a new value.</param>
		protected virtual void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null) {
				var e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the ShoeViewer class.
		/// </summary>
		public ShoeViewer() {
			this.InitializeComponent();
		}

		/// <summary>
		/// Gets or sets a BitmapSource type for the shoe's visual representation.
		/// </summary>
		public BitmapSource ShoeBitmapSource {
			get {
				return (BitmapSource)GetValue(m_ShoeBitmapSource);
			}
			set {
				SetValue(m_ShoeBitmapSource, value);
				////OnPropertyChanged("ShoeBitmapSource");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating the width of the shoe's bitmap.
		/// </summary>
		public int ShoeBitmapWidth {
			get {
				return (int)GetValue(m_ShoeBitmapWidth);
			}
			set {
				SetValue(m_ShoeBitmapWidth, value);
				////OnPropertyChanged("ShoeBitmapWidth");
			}
		}
	}
}
