namespace MarianX.Contents
{
	public class SpriteSheetSettings
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public bool Guides { get; set; }

		public SpriteSheetSettings()
		{
			Guides = true;
		}
	}
}