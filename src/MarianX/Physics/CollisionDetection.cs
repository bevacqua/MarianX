using System.Collections.Generic;
using MarianX.Enum;
using MarianX.World.Physics;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class CollisionDetection
	{
		public MoveResult CanMove(FloatRectangle bounds, Vector2 interpolation)
		{
			FloatRectangle xTarget = bounds.Extended(interpolation * Direction.Right);
			FloatRectangle yTarget = bounds.Extended(interpolation * Direction.Down);

			bool canMoveOnAxisX = CanFitInMatrix(xTarget);
			bool canMoveOnAxisY = CanFitInMatrix(yTarget);
			if (!canMoveOnAxisX && !canMoveOnAxisY)
			{
				return MoveResult.Blocked;
			}
			FloatRectangle target = bounds.Extended(interpolation);

			bool canMove = CanFitInMatrix(target);
			if (canMove)
			{
				return MoveResult.X | MoveResult.Y;
			}
			else if (canMoveOnAxisX)
			{
				return MoveResult.X;
			}
			else if (canMoveOnAxisY)
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

		protected virtual bool CanFitInMatrix(FloatRectangle bounds)
		{
			IList<Tile> intersection = GetIntersection(bounds);

			foreach (Tile tile in intersection)
			{
				if (tile.Impassable)
				{
					return false;
				}
			}

			return true;
		}
	}
}