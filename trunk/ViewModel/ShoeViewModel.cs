using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using BlackJack.Utilities;
using BlackJack.HouseLogic;
using BlackJack.TableLogic;
using BlackJack.CardLogic;

namespace BlackJack.ViewModel {
	public class ShoeViewModel : ViewModelBase {

		#region Private Fields
		private ShoeModel m_ShoeModel;
		private TableViewModel m_ParentMasterViewModel;
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the number of cards in the shoe.
		/// </summary>
		public int NumberOfCards {
			get { return m_ShoeModel.ShoeContents.Length; }
		}

		/// <summary>
		/// Gets the current position in the shoe.
		/// </summary>
		public int Position {
			get { return m_ShoeModel.ShoePosition; }
			set {
				m_ShoeModel.ShoePosition = value;
				OnPropertyChanged("Position");
			}
		}

		public Card[] Contents {
			get { return m_ShoeModel.ShoeContents; }
		}

		public BitmapSource ShoeBitmapSource {
			get { return m_ShoeModel.ShoeBitmapSource; }
		}

		public int ShoeBitmapWidth {
			get { return NumberOfCards * 15; }
		}

		public bool NeedToShuffle {
			get {
				return m_ShoeModel.NeedToShuffle;
			}
			set {
				m_ShoeModel.NeedToShuffle = value;
				OnPropertyChanged("NeedToShuffle");
			}
		}

		public bool IsBenchmark {
			get {
				return m_ShoeModel.IsBenchmark;
			}
			set {
				m_ShoeModel.IsBenchmark = value;
				OnPropertyChanged("IsBenchmark");
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the Shoe class.
		/// </summary>
		public ShoeViewModel(TableViewModel Parent, bool IsBenchmark = false) {
			m_ParentMasterViewModel = Parent;
			m_ShoeModel = new ShoeModel();
			IsBenchmark = false;
			ResetShoe();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Demolishes old shoe data, adds the decks, shuffles, and adds the cut card.
		/// </summary>
		public void ResetShoe() {
			int TotalCards = m_ParentMasterViewModel.HouseRulesVM.DecksInShoe * 52;
			int CutCardPosition = Convert.ToInt32(TotalCards * m_ParentMasterViewModel.HouseRulesVM.ShoePenetration);
			m_ShoeModel.ShoeContents = new Card[TotalCards + 1];
			Deck DummyDeck = new Deck();
			int i = 0;
			for (int DeckNum = 0; DeckNum < m_ParentMasterViewModel.HouseRulesVM.DecksInShoe; DeckNum++) {
				for (int CardNum = 0; CardNum < 52; CardNum++) {
					m_ShoeModel.ShoeContents[i] = DummyDeck.Card(CardNum);
					i++;
				}
			}
			Shuffle();
			m_ShoeModel.ShoeContents[TotalCards] = null;
			for (int x = TotalCards; x >= CutCardPosition; x--) {
				m_ShoeModel.ShoeContents[x] = m_ShoeModel.ShoeContents[x - 1];
			}
			m_ShoeModel.ShoeContents[CutCardPosition] = new Card(CardType.CutCard, CardSuit.Spade);
			if (!IsBenchmark) {
				//GenerateShoeImage();
				m_ParentMasterViewModel.LoggingVM.AddItem(LogActionType.ShoeShuffle, this);
				Position = 0;
				NeedToShuffle = false;
			}
		}


		/// <summary>
		/// Draws a card from the shoe and advances the shoe position.
		/// </summary>
		/// <returns>Card object</returns>
		public Card DrawCard() {
			Position++;
			if (m_ShoeModel.ShoeContents[Position - 1].Type == CardType.CutCard) {
				DrawCard();
				NeedToShuffle = true;
				return DrawCard();
			} else {
				return m_ShoeModel.ShoeContents[Position - 1];
			}
		}
		#endregion

		#region Private Methods
		private void GenerateShoeImage() {
			Bitmap tempBMP = new Bitmap((NumberOfCards * 15), 83);
			Graphics gfx = Graphics.FromImage(tempBMP);
			Rectangle sourceRect = new Rectangle(new Point(0, 0), new Size(15, 83));
			int i = 0;
			Stream TempStream;
			foreach (Card CurrentCard in Contents) {
				TempStream = this.GetType().Assembly.GetManifestResourceStream("BlackJack.Resources.CardImages.Card_" + CurrentCard.ShortTitle + ".gif");
				//sourceBMP = ;
				Bitmap bitmap;
				using (MemoryStream outStream = new MemoryStream()) {
					BitmapEncoder enc = new BmpBitmapEncoder();
					enc.Frames.Add(BitmapFrame.Create(m_ParentMasterViewModel.ResourcesVM.CardImages[CurrentCard.ShortTitle]));
					enc.Save(outStream);
					bitmap = new System.Drawing.Bitmap(outStream);
				}
				gfx.DrawImage(bitmap, i, 0, sourceRect, GraphicsUnit.Pixel);
				i += 15;
				DeleteObject(bitmap.GetHbitmap());
				TempStream.Dispose();
			}
			gfx.DrawLine(new Pen(Color.Black), new Point(0, 82), new Point(tempBMP.Width - 1, 82));
			gfx.DrawLine(new Pen(Color.Black), new Point(tempBMP.Width - 1, 0), new Point(tempBMP.Width - 1, 82));
			gfx.Dispose();
			m_ShoeModel.ShoeBitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
				tempBMP.GetHbitmap(),
				IntPtr.Zero,
				System.Windows.Int32Rect.Empty,
				BitmapSizeOptions.FromWidthAndHeight(tempBMP.Width, tempBMP.Height)
			);
			OnPropertyChanged("ShoeBitmapSource");
			OnPropertyChanged("ShoeBitmapWidth");
			DeleteObject(tempBMP.GetHbitmap());
		}

		/// <summary>
		/// Shuffles the shoe with the shoe's specified algorithm.
		/// </summary>
		private void Shuffle() {
			// http://en.wikipedia.org/wiki/Shuffling
			if (m_ParentMasterViewModel.HouseRulesVM.ShuffleMode == ShuffleMode.FisherYates) {
				// http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
				int RandomPosition;
				MersenneTwister RandomMT = new MersenneTwister(Convert.ToUInt64(DateTime.Now.Ticks));
				Card tempCard;
				for (int i = NumberOfCards - 2; i > 1; i--) {
					RandomPosition = RandomMT.RandomRange(0, i);
					tempCard = m_ShoeModel.ShoeContents[RandomPosition];
					m_ShoeModel.ShoeContents[RandomPosition] = m_ShoeModel.ShoeContents[i];
					m_ShoeModel.ShoeContents[i] = tempCard;
				}
			} else if (m_ParentMasterViewModel.HouseRulesVM.ShuffleMode == ShuffleMode.NoShuffle) {
				// Do anything?
			}
		}
		#endregion
	}
}
