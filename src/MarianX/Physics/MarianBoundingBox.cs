using MarianX.World.Configuration;

namespace MarianX.Physics
{
	public class MarianBoundingBox : AxisAlignedBoundingBox
	{
		public override int HitBoxWidth
		{
			get { return MagicNumbers.HitBoxWidth; }
		}

		public override int HitBoxHeight
		{
			get { return MagicNumbers.HitBoxHeight; }
		}
	}
}