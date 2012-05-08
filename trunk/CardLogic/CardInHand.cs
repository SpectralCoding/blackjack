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
	public class CardInHand : ViewModelBase {
		#region Private Fields
		private Card m_Card;
		private Point m_Position = new Point(0, 0);
		private PlayerHandViewModel m_Parent;
		private TableViewModel m_MasterParent;
		private int m_Rotation = 0;
		private double m_Scale = 1.0;
		private Visibility m_Visible = Visibility.Visible;
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Gets a value indicating whether or not the the card is visible.
		/// </summary>
		public Visibility Visible {
			get {
				return m_Visible;
			}
			private set {
				m_Visible = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("Visible"); }
			}
		}
		/// <summary>
		/// Gets a value indicating the position of the card.
		/// </summary>
		public Point Position {
			get { return m_Position; }
			private set {
				m_Position = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("Position"); }
			}
		}
		/// <summary>
		/// Gets a value indicating the rotation of the card.
		/// </summary>
		public int Rotation {
			get { return m_Rotation; }
			private set {
				m_Rotation = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("Rotation"); }
			}
		}
		/// <summary>
		/// Gets a value indicating the scale of the card.
		/// </summary>
		public double Scale {
			get { return m_Scale; }
			private set {
				m_Scale = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("Scale"); }
			}
		}
		/// <summary>
		/// Gets a value indicating the card that this contains.
		/// </summary>
		public Card Card {
			get { return m_Card; }
			private set {
				m_Card = value;
				if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("Card"); }
			}
		}
		/// <summary>
		/// Gets a value indicating the BitmapSource which represents the card image.
		/// </summary>
		public BitmapSource CardImage {
			get {
				return m_MasterParent.ResourcesVM.CardImages[m_Card.ShortTitle];
			}
		}
		#endregion

		#region Constructor


		/// <summary>
		/// Initializes a new instance of the CardInHand class.
		/// </summary>
		/// <param name="Parent">Placeholder for the Parent object.</param>
		/// <param name="MasterParent">Placeholder for the Master Parent object.</param>
		/// <param name="BaseCard">The card in which to hold this class's data.</param>
		public CardInHand(PlayerHandViewModel Parent, TableViewModel MasterParent, Card BaseCard) {
			m_Parent = Parent;
			m_MasterParent = MasterParent; 
			m_Card = BaseCard;
			if (!m_MasterParent.HouseRulesVM.FastMode) { OnPropertyChanged("CardImage"); }
		}
		#endregion

		#region Private Methods
		#endregion

		#region Public Methods
		/// <summary>
		/// Sets the Position property of the CardInHand class to the appropriate coordinates depending on what position the card is in.
		/// </summary>
		/// <param name="CardNumber">The position of the card in the hand.</param>
		public void SetPosition(int CardNumber) {
			if (m_Parent.HandMode == HandMode.DoubleDown) {
				#region Set Double Down Hand Card Locations
				m_Scale = 1;
				m_Rotation = 0;
				m_Visible = Visibility.Visible;
				switch (CardNumber) {
					case 0:
						Position = new Point(8, 0);
						break;
					case 1:
						Position = new Point(24, 25);
						break;
					case 2:
						Position = new Point(8, 125);
						m_Rotation = -90;
						break;
					default:
						Visible = Visibility.Hidden;
						break;
				}
				#endregion
			} else {
				#region Set Normal Hand Card Locations
				m_Scale = 1;
				m_Rotation = 0;
				m_Visible = Visibility.Visible;
				switch (CardNumber) {
					case 0:
						Position = new Point(8, 0);
						break;
					case 1:
						Position = new Point(24, 25);
						break;
					case 2:
						Position = new Point(40, 50);
						break;
					case 3:
						Position = new Point(40, 75);
						break;
					case 4:
						Position = new Point(40, 100);
						break;
					case 5:
						Position = new Point(40, 125);
						break;
					case 6:
						Position = new Point(40, 150);
						break;
					case 7:
						Position = new Point(40, 175);
						break;
					case 8:
						Position = new Point(8, 37);
						break;
					case 9:
						Position = new Point(24, 62);
						break;
					case 10:
						Position = new Point(24, 87);
						break;
					case 11:
						Position = new Point(24, 112);
						break;
					case 12:
						Position = new Point(24, 137);
						break;
					case 13:
						Position = new Point(24, 162);
						break;
					case 14:
						Position = new Point(24, 187);
						break;
					default:
						Visible = Visibility.Hidden;
						break;
				}
				#endregion
			}
		}
		#endregion
	}
}
