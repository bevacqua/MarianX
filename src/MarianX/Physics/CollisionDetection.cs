using System.Collections.Generic;
using MarianX.Contents;
using MarianX.Enum;
using MarianX.World.Extensions;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class CollisionDetection
	{
		public MoveResult CanMove(Rectangle bounds, Vector2 interpolation)
		{
			Rectangle xTarget = bounds.Extend(interpolation * Direction.Right);
			Rectangle yTarget = bounds.Extend(interpolation * Direction.Down);

			bool canMoveOnAxisX = CanFitInMatrix(xTarget);
			bool canMoveOnAxisY = CanFitInMatrix(yTarget);
			if (!canMoveOnAxisX && !canMoveOnAxisY)
			{
				return MoveResult.Blocked;
			}
			Rectangle target = bounds.Extend(interpolation);

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

		protected virtual IList<Tile> GetIntersection(Rectangle bounds)
		{
			TileMatrix matrix = TileMatrix.Instance;
			IList<Tile> intersection = matrix.Intersect(bounds);
			return intersection;
		}

		protected virtual bool CanFitInMatrix(Rectangle bounds)
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