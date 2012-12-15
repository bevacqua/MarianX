using System;
using MarianX.Core;
using MarianX.Enum;
using MarianX.Events;
using MarianX.Physics;
using MarianX.Sprites;
using MarianX.World.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Mobiles
{
	public class Marian : PlayerMobile
	{
		private const string AssetName = "Mobiles/marian";

		private TimeSpan? lastJumpStarted;
		private Vector2? jumpStartPosition;

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
			}
		}

		public Marian()
			: base(AssetName)
		{
			BoundingBox = new MarianBoundingBox();
		}

		public override void Initialize()
		{
			base.Initialize();
			SetStartPosition();
			AnimationComplete += Marian_AnimationComplete;
		}

		private void SetStartPosition()
		{
			State = HitBoxState.Airborne;
			Speed = Vector2.Zero;
			Direction = Direction.None;
			Position = new Vector2(MagicNumbers.StartX, MagicNumbers.StartY);
			IdleEffects();
		}

		private void SetRestartPosition()
		{
			SetStartPosition();
			Flash();
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

			if (result == MoveResult.Died)
			{
				State = HitBoxState.Dead;
				DeathEffects();
			}
			else if (wasAirborne)
			{
				if (jumpStartPosition.HasValue && Position.Y > jumpStartPosition.Value.Y)
				{
					jumpStartPosition = null; // avoid repetition.
					FallEffects();
				}
				if (State == HitBoxState.Surfaced)
				{
					Direction = Direction.None;
					IdleEffects();
				}
			}
			return result;
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
				JumpEffects();
				Speed.Y = MagicNumbers.JumpSpeed;
				lastJumpStarted = gameTime.TotalGameTime;
				jumpStartPosition = Position + MagicNumbers.FallEffect;
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
				if (!lastJumpStarted.HasValue) // sanity.
				{
					return;
				}
				if (gameTime.TotalGameTime - lastJumpStarted.Value < MagicNumbers.JumpWindow)
				{
					Speed.Y = MagicNumbers.JumpSpeed / 2;
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

		private void Marian_AnimationComplete(object sender, AnimationCompleteArgs args)
		{
			if (State == HitBoxState.Dead)
			{
				SetRestartPosition();
			}
		}
	}
}