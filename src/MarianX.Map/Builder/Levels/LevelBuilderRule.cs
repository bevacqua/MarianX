namespace MarianX.Map.Builder.Levels
{
	public class LevelBuilderRule
	{
		public int Tile { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		public string TextWidth { get; set; }
		public string TextHeight { get; set; }
		public string TextX { get; set; }
		public string TextY { get; set; }

		public int Right
		{
			get { return X + Width; }
		}

		public int Bottom
		{
			get { return Y + Height; }
		}

		private bool processed;

		public void Process(int columns, int rows)
		{
			if (processed)
			{
				return;
			}
			processed = true;

			X = Parse(TextX, columns);
			Y = Parse(TextY, rows);
			Width = Parse(TextWidth, columns);
			Height = Parse(TextHeight, rows);
		}

		private int Parse(string text, int star)
		{
			int offset = 0;

			if (text.StartsWith("*"))
			{
				text = text.Substring(1, text.Length - 1).Trim();
				offset = star;
			}

			if (text.StartsWith("+"))
			{
				text = text.Substring(1, text.Length - 1);
			}

			if (string.IsNullOrEmpty(text))
			{
				return offset;
			}

			int result = int.Parse(text);
			return result + offset;
		}
	}
}