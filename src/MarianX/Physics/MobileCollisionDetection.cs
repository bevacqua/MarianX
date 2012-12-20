using MarianX.Mobiles;

namespace MarianX.Physics
{
	public class MobileCollisionDetection
	{
		public void Perform(Marian marian, Npc npc)
		{
			var aabb = marian.BoundingBox.Bounds;
			var aabbNpc = npc.BoundingBox.Bounds;
			if (aabbNpc.Intersects(aabb))
			{
				marian.Die();
			}
		}
	}
}