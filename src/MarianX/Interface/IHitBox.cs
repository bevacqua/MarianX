using MarianX.Physics;
using MarianX.Sprites;

namespace MarianX.Interface
{
	public interface IHitBox
	{
		AxisAlignedBoundingBox BoundingBox { get; }
		HitBoxState State { get; set; }
	}
}