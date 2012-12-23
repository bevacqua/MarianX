using System;
using MarianX.Contents;
using MarianX.Diagnostics;
using MarianX.Effects;
using MarianX.Enum;
using MarianX.Physics;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Mobiles.Player
{
	public class PlayerMobile : DiagnosticMobile
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

		private readonly PlayerAnimation animation;
		private readonly PlayerSoundManager soundManager;

		public bool Invulnerable { get; set; }

		public PlayerMobile(string assetName)
			: base(assetName, settings)
		{
			animation = new PlayerAnimation(this);
			soundManager = new PlayerSoundManager();
		}

		public override void Load(ContentManager content)
		{
			base.Load(content);
			soundManager.Load(content);
		}

		protected void IdleEffects()
		{
			animation.Update(); // invalidate.
		}

		protected void JumpEffects()
		{
			animation.Jump();
			soundManager.Jump();
		}

		protected void FallEffects()
		{
			soundManager.Fall();
		}

		protected void DeathEffects()
		{
			animation.Die();
			soundManager.Die();
		}

		protected void LevelCompleteEffects()
		{
			animation.Cheer();
		}

		private float offset;

		protected void GameCompleteEffects()
		{
			if (Tint == Color.White)
			{
				Tint = Color.FromNonPremultiplied(240, 40, 40,255);
			}
			if (Scale + .2f < 1.8f)
			{
				Scale += .2f;
				Tint = Color.Multiply(Tint, 0.8f);
				offset = ContentHeight * .2f;
				float x = Position.X;
				float y = Position.Y - offset;
				Position = new Vector2(x, y);
			}
			animation.Cheer();
		}

		private Color tintBeforeFlash;
		private DateTime flashStart;
		private int flashFrame;
		private bool hideFrame;

		protected void Flash()
		{
			Invulnerable = true;
			flashFrame = 0;
			flashStart = DateTime.UtcNow;
			tintBeforeFlash = Tint;
			Tint = MagicNumbers.InvulnerableTint;
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (Invulnerable)
			{
				bool flashing = flashStart + MagicNumbers.InvulnerableTimeout > DateTime.UtcNow;
				if (!flashing || State == HitBoxState.Dead)
				{
					Invulnerable = false;
					hideFrame = false;
					Tint = tintBeforeFlash;
				}
				else
				{
					hideFrame = ++flashFrame % MagicNumbers.InvulnerableFrameInterval == 0;

					if (hideFrame)
					{
						flashFrame = 0;
					}
				}
			}

			if (!hideFrame)
			{
				base.Draw(gameTime, spriteBatch);
			}
		}
	}
}