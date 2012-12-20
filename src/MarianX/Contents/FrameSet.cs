using System;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class FrameSet
	{
		public int Row { get; set; }
		public int Start { get; set; }
		public int Frames { get; set; }
		public bool Loop { get; set; }
		public TimeSpan Interval { get; set; }
		public SpriteEffects Effects { get; set; }

		public int Length
		{
			get { return Frames + Start; }
		}

		public float Tilt { get; set; }

		public FrameSet()
		{
			Loop = true;
			Interval = TimeSpan.FromMilliseconds(100);
			Effects = SpriteEffects.None;
		}
	}
}