using MarianX.Collisions;
using MarianX.Configuration;
using MarianX.Contents;
using MarianX.Core;
using MarianX.Enum;
using MarianX.Platform;
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

		private const int IdleRight = 0;
		private const int IdleLeft = 1;
		private const int WalkRight = 2;
		private const int WalkLeft = 3;
		private const int JumpRight = 4;
		private const int JumpLeft = 5;
		private const int SteerRight = 6;
		private const int SteerLeft = 7;

		private static readonly SpriteSheetSettings settings;

		static Marian()
		{
			settings = new SpriteSheetSettings
			{
				Width = FrameWidth,
				Height = FrameHeight,
				FrameSets = new[]
				{
					new FrameSet {Row = 0, Frames = 1},
					new FrameSet {Row = 0, Frames = 1, Effects = SpriteEffects.FlipHorizontally},
					new FrameSet {Row = 1, Frames = 3},
					new FrameSet {Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally},
					new FrameSet {Row = 2, Frames = 4, Loop = false},
					new FrameSet {Row = 2, Frames = 4, Loop = false, Effects = SpriteEffects.FlipHorizontally},
					new FrameSet {Row = 2, Frames = 1, Start = 3, Loop = false},
					new FrameSet {Row = 2, Frames = 1, Start = 3, Loop = false, Effects = SpriteEffects.FlipHorizontally},
				}
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
				SetFrameSet(lastFacedLeft ? IdleLeft : IdleRight);
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
				SetFrameSet(lastFacedLeft ? JumpLeft : JumpRight);
				Speed.Y = 30; // Speed should lower with accel until == 0?
			}

			if (kb.IsShortcutDown(Action.Right))
			{
				if (previous != Direction.Right)
				{
					SetFrameSet(WalkRight);
					Direction = Direction.Right;
					lastFacedLeft = false;
				}
			}
			else if (kb.IsShortcutDown(Action.Left))
			{
				if (previous != Direction.Left)
				{
					SetFrameSet(WalkLeft);
					Direction = Direction.Left;
					lastFacedLeft = true;
				}
			}
			else if (previous != Direction.None)
			{
				SetFrameSet(lastFacedLeft ? IdleLeft : IdleRight);
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
					SetFrameSet(SteerRight);
					Direction = Direction.Right;
					lastFacedLeft = false;
				}
			}
			else if (kb.IsShortcutDown(Action.Left))
			{
				if (previous != Direction.Left)
				{
					SetFrameSet(SteerLeft);
					Direction = Direction.Left;
					lastFacedLeft = true;
				}
			}
			else if (previous != Direction.None)
			{
				SetFrameSet(SteerRight);
				Direction = Direction.None;
			}
		}
	}
}