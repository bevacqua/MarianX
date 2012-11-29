using System.Collections.Generic;

namespace MarianX.Contents
{
	public class SpriteSheetSettings
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public bool Guides { get; set; }
		public IList<FrameSet> FrameSets { get; set; }

		public SpriteSheetSettings()
		{
			Guides = true;
		}
	}
}