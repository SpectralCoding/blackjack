//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="SpectralCoding">
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using BlackJack.CardLogic;
using BlackJack.TableLogic;
using BlackJack.HouseLogic;
using BlackJack.Utilities;
using BlackJack.ViewModel;

namespace BlackJack {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private TableViewModel MasterViewModel;

		#region Delegates
		#endregion

		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		/// <param name="ParentTVM">Parent TableViewModel object.</param>
		public MainWindow(TableViewModel ParentTVM) {
			MasterViewModel = ParentTVM;
			DataContext = MasterViewModel;
			InitializeComponent();
			BlackJackTable.DataContext = MasterViewModel;
			ShoeContentsTabItem.DataContext = MasterViewModel.ShoeVM;
			DebugLogTabItem.DataContext = MasterViewModel.LoggingVM;
			BenchmarkTabItem.DataContext = MasterViewModel.BenchmarkVM;
			HouseRulesTabItem.DataContext = MasterViewModel.HouseRulesVM;
		}

		private void RunBenchmarkBtn_Click(object sender, RoutedEventArgs e) {
			Task Benchtask = Task.Factory.StartNew(
				() => MasterViewModel.BenchmarkVM.RunBenchmark()
			);
		}

		private void RunAllBenchmarksBtn_Click(object sender, RoutedEventArgs e) {
			Task Benchtask = Task.Factory.StartNew(
				() => MasterViewModel.BenchmarkVM.RunAllBenchmarks()
			);
		}

		private void DebugTxt_TextChanged(object sender, TextChangedEventArgs e) {
			DebugTxt.ScrollToEnd();
		}

		private void StartGameBtn_Click(object sender, RoutedEventArgs e) {
			MasterViewModel.StartGame();
		}

		private void ShuffleShoeBtn_Click(object sender, RoutedEventArgs e) {
			MasterViewModel.ShoeVM.ResetShoe();
		}

		private void DealCardsBtn_Click(object sender, RoutedEventArgs e) {
			MasterViewModel.DealCards();
		}

		private void ClearTableBtn_Click(object sender, RoutedEventArgs e) {
			MasterViewModel.ClearTable();
		}
	}
}
