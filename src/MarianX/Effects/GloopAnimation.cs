using MarianX.Contents;
using MarianX.Mobiles;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Effects
{
	public class GloopAnimation : Animation
	{
		private readonly FrameSet faceRight = new FrameSet { Row = 0, Frames = 13 };
		private readonly FrameSet faceLeft = new FrameSet { Row = 0, Frames = 13, Effects = SpriteEffects.FlipHorizontally };

		public GloopAnimation(Mobile mobile)
			: base(mobile)
		{
		}

		public void Update()
		{
			mobile.SetFrameSet(lastFacedLeft ? faceLeft : faceRight);
		}
	}
}