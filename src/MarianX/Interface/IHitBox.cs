using MarianX.Collisions;
using MarianX.Sprites;

namespace MarianX.Interface
{
	public interface IHitBox
	{
		AxisAlignedBoundingBox BoundingBox { get; }
		HitBoxState State { get; set; }
	}
}