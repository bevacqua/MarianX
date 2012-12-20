using System.Collections.Generic;
using MarianX.Events;
using MarianX.Mobiles;

namespace MarianX.Physics
{
	public class MobileCollisionDetectionEngine
	{
		private readonly Marian marian;
		private readonly IList<Npc> npcs;
		private readonly MobileCollisionDetection collisionDetection;

		public MobileCollisionDetectionEngine(Marian marian)
		{
			this.marian = marian;

			npcs = new List<Npc>();
			collisionDetection = new MobileCollisionDetection();

			marian.Move += marian_Move;
		}

		public void AddNpc(Npc npc)
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
		}

		private void npc_Move(Mobile sender, MoveArgs args)
		{
			collisionDetection.Perform(marian, (Npc)sender);
		}
	}
}