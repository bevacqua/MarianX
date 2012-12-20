using System;
using MarianX.Contents;
using MarianX.Core;
using MarianX.World.Interface;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Mobiles
{
	public abstract class Npc : Mobile
	{
		protected Npc(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			GameCore.Instance.MobileCollisionDetection.AddNpc(this);
		}

		public override void UpdateOutput(GameTime gameTime)
		{
			if (In(TileMatrix.Instance.Metadata))
			{
				base.UpdateOutput(gameTime);
			}
		}

		private bool In(ILevel metadata)
		{
			if (metadata.Level == 2)
			{
				throw new InvalidOperationException();
			}

			return true;
		}
	}
}