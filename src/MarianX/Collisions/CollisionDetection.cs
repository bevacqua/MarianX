using System.Collections.Generic;
using MarianX.Contents;
using MarianX.Enum;
using MarianX.Extensions;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class CollisionDetection
	{
		public MoveResult CanMove(Rectangle bounds, Vector2 interpolation)
		{
			Rectangle xTarget = bounds.Offset(interpolation * Direction.Right);
			Rectangle yTarget = bounds.Offset(interpolation * Direction.Down);

			bool canMoveOnAxisX = CanFitInMatrix(xTarget);
			bool canMoveOnAxisY = CanFitInMatrix(yTarget);
			if (!canMoveOnAxisX && !canMoveOnAxisY)
			{
				return MoveResult.Blocked;
			}
			Rectangle target = bounds.Offset(interpolation);

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

		private bool CanFitInMatrix(Rectangle bounds)
		{
			TileMatrix matrix = TileMatrix.Instance;
			IList<Tile> intersection = matrix.Intersect(bounds);

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