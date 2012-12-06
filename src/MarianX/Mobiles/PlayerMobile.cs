using MarianX.Contents;
using MarianX.Effects;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework.Content;

namespace MarianX.Mobiles
{
	public class PlayerMobile : Mobile
	{
		private static readonly SpriteSheetSettings settings;

		static PlayerMobile()
		{
			settings = new SpriteSheetSettings
			{
				Width = MagicNumbers.FrameWidth,
				Height = MagicNumbers.FrameHeight
			};
		}

		public override Direction Direction
		{
			get
			{
				return base.Direction;
			}
			set
			{
				if (Direction != Direction.None && Direction == value)
				{
					return;
				}
				base.Direction = value;

				animation.UpdateFace();
				animation.Update();
			}
		}

		private readonly Animation animation;
		private readonly PlayerSoundManager soundManager;

		public PlayerMobile(string assetName)
			: base(assetName, settings)
		{
			animation = new Animation(this);
			soundManager = new PlayerSoundManager();
		}

		public override void Load(ContentManager content)
		{
			base.Load(content);
			animation.Load();
			soundManager.Load(content);
		}

		protected void JumpEffects()
		{
			animation.Jump();
			soundManager.Jump();
		}
	}
}