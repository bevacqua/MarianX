using MarianX.Collisions;

namespace MarianX.Interface
{
	public interface IHitBox
	{
		AxisAlignedBoundingBox BoundingBox { get; }
	}
}