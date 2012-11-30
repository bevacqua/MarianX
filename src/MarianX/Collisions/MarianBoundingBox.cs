using MarianX.Interface;

namespace MarianX.Collisions
{
	public class MarianBoundingBox : AxisAlignedBoundingBox
	{
		public MarianBoundingBox()
		{
			HitBoxWidth = MagicNumbers.MarianHitBoxWidth;
			HitBoxHeight = MagicNumbers.MarianHitBoxHeight;
		}
	}
}