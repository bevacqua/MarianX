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
			MoveResult flags = MoveResult.None;

			if (bounds.X - interpolation.X < Tile.Width)
			{
				flags |= MoveResult.BlockedOnX;
			}

			if (bounds.Y - interpolation.Y < Tile.Height)
			{
				flags |= MoveResult.BlockedOnY;
			}

			FloatRectangle xTarget = bounds.Extended(interpolation * Direction.Right);
			FloatRectangle yTarget = bounds.Extended(interpolation * Direction.Down);

			FitResult fitX = CanFitInMatrix(xTarget);
			FitResult fitY = CanFitInMatrix(yTarget);

			if (fitX == FitResult.Mortal || fitY == FitResult.Mortal)
			{
				return flags | MoveResult.Died;
			}
			if (fitX == FitResult.Solid && fitY == FitResult.Solid)
			{
				return MoveResult.Blocked;
			}
			FloatRectangle target = bounds.Extended(interpolation);

			var fit = CanFitInMatrix(target);
			if (fit == FitResult.Mortal)
			{
				return flags | MoveResult.Died;
			}
			if (fit == FitResult.Ok)
			{
				return flags | MoveResult.X | MoveResult.Y;
			}

			if (fitX == FitResult.Ok)
			{
				flags |= MoveResult.X;
			}
			else
			{
				flags |= MoveResult.BlockedOnX;
			}

			if (fitY == FitResult.Ok)
			{
				flags |= MoveResult.Y;
			}
			else
			{
				flags |= MoveResult.BlockedOnY;
			}

			return flags;
		}

		protected virtual IList<Tile> GetIntersection(FloatRectangle bounds)
		{
			TileMatrix matrix = TileMatrix.Instance;
			IList<Tile> intersection = matrix.Intersect(bounds);
			return intersection;
		}

		public virtual FitResult CanFitInMatrix(FloatRectangle bounds)
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