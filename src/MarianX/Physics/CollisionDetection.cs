using System.Collections.Generic;
using MarianX.Diagnostics;
using MarianX.Enum;
using MarianX.World.Physics;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Physics
{
	public class CollisionDetection
	{
		public virtual MoveResult CanMove(FloatRectangle bounds, Vector2 interpolation, DetectionType detectionType)
		{
			MoveResult flags = MoveResult.None;

			FloatRectangle xTarget = bounds.Displaced(interpolation * Direction.Right);
			FloatRectangle yTarget = bounds.Displaced(interpolation * Direction.Down);
			FloatRectangle target = bounds.Displaced(interpolation);

			FitResult fitX = CanFitInMatrix(xTarget);
			FitResult fitY = CanFitInMatrix(yTarget);
			FitResult fit = CanFitInMatrix(target);

			try
			{
				if (detectionType == DetectionType.Collision) // senseless to test this when retracing.
				{
					flags |= CheckInboundMapLimits(bounds, interpolation);
				}

				if (fitX == FitResult.Mortal || fitY == FitResult.Mortal)
				{
					flags |= MoveResult.Died;
					return flags;
				}

				if (fitX == FitResult.Solid && fitY == FitResult.Solid)
				{
					flags = MoveResult.Blocked;
					return flags;
				}

				if (fit == FitResult.Mortal)
				{
					flags |= MoveResult.Died;
					return flags;
				}
				if (fit == FitResult.Ok)
				{
					flags |= MoveResult.X | MoveResult.Y;
					return flags;
				}

				flags |= AdjustFlags(interpolation, fitX, fitY);

				return flags;
			}
			finally
			{
				if (detectionType == DetectionType.Collision)
				{
					Diagnostic.Write("fit ", fit);
					Diagnostic.Write("fitX", fitX);
					Diagnostic.Write("fitY", fitY);
					Diagnostic.Write("rslt", flags);
				}
				else if (detectionType == DetectionType.Retrace)
				{
					Diagnostic.Write("rFt ", fit);
					Diagnostic.Write("rFtX", fitX);
					Diagnostic.Write("rFtY", fitY);
					Diagnostic.Write("RSLT", flags);
				}
			}
		}

		private MoveResult CheckInboundMapLimits(FloatRectangle bounds, Vector2 interpolation)
		{
			if (interpolation.X < 0 && bounds.X + interpolation.X < Tile.Width)
			{
				return MoveResult.BlockedOnNegativeX | MoveResult.FlattenXSpeed;
			}

			if (interpolation.Y < 0 && bounds.Y + interpolation.Y < Tile.Height)
			{
				return MoveResult.BlockedOnNegativeY | MoveResult.FlattenYSpeed;
			}

			return MoveResult.None;
		}

		private MoveResult AdjustFlags(Vector2 interpolation, FitResult fitX, FitResult fitY)
		{
			MoveResult adjustments = MoveResult.None;

			if (fitX == FitResult.Ok)
			{
				adjustments |= MoveResult.X;
			}
			else
			{
				if (interpolation.X > 0)
				{
					adjustments |= MoveResult.BlockedOnPositiveX;
				}
				else if (interpolation.Y < 0)
				{
					adjustments |= MoveResult.BlockedOnNegativeX;
				}
			}

			if (fitY == FitResult.Ok)
			{
				adjustments |= MoveResult.Y;
			}
			else
			{
				if (interpolation.Y > 0)
				{
					adjustments |= MoveResult.BlockedOnPositiveY;
				}
				else if (interpolation.Y < 0)
				{
					adjustments |= MoveResult.BlockedOnNegativeY;
				}
			}

			return adjustments;
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