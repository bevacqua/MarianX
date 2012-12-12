using System;
using MarianX.Contents;

namespace MarianX.Events
{
	public class AnimationCompleteArgs : EventArgs
	{
		public FrameSet FrameSet { get; set; }
	}
}