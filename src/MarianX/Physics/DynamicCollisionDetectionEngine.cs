using System.Collections.Generic;
using MarianX.Events;
using MarianX.Items;
using MarianX.Mobiles;
using MarianX.Mobiles.NPC;
using MarianX.Mobiles.Player;

namespace MarianX.Physics
{
	public class DynamicCollisionDetectionEngine
	{
		private readonly Marian marian;
		private readonly IList<Npc> npcs;
		private readonly IList<Item> items;

		private readonly DynamicCollisionDetection collisionDetection;

		public DynamicCollisionDetectionEngine(Marian marian)
		{
			this.marian = marian;

			npcs = new List<Npc>();
			items = new List<Item>();

			collisionDetection = new DynamicCollisionDetection();

			marian.Move += marian_Move;
		}

		public void TrackNpc(Npc npc)
		{
			npcs.Add(npc);
			npc.Move += npc_Move;
		}

		private void marian_Move(Mobile sender, MoveArgs args)
		{
			foreach (Npc npc in npcs)
			{
				collisionDetection.Perform(marian, npc);
			}

			foreach (Item item in items)
			{
				collisionDetection.Perform(marian, item);
			}
		}

		private void npc_Move(Mobile sender, MoveArgs args)
		{
			collisionDetection.Perform(marian, (Npc)sender);
		}

		public void TrackItem(Item item)
		{
			items.Add(item);
		}
	}
}