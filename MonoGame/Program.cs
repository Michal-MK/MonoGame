namespace MonoGame {
	public static class Program {
		static void Main() {
			using (Game game = new Game()) {
				game.Run();
			}
		}
	}
}