using System.Collections.Generic;
using MarianX.Extensions;
using MarianX.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Collisions
{
	public class CollisionDetection
	{
		public event OnTopOfSurface OnTopOfSurface;

		public bool CanMove(AxisAlignedBoundingBox boundingBox, Vector2 interpolation)
		{
			Rectangle bounds = boundingBox.Bounds;
			Rectangle to = bounds.Offset(interpolation);

			bool canMove = CanFitInMatrix(to);
			if (!canMove && interpolation.Y > 0)
			{
				OnTopOfSurfaceArgs args = new OnTopOfSurfaceArgs
				{
					BoundingBox = boundingBox,
					Interpolation = interpolation
				};
				RaiseOnTopOfSurface(args);
			}
			return canMove;
		}

		private void RaiseOnTopOfSurface(OnTopOfSurfaceArgs args)
		{
			if (OnTopOfSurface != null)
			{
				OnTopOfSurface(this, args);
			}
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

	public delegate void OnTopOfSurface(object sender, OnTopOfSurfaceArgs args);

	public class OnTopOfSurfaceArgs
	{
		public AxisAlignedBoundingBox BoundingBox { get; set; }
		public Vector2 Interpolation { get; set; }
	}
}