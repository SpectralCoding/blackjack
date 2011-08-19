//-----------------------------------------------------------------------
// <copyright file="MersenneTwister.cs" company="SpectralCoding">
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

namespace BlackJack.Utilities {
	/// <summary>
	/// Represents a random number generating class.
	/// </summary>
	public class MersenneTwister {
		private const int m_N = 624;
		private const int m_M = 397;
		private const uint m_K = 0x9908B0DFU;
		private const uint m_DefaultSeed = 4357;
        
		private ulong[] m_State = new ulong[m_N + 1];
		private int m_Next = 0;
		private ulong m_SeedValue;
		
		/// <summary>
		/// Initializes a new instance of the MersenneTwister class.
		/// </summary>
		public MersenneTwister() {
			SeedMT(m_DefaultSeed);
		}

		/// <summary>
		/// Initializes a new instance of the MersenneTwister class.
		/// </summary>
		/// <param name="seed">Seed to start the random number generator with.</param>
		public MersenneTwister(ulong seed) {
			m_SeedValue = seed;
			SeedMT(m_SeedValue);
		}
		
		/// <summary>
		/// Generates a random integer.
		/// </summary>
		/// <returns>A random integer.</returns>
		public ulong RandomInt() {
			ulong y;
			if ((m_Next + 1) > m_N) {
				return ReloadMT();
			}
			y  = m_State[m_Next++];
			y ^= y >> 11;
			y ^= (y << 7) & 0x9D2C5680U;
			y ^= (y << 15) & 0xEFC60000U;
			return y ^ y >> 18;
		}
		
		/// <summary>
		/// Sets a seed for the random number generator
		/// </summary>
		/// <param name="seed">The seed in which to use</param>
		private void SeedMT(ulong seed) {
			ulong x = (seed | 1U) & 0xFFFFFFFFU;
			int j = m_N;
			for (j = m_N; j >= 0; j--) {
				m_State[j] = (x *= 69069U) & 0xFFFFFFFFU;
			}
			m_Next = 0;
		}
		
		/// <summary>
		/// Generates a random number between the two ranges.
		/// </summary>
		/// <param name="lo">Lower limit.</param>
		/// <param name="hi">Upper limit.</param>
		/// <returns>Random number</returns>
		public int RandomRange(int lo, int hi) {		
			return Math.Abs((int)RandomInt() % (hi - lo + 1)) + lo;
		}
		
		/// <summary>
		/// Reloads the random number generator.
		/// </summary>
		/// <returns>A random number.</returns>
		private ulong ReloadMT() {
			ulong[] PartZero = m_State;
			int PartZeroPosition = 0;
			ulong[] PartTwo = m_State;
			int PartTwoPosition = 2;
			ulong[] PartM = m_State;
			int PartMPosition = m_M;
			ulong StateZero;
			ulong StateOne;
			int j;
			if ((m_Next + 1) > m_N) {
				SeedMT(m_SeedValue);
			}
			for (StateZero = m_State[0], StateOne = m_State[1], j = m_N - m_M + 1; --j > 0; StateZero = StateOne, StateOne = PartTwo[PartTwoPosition++]) {
				PartZero[PartZeroPosition++] = PartM[PartMPosition++] ^ (MixBits(StateZero, StateOne) >> 1) ^ (LowBit(StateOne) != 0 ? m_K : 0U);
			}
			for (PartM[0] = m_State[0], PartMPosition = 0, j = m_M; --j > 0; StateZero = StateOne, StateOne = PartTwo[PartTwoPosition++]) {
				PartZero[PartZeroPosition++] = PartM[PartMPosition++] ^ (MixBits(StateZero, StateOne) >> 1) ^ (LowBit(StateOne) != 0 ? m_K : 0U);
			}
			StateOne = m_State[0];
			PartZero[PartZeroPosition] = PartM[PartMPosition] ^ (MixBits(StateZero, StateOne) >> 1) ^ (LowBit(StateOne) != 0 ? m_K : 0U);
			StateOne ^= StateOne >> 11;
			StateOne ^= (StateOne << 7) & 0x9D2C5680U;
			StateOne ^= (StateOne << 15) & 0xEFC60000U;
			return StateOne ^ StateOne >> 18;
		}
		
		/// <summary>
		/// Generates a high bit
		/// </summary>
		/// <param name="u">Unknown parameter</param>
		/// <returns>A high bit</returns>
		private ulong HighBit(ulong u) {
			return u & 0x80000000U;
		}

		/// <summary>
		/// Generates a low bit
		/// </summary>
		/// <param name="u">Unknown parameter</param>
		/// <returns>Returns a low bit.</returns>
		private ulong LowBit(ulong u) {
			return u & 0x00000001U;
		}

		/// <summary>
		/// Generates low bits
		/// </summary>
		/// <param name="u">Unknown parameter</param>
		/// <returns>Returns low bits.</returns>
		private ulong LowBits(ulong u) {
			return u & 0x7FFFFFFFU;
		}

		/// <summary>
		/// Mixes the bits.
		/// </summary>
		/// <param name="u">The first Bit.</param>
		/// <param name="v">The second Bit.</param>
		/// <returns>Mixed bit.</returns>
		private ulong MixBits(ulong u, ulong v) {
			return HighBit(u) | LowBits(v);
		}
	}
}
