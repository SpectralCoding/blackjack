//-----------------------------------------------------------------------
// <copyright file="CardInHand.cs" company="SpectralCoding">
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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BlackJack.Utilities;
using BlackJack.ViewModel;

namespace BlackJack.CardLogic {
	/// <summary>
	/// Represents a card's metadata.
	/// </summary>
	public class DealerCardInHand : ViewModelBase {
		#region Private Fields
		private Card m_Card;
		private BitmapSource m_CardImage;
		private Point m_Position = new Point(0, 0);
		private DealerHandViewModel m_Parent;
		private bool m_IsShowing;
		#endregion
		
		#region Public Properties
		public Point Position {
			get { return m_Position; }
			private set {
				m_Position = value;
				OnPropertyChanged("Position");
			}
		}
		/// <summary>
		/// Gets a value indicating the card that this contains.
		/// </summary>
		public Card Card {
			get { return m_Card; }
			private set {
				m_Card = value;
				OnPropertyChanged("Card");
			}
		}
		/// <summary>
		/// Gets a value indicating the BitmapSource which represents the card image.
		/// </summary>
		public BitmapSource CardImage {
			get {
				return m_CardImage;
			}
			private set {
				m_CardImage = value;
				OnPropertyChanged("CardImage");
			}
		}
		public bool IsShowing {
			get {
				return m_IsShowing;
			}
			set {
				if (m_IsShowing != value) {
					m_IsShowing = value;
					CardImage = GenerateCardImage();
					OnPropertyChanged("CardImage");
					OnPropertyChanged("IsShowing");
				}
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the CardInHand class.
		/// </summary>
		/// <param name="Parent">Placeholder for the Parent object.</param>
		/// <param name="BaseCard">The card in which to hold this class's data.</param>
		public DealerCardInHand(DealerHandViewModel Parent, Card BaseCard, bool ShowCard) {
			m_Card = BaseCard;
			m_Parent = Parent;
			m_IsShowing = ShowCard;
			CardImage = GenerateCardImage();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Generates a card image based on the card's properties.
		/// </summary>
		/// <returns>A BitmapSource object containing the Bitmap representing the card.</returns>
		private BitmapSource GenerateCardImage() {
			Stream TempStream;
			if (m_IsShowing) {
				TempStream = this.GetType().Assembly.GetManifestResourceStream("BlackJack.Resources.CardImages.Card_" + m_Card.ShortTitle + ".gif");
			} else {
				TempStream = this.GetType().Assembly.GetManifestResourceStream("BlackJack.Resources.CardImages.Card_Back_Red.gif");
			}
			System.Drawing.Bitmap sourceBMP = new System.Drawing.Bitmap(TempStream);
			BitmapSource tempBitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
				sourceBMP.GetHbitmap(),
				IntPtr.Zero,
				System.Windows.Int32Rect.Empty,
				BitmapSizeOptions.FromWidthAndHeight(sourceBMP.Width, sourceBMP.Height)
			);
			return tempBitmapSource;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Sets the Position property of the CardInHand class to the appropriate coordinates depending on what position the card is in.
		/// </summary>
		/// <param name="CardNumber">The position of the card in the hand.</param>
		public void SetPosition(int CardNumber, int TotalCards) {
			Position = new Point(700 - ((80.0 * CardNumber) + 383.0 - ((TotalCards - 1) * 40)), 0.0);
		}
		#endregion
	}
}
