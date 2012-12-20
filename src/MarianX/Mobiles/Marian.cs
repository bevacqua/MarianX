using System;
using MarianX.Core;
using MarianX.Enum;
using MarianX.Events;
using MarianX.Physics;
using MarianX.Sprites;
using MarianX.World.Configuration;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Mobiles
{
	public class Marian : PlayerMobile
	{
		private const string AssetName = "Mobiles/marian";

		private TimeSpan? lastJumpStarted;
		private Vector2? jumpStartPosition;
		private HitBoxState state;

		public override HitBoxState State
		{
			get
			{
				return state;
			}
			set
			{
				if (State == HitBoxState.Dead) // ignore state changes when dead.
				{
					return;
				}
				state = value;
			}
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
			}
		}

		public Marian()
			: base(AssetName)
		{
			BoundingBox = new MarianBoundingBox();
			AnimationComplete += Marian_AnimationComplete;
		}

		public override void Initialize()
		{
			base.Initialize();
			SetStartPosition();
		}

		private void SetStartPosition()
		{
			state = HitBoxState.Airborne;
			Speed = Vector2.Zero;
			Direction = Direction.None;
			Position = TileMatrix.Instance.StartPosition;
			IdleEffects();
		}

		private void SetRestartPosition()
		{
			SetStartPosition();
			Flash();
		}
		
		private KeyboardState keyboardState;

		public override void UpdateInput(GameTime gameTime)
		{
			keyboardState = Keyboard.GetState();
			base.UpdateInput(gameTime);
		}

		public override void UpdateOutput(GameTime gameTime)
		{
			UpdateMovement(gameTime);
			base.UpdateOutput(gameTime);
		}

		private KeyboardState oldState;

		private void UpdateMovement(GameTime gameTime)
		{
			var kb = new KeyboardConfiguration(keyboardState);

			if (Config.Diagnostic)
			{
				if (kb.IsKeyPressed(ActionKey.Suicide, oldState))
				{
					Die();
				}
				oldState = keyboardState;
			}

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

		protected override MoveResult UpdatePosition(GameTime gameTime, Vector2 interpolation)
		{
			var wasAirborne = State == HitBoxState.Airborne;

			MoveResult result = base.UpdatePosition(gameTime, interpolation);

			if (!result.HasFlag(MoveResult.Y) || result.HasFlag(MoveResult.FlattenYSpeed)) // sanity.
			{
				lastJumpStarted = null;
			}

			if (result == MoveResult.Died)
			{
				Die();
			}
			else if (result == MoveResult.LevelCompleted)
			{
				CompleteLevel();
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

		private void CompleteLevel()
		{
			if (State != HitBoxState.LevelCompleteAnimation)
			{
				State = HitBoxState.LevelCompleteAnimation;
				LevelCompleteEffects();
			}
		}

		public void Die()
		{
			if (State != HitBoxState.Dead) // sanity.
			{
				State = HitBoxState.Dead;
				DeathEffects();
			}
		}

		private void Marian_AnimationComplete(object sender, AnimationCompleteArgs args)
		{
			if (State == HitBoxState.Dead)
			{
				SetRestartPosition();
			}
			else if (State == HitBoxState.LevelCompleteAnimation)
			{
				GameCore.Instance.AdvanceLevel();
			}
		}
	}
}