using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGame {
	public static class MoreRandom {

		public static Vector2 RandomVec2(int width, int height, Random rand) {
			return new Vector2(rand.Next(0, width), rand.Next(0, height));
		}
	}
}
