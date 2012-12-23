using MarianX.Contents;
using MarianX.Core;
using Microsoft.Xna.Framework;

namespace MarianX.Mobiles.NPC
{
	public abstract class Npc : Mobile
	{
		public Vector2 StartPosition { get; set; }

		protected Npc(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
			GameCore.Instance.DynamicCollisionDetection.TrackNpc(this);
			Move += GameCore.Instance.ViewportManager.OnMobileMoved;
		}

		public override void Initialize()
		{
			base.Initialize();
			Position = StartPosition;
		}
	}
}