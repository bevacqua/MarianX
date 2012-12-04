using MarianX.Collisions;
using MarianX.Contents;
using MarianX.Input;
using MarianX.Interface;
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

		private const int Idle = 0;
		private const int WalkRight = 1;
		private const int WalkLeft = 2;
		private const int JumpRight = 3;
		private const int JumpLeft = 4;

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
					new FrameSet {Row = 1, Frames = 3},
					new FrameSet {Row = 1, Frames = 3, Effects = SpriteEffects.FlipHorizontally},
					new FrameSet {Row = 2, Frames = 4, Loop = false},
					new FrameSet {Row = 2, Frames = 4, Loop = false, Effects = SpriteEffects.FlipHorizontally},
				}
			};
		}

		private readonly Viewport viewport;

		public PlayerState State { get; protected set; }

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

		private void UpdateMovement(KeyboardState keyboardState)
		{
			Direction previous = Direction;

			var kb = new KeyboardConfiguration(keyboardState);
			if (State == PlayerState.Surfaced)
			{
				UpdateMovementSurfaced(kb);
				// TODO: jump, states, movement changes to support?
			}
			else if (State == PlayerState.Airborne)
			{
				UpdateMovementAirborne(kb);
			}

			if (kb.IsShortcutDown(Action.Right))
			{
				if (previous != Direction.Right)
				{
					SetFrameSet(WalkRight);
					Direction = Direction.Right;
				}
			}
			else if (kb.IsShortcutDown(Action.Left))
			{
				if (previous != Direction.Left)
				{
					SetFrameSet(WalkLeft);
					Direction = Direction.Left;
				}
			}
			else if (previous != Direction.None)
			{
				SetFrameSet(Idle);
				Direction = Direction.None;
			}

			if (Direction != previous)
			{
				Speed = Vector2.Zero;
			}
		}

		private void UpdateMovementSurfaced(KeyboardConfiguration kb)
		{
			if (kb.IsShortcutDown(Action.Jump))
			{
				SetFrameSet(JumpRight);
				Speed.Y = 30; // Speed should lower with accel until == 0? 
				State = PlayerState.Airborne; // TODO: set surfaced again when colliding on Y (on the top of a surface).
				// and set it back to airborne when not colliding on a surface. move both to CD.
			}
		}

		private void UpdateMovementAirborne(KeyboardConfiguration kb)
		{
		}
	}
}