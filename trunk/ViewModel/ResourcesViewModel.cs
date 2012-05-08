//-----------------------------------------------------------------------
// <copyright file="ResourcesViewModel.cs" company="SpectralCoding">
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

namespace BlackJack.ViewModel {
	/// <summary>
	/// Represents all resources available to an application.
	/// </summary>
	public class ResourcesViewModel : ViewModelBase {
		private TableViewModel m_Parent;
		private ResourcesModel m_ResourcesModel;
		private string[] m_CardList = { "Ac", "Ad", "Ah", "As", "2c", "2d", "2h", "2s", "3c", "3d", "3h", "3s", "4c", "4d", "4h", "4s", "5c", "5d", "5h", "5s", "6c", "6d", "6h", "6s", "7c", "7d", "7h", "7s", "8c", "8d", "8h", "8s", "9c", "9d", "9h", "9s", "Tc", "Td", "Th", "Ts", "Jc", "Jd", "Jh", "Js", "Qc", "Qd", "Qh", "Qs", "Kc", "Kd", "Kh", "Ks", "Back_Blue", "Back_Red", "Cs" };

		/// <summary>
		/// Gets a value which contains a dictionary of card images.
		/// </summary>
		public Dictionary<string, BitmapSource> CardImages {
			get {
				return m_ResourcesModel.CardImages;
			}
		}

		/// <summary>
		/// Initializes a new instance of the ResourcesViewModel class.
		/// </summary>
		/// <param name="Parent">Placeholder for parent object.</param>
		public ResourcesViewModel(TableViewModel Parent) {
			m_Parent = Parent;
			m_ResourcesModel = new ResourcesModel();
			CardLoad();
		}

		/// <summary>
		/// Loads all card images into memory.
		/// </summary>
		public void CardLoad() {
			Stream TempStream = null;
			System.Drawing.Bitmap sourceBMP = null;
			BitmapSource tempBitmapSource;
			int i = 0;
			foreach (string curCardName in m_CardList) {
				TempStream = this.GetType().Assembly.GetManifestResourceStream("BlackJack.Resources.CardImages.Card_" + curCardName + ".gif");
				sourceBMP = new System.Drawing.Bitmap(TempStream);
				tempBitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
					sourceBMP.GetHbitmap(),
					IntPtr.Zero,
					System.Windows.Int32Rect.Empty,
					BitmapSizeOptions.FromWidthAndHeight(sourceBMP.Width, sourceBMP.Height)
				);
				m_ResourcesModel.CardImages[curCardName] = tempBitmapSource;
				i++;
			}
			TempStream.Dispose();
			DeleteObject(sourceBMP.GetHbitmap());
		}
	}
}
