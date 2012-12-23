using MarianX.Enum;
using MarianX.Physics;

namespace MarianX.Interface
{
	public interface IHitBox
	{
		AxisAlignedBoundingBox BoundingBox { get; }
		HitBoxState State { get; set; }
	}
}