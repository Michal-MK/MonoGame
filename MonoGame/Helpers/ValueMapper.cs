using System.Runtime.CompilerServices;

namespace MonoGame {
	public static class ValueMapper {

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Map(double input, double inFrom, double inTo, double outFrom, double outTo) {
			double slope = 1.0 * (outTo - outFrom) / (inTo - inFrom);
			return outFrom + slope * (input - inFrom);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Map(int input, int inFrom, int inTo, int outFrom, int outTo) {
			int slope = (outTo - outFrom) / (inTo - inFrom);
			return outFrom + slope * (input - inFrom);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Map(double input, double inFrom, double inTo, int outFrom, int outTo) {
			double slope = (outTo - outFrom) / (inTo - inFrom);
			return (int)(outFrom + slope * (input - inFrom));
		}
	}
}
