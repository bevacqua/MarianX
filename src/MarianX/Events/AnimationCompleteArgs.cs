using System;
using MarianX.Contents;
using Microsoft.Xna.Framework;

namespace MarianX.Events
{
	public class AnimationCompleteArgs : EventArgs
	{
		public FrameSet FrameSet { get; set; }
		public GameTime GameTime { get; set; }
	}
}