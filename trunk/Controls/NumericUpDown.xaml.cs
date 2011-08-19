//-----------------------------------------------------------------------
// <copyright file="NumericUpDown.xaml.cs" company="SpectralCoding">
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

namespace BlackJack
{
	/// <summary>
	/// Interaction logic for NumericUpDown.xaml
	/// </summary>
	public partial class NumericUpDown : UserControl, INotifyPropertyChanged {
		private static readonly DependencyProperty m_Value = DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(5));
		private static readonly DependencyProperty m_Max = DependencyProperty.Register("Max", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(10));
		private static readonly DependencyProperty m_Min = DependencyProperty.Register("Min", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(1));
		public event PropertyChangedEventHandler PropertyChanged;

		#region Public Properties
		/// <summary>
		/// Gets or sets the value for the control.
		/// </summary>
		public int Value {
			get {
				return (int)GetValue(m_Value);
			}
			set {
				SetValue(m_Value, value);
				OnPropertyChanged("Value");
			}
		}

		/// <summary>
		/// Gets or sets the maximum number for the control.
		/// </summary>
		public int Max {
			get {
				return (int)GetValue(m_Max);
			}
			set {
				SetValue(m_Max, value);
				OnPropertyChanged("Max");
			}
		}

		/// <summary>
		/// Gets or sets the minimum number for the control.
		/// </summary>
		public int Min {
			get {
				return (int)GetValue(m_Min);
			}
			set {
				SetValue(m_Min, value);
				OnPropertyChanged("Min");
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the NumericUpDown class.
		/// </summary>
		public NumericUpDown() {
			this.InitializeComponent();
		}

		/// <summary>
		/// Sends notifications telling the handler that the property has changed.
		/// </summary>
		/// <param name="PropertyName">The name of the property that has been changed.</param>
		private void OnPropertyChanged(string PropertyName) {
			PropertyChangedEventHandler Handler = PropertyChanged;
			if (Handler != null) {
				Handler(this, new PropertyChangedEventArgs(PropertyName));
			}
		}

		/// <summary>
		/// Adds support for incrementing numbers.
		/// </summary>
		/// <param name="sender">The object representing the sender.</param>
		/// <param name="e">Event arguments.</param>
		private void UpBtn_Click(object sender, RoutedEventArgs e) {
			if (Value != Max) {
				Value++;
			}
		}

		/// <summary>
		/// Adds support for decrementing numbers.
		/// </summary>
		/// <param name="sender">The object representing the sender.</param>
		/// <param name="e">Event arguments.</param>
		private void DownBtn_Click(object sender, RoutedEventArgs e) {
			if (Value != Min) {
				Value--;
			}
		}

		/// <summary>
		/// Adds support for changing numbers with the scroll wheel numbers.
		/// </summary>
		/// <param name="sender">The object representing the sender.</param>
		/// <param name="e">Event arguments.</param>
		private void ValueTxt_MouseWheel(object sender, MouseWheelEventArgs e) {
			if (e.Delta > 0) {
				UpBtn_Click(sender, null);
			} else if (e.Delta < 0) {
				DownBtn_Click(sender, null);
			}
		}
	}
}