using System.Collections.Generic;
using MarianX.Enum;
using MarianX.World.Physics;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class CollisionDetection
	{
		public virtual MoveResult CanMove(FloatRectangle bounds, Vector2 interpolation)
		{
			if (bounds.X - interpolation.X < Tile.Width)
			{
				return MoveResult.Blocked;
			}

			if (bounds.Y - interpolation.Y < Tile.Height)
			{
				return MoveResult.Blocked;
			}

			FloatRectangle xTarget = bounds.Extended(interpolation * Direction.Right);
			FloatRectangle yTarget = bounds.Extended(interpolation * Direction.Down);

			FitResult fitX = CanFitInMatrix(xTarget);
			FitResult fitY = CanFitInMatrix(yTarget);

			if (fitX == FitResult.Mortal || fitY == FitResult.Mortal)
			{
				return MoveResult.Died;
			}
			if (fitX == FitResult.Solid && fitY == FitResult.Solid)
			{
				return MoveResult.Blocked;
			}
			FloatRectangle target = bounds.Extended(interpolation);

			var fit = CanFitInMatrix(target);
			if (fit == FitResult.Mortal)
			{
				return MoveResult.Died;
			}
			if (fit == FitResult.Ok)
			{
				return MoveResult.X | MoveResult.Y;
			}
			else if (fitX == FitResult.Ok)
			{
				return MoveResult.X;
			}
			else if (fitY == FitResult.Ok)
			{
				return MoveResult.Y;
			}

			return MoveResult.Blocked;
		}

		protected virtual IList<Tile> GetIntersection(FloatRectangle bounds)
		{
			TileMatrix matrix = TileMatrix.Instance;
			IList<Tile> intersection = matrix.Intersect(bounds);
			return intersection;
		}

		protected virtual FitResult CanFitInMatrix(FloatRectangle bounds)
		{
			IList<Tile> intersection = GetIntersection(bounds);

			foreach (Tile tile in intersection)
			{
				if (tile.Deathly)
				{
					return FitResult.Mortal; // died.
				}
				if (tile.Impassable)
				{
					return FitResult.Solid;
				}
			}

			return FitResult.Ok;
		}
	}
}