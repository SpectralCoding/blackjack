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
		private TableViewModel TableViewModel;
		private DispatcherTimer AutoPlayTimer = new DispatcherTimer();

		#region Delegates
		#endregion

		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		/// <param name="ParentTVM">Parent TableViewModel object.</param>
		public MainWindow(TableViewModel ParentTVM) {
			InitializeComponent();
			TableViewModel = ParentTVM;
			BlackJackTable.DataContext = TableViewModel;
			ShoeContentsTabItem.DataContext = TableViewModel.ShoeVM;
			DebugLogTabItem.DataContext = TableViewModel.LoggingVM;
			BenchmarkTabItem.DataContext = TableViewModel.BenchmarkVM;
			HouseRulesTabItem.DataContext = TableViewModel.HouseRulesVM;
			DataContext = TableViewModel;
			AutoPlayTimer.Interval = TimeSpan.FromTicks(1);
			//AutoPlayTimer.Interval = TimeSpan.FromMilliseconds(500);
			AutoPlayTimer.Tick += new EventHandler(Tick);

		}

		private void RunBenchmarkBtn_Click(object sender, RoutedEventArgs e) {
			Task Benchtask = Task.Factory.StartNew(
				() => TableViewModel.BenchmarkVM.RunBenchmark()
			);
		}

		private void RunAllBenchmarksBtn_Click(object sender, RoutedEventArgs e) {
			Task Benchtask = Task.Factory.StartNew(
				() => TableViewModel.BenchmarkVM.RunAllBenchmarks()
			);
		}

		private void DebugTxt_TextChanged(object sender, TextChangedEventArgs e) {
			DebugTxt.ScrollToEnd();
		}

		private void StartGameBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.StartGame();
		}

		private void ShuffleShoeBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.ShoeVM.ResetShoe();
		}

		private void DealCardsBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.DealCards();
		}

		private void ClearTableBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.ClearTable();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {

		}

		private void PlayerStandBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.PlayerStand();
		}

		private void PlayerHitBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.PlayerHit();
		}

		private void PlayerDoubleDownBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.PlayerDoubleDown();
		}

		private void PlayerSplitBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.PlayerSplit();
		}

		private void PlayerSurrenderBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.PlayerSurrender();
		}

		private void PlayerDoubleForLessBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.PlayerDoubleForLess();
		}

		private void PlayerPlayStrategyBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.PlayerPlayStrategy();
		}

		private void NextStepBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.NextStep();
		}

		private void DealACardBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.DealACard();
		}

		private void DealerPlayBtn_Click(object sender, RoutedEventArgs e) {
			TableViewModel.DealerPlay();
		}

		private void PlayerTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {

		}

		private void Grid_Loaded(object sender, RoutedEventArgs e) {
			
		}

		private void Tick(object sender, EventArgs e) {
			TableViewModel.NextStep();
		}

		private void AutoPlayToggle_Click(object sender, RoutedEventArgs e) {
			if ((string)AutoPlayToggle.Content == "Start Auto Play") {
				AutoPlayToggle.Content = "Stop Auto Play";
				AutoPlayTimer.Start();
			} else {
				AutoPlayToggle.Content = "Start Auto Play";
				AutoPlayTimer.Stop();
			}
		}
	}
}

