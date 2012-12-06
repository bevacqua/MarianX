using System;
using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Core;
using MarianX.Enum;
using MarianX.Sprites;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Mobiles
{
	public class Marian : AnimatedPlayerMobile
	{
		private const string AssetName = "marian";

		private TimeSpan lastJumpStarted;

		public Marian()
			: base(AssetName)
		{
			BoundingBox = new MarianBoundingBox();
		}

		public override void Initialize()
		{
			base.Initialize();
			Position = new Vector2(MagicNumbers.StartX, MagicNumbers.StartY);
		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();
			UpdateMovement(keyboardState, gameTime);

			base.Update(gameTime);
		}

		protected override MoveResult UpdatePosition(Vector2 interpolation)
		{
			var wasAirborne = State == HitBoxState.Airborne;

			MoveResult result = base.UpdatePosition(interpolation);

			if (wasAirborne && State != HitBoxState.Airborne)
			{
				Direction = Direction.None;
			}
			return result;
		}

		public override Direction Direction
		{
			get
			{
				return base.Direction;
			}
			set
			{
				if (Direction == value)
				{
					return;
				}
				base.Direction = value;
				Speed = Vector2.Zero;
			}
		}

		private void UpdateMovement(KeyboardState keyboardState, GameTime gameTime)
		{
			var kb = new KeyboardConfiguration(keyboardState);

			if (State == HitBoxState.Surfaced)
			{
				UpdateMovementSurfaced(kb, gameTime);
			}
			else if (State == HitBoxState.Airborne)
			{
				UpdateMovementAirborne(kb, gameTime);
			}
		}

		private void UpdateMovementSurfaced(KeyboardConfiguration kb, GameTime gameTime)
		{
			if (kb.IsKeyDown(ActionKey.Jump))
			{
				JumpAnimation();
				Speed.Y = MagicNumbers.MarianJumpSpeed;
				lastJumpStarted = gameTime.TotalGameTime;
			}
			else if (kb.IsKeyDown(ActionKey.Right))
			{
				Direction = Direction.Right;
			}
			else if (kb.IsKeyDown(ActionKey.Left))
			{
				Direction = Direction.Left;
			}
			else
			{
				Direction = Direction.None;
			}
		}

		private void UpdateMovementAirborne(KeyboardConfiguration kb, GameTime gameTime)
		{
			if (kb.IsKeyDown(ActionKey.Jump))
			{
				if (lastJumpStarted == TimeSpan.Zero) // sanity.
				{
					return;
				}
				if (gameTime.TotalGameTime - lastJumpStarted < MagicNumbers.MarianJumpWindow)
				{
					Speed.Y += MagicNumbers.MarianJumpSpeed;
				}
			}

			if (kb.IsKeyDown(ActionKey.Right))
			{
				Direction = Direction.Right;
			}
			else if (kb.IsKeyDown(ActionKey.Left))
			{
				Direction = Direction.Left;
			}
			else
			{
				Direction = Direction.None;
			}
		}
	}
}