//-----------------------------------------------------------------------
// <copyright file="GameModel.cs" company="SpectralCoding">
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
using System.Linq;
using System.Text;
using BlackJack.CardLogic;
using BlackJack.HouseLogic;
using BlackJack.PlayerLogic;
using BlackJack.Statistics;
using BlackJack.Utilities;

namespace BlackJack.GameLogic {
	/// <summary>
	/// Represents a Game.
	/// </summary>
	public sealed class GameModel {
		private ShoeModel m_Shoe;
		private PlayerModel[] m_Player;
		private PlayerStrategyModel m_PlayerStrategy;
		private Dealer m_Dealer;
		private HouseRulesModel m_HouseRules;
		private GameStatistics m_GameStatistics;

		/// <summary>
		/// Gets or sets the current instance of the Singleton.
		/// </summary>
		public static GameModel Instance {
			get { return Nested.Instance; }
			set { Nested.Instance = value; }
		}

		/// <summary>
		/// Gets a value indicating the contents of the Shoe.
		/// </summary>
		public ShoeModel Shoe {
			get {
				return m_Shoe;
			}
		}

		public HouseRulesModel HouseRules {
			get {
				return m_HouseRules;
			}
			set {
				m_HouseRules = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the GameModel class.
		/// </summary>
		public GameModel() {
		}

		/// <summary>
		/// Starts a new game.
		/// </summary>
		public void StartGame() {
			m_Shoe = new ShoeModel();
		}

		/// <summary>
		/// Sets up the game.
		/// </summary>
		public void Setup() {
			m_Shoe = new ShoeModel();
		}

		#region Singleton Stuff
		/// <summary>
		/// Instantiates a new Singleton object.
		/// </summary>
		/// <returns>Singleton object.</returns>
		internal static GameModel Load() {
			return new GameModel();
		}

		/// <summary>
		/// Nested class which contains the Singleton instance.
		/// </summary>
		public class Nested {
			internal static GameModel Instance = GameModel.Load();

			/// <summary>
			/// Initializes static members of the Nested class.
			/// </summary>
			static Nested() {
			}
		}
		#endregion
	}
}
