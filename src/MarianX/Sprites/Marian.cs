using MarianX.Collisions;
using MarianX.Configuration;
using MarianX.Contents;
using MarianX.Core;
using MarianX.Enum;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarianX.Sprites
{
	public class Marian : DiagnosticMobile
	{
		private const string AssetName = "marian";
		private const int FrameWidth = MagicNumbers.MarianFrameWidth;
		private const int FrameHeight = MagicNumbers.MarianFrameHeight;

		private readonly FrameSet idleRight = new FrameSet { Row = 0, Frames = 1 };
		private readonly FrameSet idleLeft = new FrameSet { Row = 0, Frames = 1, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet walkRight = new FrameSet { Row = 1, Frames = 3 };
		private readonly FrameSet walkLeft = new FrameSet { Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet jumpRight = new FrameSet { Row = 2, Frames = 4, Loop = false };
		private readonly FrameSet jumpLeft = new FrameSet { Row = 2, Frames = 4, Loop = false, Effects = SpriteEffects.FlipHorizontally };
		private readonly FrameSet steerRight = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false };
		private readonly FrameSet steerLeft = new FrameSet { Row = 2, Frames = 1, Start = 3, Loop = false, Effects = SpriteEffects.FlipHorizontally };

		private static readonly SpriteSheetSettings settings;

		static Marian()
		{
			settings = new SpriteSheetSettings
			{
				Width = FrameWidth,
				Height = FrameHeight
			};
		}

		private readonly Viewport viewport;
		private bool lastFacedLeft;

		public Marian(Viewport viewport)
			: base(AssetName, settings)
		{
			this.viewport = viewport;

			BoundingBox = new MarianBoundingBox();
		}

		public override void Initialize()
		{
			SetFrameSet(idleRight);

			base.Initialize();

			float startY = viewport.Height - FrameHeight - Tile.Height * 2;
			Position = new Vector2(MagicNumbers.StartX, startY);
		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();
			UpdateMovement(keyboardState);

			base.Update(gameTime);
		}

		protected override MoveResult UpdatePosition(Vector2 interpolation)
		{
			var wasAirborne = State == HitBoxState.Airborne;

			MoveResult result = base.UpdatePosition(interpolation);

			if (wasAirborne && !result.HasFlag(MoveResult.Y))
			{
				SetFrameSet(lastFacedLeft ? idleLeft : idleRight);
				Direction = Direction.None;
			}
			return result;
		}

		private void UpdateMovement(KeyboardState keyboardState)
		{
			Direction previous = Direction;

			var kb = new KeyboardConfiguration(keyboardState);

			if (State == HitBoxState.Surfaced)
			{
				UpdateMovementSurfaced(kb);
			}
			else if (State == HitBoxState.Airborne)
			{
				UpdateMovementAirborne(kb);
			}

			if (Direction != previous)
			{
				Speed = Vector2.Zero;
			}
		}

		private void UpdateMovementSurfaced(KeyboardConfiguration kb)
		{
			Direction previous = Direction;

			if (kb.IsShortcutDown(Action.Jump))
			{
				SetFrameSet(lastFacedLeft ? jumpLeft : jumpRight);
				Speed.Y = MagicNumbers.MarianJumpSpeed;
			}

			if (kb.IsShortcutDown(Action.Right))
			{
				if (previous != Direction.Right)
				{
					SetFrameSet(walkRight);
					Direction = Direction.Right;
					lastFacedLeft = false;
				}
			}
			else if (kb.IsShortcutDown(Action.Left))
			{
				if (previous != Direction.Left)
				{
					SetFrameSet(walkLeft);
					Direction = Direction.Left;
					lastFacedLeft = true;
				}
			}
			else if (previous != Direction.None)
			{
				SetFrameSet(lastFacedLeft ? idleLeft : idleRight);
				Direction = Direction.None;
			}
		}

		private void UpdateMovementAirborne(KeyboardConfiguration kb)
		{
			Direction previous = Direction;

			if (kb.IsShortcutDown(Action.Right))
			{
				if (previous != Direction.Right)
				{
					SetFrameSet(steerRight);
					Direction = Direction.Right;
					lastFacedLeft = false;
				}
			}
			else if (kb.IsShortcutDown(Action.Left))
			{
				if (previous != Direction.Left)
				{
					SetFrameSet(steerLeft);
					Direction = Direction.Left;
					lastFacedLeft = true;
				}
			}
			else if (previous != Direction.None)
			{
				SetFrameSet(steerRight);
				Direction = Direction.None;
			}
		}
	}
}