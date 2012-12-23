using System;
using MarianX.Items;
using MarianX.Mobiles.NPC;
using MarianX.Mobiles.Player;

namespace MarianX.Physics
{
	public class DynamicCollisionDetection
	{
		public void Perform(Marian marian, Npc npc)
		{
			var aabb = marian.BoundingBox.Bounds;
			var aabbNpc = npc.BoundingBox.Bounds;
			if (aabbNpc.Intersects(aabb))
			{
				if (!marian.Invulnerable)
				{
					marian.Die();
				}
			}
		}

		public void Perform(Marian marian, Item item)
		{
			var aabb = marian.BoundingBox.Bounds;
			var aabbItem = item.BoundingBox.Bounds;
			if (aabbItem.Intersects(aabb))
			{
				item.PickUp(marian);
			}
		}
	}
}