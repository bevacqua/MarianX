using MarianX.World.Configuration;

namespace MarianX.Physics
{
	public class GloopBoundingBox : AxisAlignedBoundingBox
	{
		public override int HitBoxWidth
		{
			get { return MagicNumbers.GloopHitBoxWidth; }
		}

		public override int HitBoxHeight
		{
			get { return MagicNumbers.GloopHitBoxHeight; }
		}
	}
}