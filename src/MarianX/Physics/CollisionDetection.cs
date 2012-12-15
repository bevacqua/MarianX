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

			bool? canMoveOnAxisX = CanFitInMatrix(xTarget);
			bool? canMoveOnAxisY = CanFitInMatrix(yTarget);
			
			if (!canMoveOnAxisX.HasValue || !canMoveOnAxisY.HasValue)
			{
				return MoveResult.Died;
			}
			if (!canMoveOnAxisX.Value && !canMoveOnAxisY.Value)
			{
				return MoveResult.Blocked;
			}
			FloatRectangle target = bounds.Extended(interpolation);

			bool? canMove = CanFitInMatrix(target);
			if (!canMove.HasValue)
			{
				return MoveResult.Died;
			}
			if (canMove.Value)
			{
				return MoveResult.X | MoveResult.Y;
			}
			else if (canMoveOnAxisX.Value)
			{
				return MoveResult.X;
			}
			else if (canMoveOnAxisY.Value)
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

		protected virtual bool? CanFitInMatrix(FloatRectangle bounds)
		{
			IList<Tile> intersection = GetIntersection(bounds);

			foreach (Tile tile in intersection)
			{
				if (tile.Deathly)
				{
					return null; // died.
				}
				if (tile.Impassable)
				{
					return false;
				}
			}

			return true;
		}
	}
}