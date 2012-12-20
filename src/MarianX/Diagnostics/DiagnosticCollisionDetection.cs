using System;
using System.Collections.Generic;
using MarianX.Core;
using MarianX.Enum;
using MarianX.Interface;
using MarianX.Physics;
using MarianX.Sprites;
using MarianX.World.Physics;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Diagnostics
{
	public class DiagnosticCollisionDetection : StaticCollisionDetection, IGameContent
	{
		private SquareGrid movementGrid;
		private SquareGrid matchGrid;
		private SquareGrid intersectionGrid;

		protected override IList<Tile> GetIntersection(FloatRectangle bounds)
		{
			IList<Tile> tiles = base.GetIntersection(bounds);

			movementGrid = null;
			matchGrid = null;
			intersectionGrid = null;

			if (Config.Diagnostic)
			{
				HighlightMovement(bounds);
				HighlightMatchingTiles(tiles, bounds);
				HighlightIntersection(tiles, bounds);
			}
			return tiles;
		}

		private void HighlightMovement(FloatRectangle bounds)
		{
			movementGrid = new SquareGrid(new List<Square>
			{
				new Square
				{
					Alpha = 1f,
					Color = Color.Yellow,
					Bounds = (Rectangle)bounds
				}
			});
		}

		private void HighlightMatchingTiles(IEnumerable<Tile> tiles, FloatRectangle bounds)
		{
			IList<Square> squares = new List<Square>();

			foreach (Tile tile in tiles)
			{
				FloatRectangle intersection = (Rectangle)FloatRectangle.Intersect(bounds, tile.Bounds);
				bool intersects = intersection.Width > 0 && intersection.Height > 0;

				Square square = new Square
				{
					Alpha = 0.7f,
					Bounds = tile.Bounds,
					Color = intersects ? Color.MediumPurple : Color.MintCream
				};
				squares.Add(square);
			}

			matchGrid = new SquareGrid(squares);
		}

		private void HighlightIntersection(IEnumerable<Tile> tiles, FloatRectangle bounds)
		{
			IList<Square> squares = new List<Square>();

			foreach (Tile tile in tiles)
			{
				Rectangle intersection = (Rectangle)FloatRectangle.Intersect(bounds, tile.Bounds);
				bool intersects = intersection.Width > 0 && intersection.Height > 0;
				if (!intersects)
				{
					continue;
				}

				Square square = new Square
				{
					Alpha = 1f,
					Bounds = intersection,
					Color = Color.DarkRed
				};
				squares.Add(square);
			}

			intersectionGrid = new SquareGrid(squares);
		}

		public void Initialize()
		{
		}

		public void Load(ContentManager content)
		{
		}

		public void UpdateInput(GameTime gameTime)
		{
		}

		public void UpdateOutput(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			if (movementGrid != null)
			{
				movementGrid.Draw(spriteBatch);
			}
			if (matchGrid != null)
			{
				matchGrid.Draw(spriteBatch);
			}
			if (intersectionGrid != null)
			{
				intersectionGrid.Draw(spriteBatch);
			}

			spriteBatch.End();
		}

		public void Unload()
		{
		}

		public void UpdateScreenPosition(Vector2 screenPosition)
		{
		}

		public Vector2 Position
		{
			get { throw new NotImplementedException(); }
		}
	}
}