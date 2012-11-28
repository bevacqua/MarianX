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
			using (MarianX game = new MarianX())
			{
				game.Run();
			}
		}
	}
#endif
}

