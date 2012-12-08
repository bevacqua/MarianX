using System.Collections.Generic;
using MarianX.Core;
using MarianX.Interface;
using MarianX.Sprites;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Physics
{
	public class DiagnosticCollisionDetection : CollisionDetection, IGameContent
	{
		protected override IList<Tile> GetIntersection(Rectangle bounds)
		{
			IList<Tile> tiles = base.GetIntersection(bounds);

			if (Config.Diagnostic)
			{
				HighlightMatchingTiles(tiles);
			}
			return tiles;
		}

		private SquareGrid grid;

		private void HighlightMatchingTiles(IEnumerable<Tile> tiles)
		{
			IList<Square> squares = new List<Square>();

			foreach (Tile tile in tiles)
			{
				Square square = new Square
				{
					Alpha = 0.4f,
					Bounds = tile.Bounds,
					Color = Color.SteelBlue
				};
				squares.Add(square);
			}
			grid = new SquareGrid(squares);
		}

		public void Initialize()
		{
		}

		public void Load(ContentManager content)
		{
		}

		public void Update(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			if (grid != null)
			{
				grid.Draw(spriteBatch);
			}

			spriteBatch.End();
		}

		public void Unload()
		{
		}
	}
}