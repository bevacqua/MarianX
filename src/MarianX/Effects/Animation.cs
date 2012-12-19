using MarianX.Mobiles;
using MarianX.Physics;

namespace MarianX.Effects
{
	public abstract class Animation
	{
		protected bool lastFacedLeft;
		protected readonly Mobile mobile;

		protected Animation(Mobile mobile)
		{
			this.mobile = mobile;
		}

		public void UpdateFace()
		{
			if (mobile.Direction == Direction.Left)
			{
				lastFacedLeft = true;
			}
			else if (mobile.Direction == Direction.Right)
			{
				lastFacedLeft = false;
			}
		}
	}
}