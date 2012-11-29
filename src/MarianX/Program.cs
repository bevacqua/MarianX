using MarianX.Core;

namespace MarianX
{
#if WINDOWS || XBOX
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static void Main(string[] args)
		{
			using (GameCore game = new GameCore())
			{
				game.Run();
			}
		}
	}
#endif
}

