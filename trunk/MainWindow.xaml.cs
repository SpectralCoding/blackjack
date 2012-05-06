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
using System.Globalization;
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
		private TableViewModel BlankTVM = new TableViewModel();

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
				if (TableViewModel.HouseRulesVM.AutoPlaySpeed == AutoPlaySpeed.BlazingFast) {
					AutoPlayTimer.Interval = TimeSpan.FromTicks(1);
				} else if (TableViewModel.HouseRulesVM.AutoPlaySpeed == AutoPlaySpeed.Fast) {
					AutoPlayTimer.Interval = TimeSpan.FromMilliseconds(100);
				} else if (TableViewModel.HouseRulesVM.AutoPlaySpeed == AutoPlaySpeed.Medium) {
					AutoPlayTimer.Interval = TimeSpan.FromMilliseconds(250);
				} else if (TableViewModel.HouseRulesVM.AutoPlaySpeed == AutoPlaySpeed.Screensaver) {
					AutoPlayTimer.Interval = TimeSpan.FromMilliseconds(500);
				}
				AutoPlayTimer.Start();
			} else {
				AutoPlayToggle.Content = "Start Auto Play";
				AutoPlayTimer.Stop();
			}
		}

		private void FastModeChk_Checked(object sender, RoutedEventArgs e) {
			if (TableViewModel.GameStatisticsVM.CardsDealt > 0) {
				BlackJackTable.DataContext = BlankTVM;
				DealerControlsTabItem.DataContext = BlankTVM;
				PlayerControlsTabItem.DataContext = BlankTVM.CurrentPlayerHandVM;
			}
		}

		private void FastModeChk_Unchecked(object sender, RoutedEventArgs e) {
			// This is so fucked up. God damn it.
			BlackJackTable.DataContext = TableViewModel;
			DealerControlsTabItem.DataContext = TableViewModel;
			PlayerControlsTabItem.DataContext = TableViewModel.CurrentPlayerHandVM;
		}

	}

	#region Converters
	#region SplitAcesEnumToString
	[ValueConversion(typeof(SplitAcesLimit), typeof(string))]
	public class SplitAcesEnumToString : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			SplitAcesLimit SplitAcesLimitEnum = (SplitAcesLimit)value;
			if (SplitAcesLimitEnum == SplitAcesLimit.Once) {
				return "One Time";
			} else if (SplitAcesLimitEnum == SplitAcesLimit.Twice) {
				return "Two Times";
			} else {
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			string SplitAcesLimitStr = (string)value;
			if (SplitAcesLimitStr == "One Time") {
				return SplitAcesLimit.Once;
			} else if (SplitAcesLimitStr == "Two Times") {
				return SplitAcesLimit.Twice;
			} else if (SplitAcesLimitStr == "Three Times") {
				return SplitAcesLimit.Thrice;
			}
			return null;
		}
	}
	#endregion
	#region DealerHitModeEnumToString
	[ValueConversion(typeof(DealerHitMode), typeof(string))]
	public class DealerHitModeEnumToString : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			DealerHitMode DealerHitModeEnum = (DealerHitMode)value;
			if (DealerHitModeEnum == DealerHitMode.OnSixteen) {
				return "16";
			} else if (DealerHitModeEnum == DealerHitMode.OnSoftSeventeen) {
				return "Soft 17";
			} else {
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			string DealerHitModeStr = (string)value;
			if (DealerHitModeStr == "16") {
				return DealerHitMode.OnSixteen;
			} else if (DealerHitModeStr == "Soft 17") {
				return DealerHitMode.OnSoftSeventeen;
			}
			return null;
		}
	}
	#endregion
	#region AutoPlaySpeedToString
	[ValueConversion(typeof(AutoPlaySpeed), typeof(string))]
	public class AutoPlaySpeedToString : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			AutoPlaySpeed AutoPlaySpeedEnum = (AutoPlaySpeed)value;
			if (AutoPlaySpeedEnum == AutoPlaySpeed.BlazingFast) {
				return "Blazing Fast";
			} else if (AutoPlaySpeedEnum == AutoPlaySpeed.Fast) {
				return "Fast";
			} else if (AutoPlaySpeedEnum == AutoPlaySpeed.Medium) {
				return "Medium";
			} else if (AutoPlaySpeedEnum == AutoPlaySpeed.Screensaver) {
				return "Screensaver";
			} else {
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			string AutoPlaySpeedStr = (string)value;
			if (AutoPlaySpeedStr == "Blazing Fast") {
				return AutoPlaySpeed.BlazingFast;
			} else if (AutoPlaySpeedStr == "Fast") {
				return AutoPlaySpeed.Fast;
			} else if (AutoPlaySpeedStr == "Medium") {
				return AutoPlaySpeed.Medium;
			} else if (AutoPlaySpeedStr == "Screensaver") {
				return AutoPlaySpeed.Screensaver;
			} else {
				return null;
			}
		}
	}
	#endregion
	#endregion


}

