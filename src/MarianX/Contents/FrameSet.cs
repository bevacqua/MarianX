using System;

namespace MarianX.Contents
{
	public class FrameSet
	{
		public int Count { get; set; }
		public bool Loop { get; set; }
		public TimeSpan Interval { get; set; }

		public FrameSet()
		{
			Loop = true;
			Interval = TimeSpan.FromMilliseconds(100);
		}
	}
}