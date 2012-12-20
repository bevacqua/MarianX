using MarianX.Contents;
using MarianX.Mobiles;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Effects
{
	public class GloopAnimation : Animation
	{
		private readonly FrameSet faceLeft = new FrameSet { Row = 0, Frames = 13, Tilt = 0.01f };
		private readonly FrameSet faceRight = new FrameSet { Row = 0, Frames = 13, Tilt = -0.01f, Effects = SpriteEffects.FlipHorizontally };

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