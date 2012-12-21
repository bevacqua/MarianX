namespace MarianX.Map.Builder.Levels
{
	public class LevelBuilderRule
	{
		public int Tile { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		public int Right
		{
			get { return X + Width; }
		}

		public int Bottom
		{
			get { return Y + Height; }
		}
	}
}