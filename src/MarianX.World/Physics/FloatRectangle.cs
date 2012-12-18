using Microsoft.Xna.Framework;

namespace MarianX.World.Physics
{
	public struct FloatRectangle
	{
		public float X;
		public float Y;
		public float Width;
		public float Height;

		public float Left { get { return X; } }
		public float Top { get { return Y; } }
		public float Right { get { return X + Width; } }
		public float Bottom { get { return Y + Height; } }

		public FloatRectangle(float x, float y, float width, float height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		private FloatRectangle(FloatRectangle source)
			: this(source.X, source.Y, source.Width, source.Height)
		{
		}

		/// <summary>
		/// For when you REALLY need a regular rectangle.
		/// </summary>
		public static explicit operator Rectangle(FloatRectangle r)
		{
			return new Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
		}

		public static implicit operator FloatRectangle(Rectangle r)
		{
			return new FloatRectangle(r.X, r.Y, r.Width, r.Height);
		}

		public FloatRectangle Displaced(Vector2 vector)
		{
			FloatRectangle extended = new FloatRectangle(this);
			var x = vector.X;
			if (x < 0)
			{
				extended.X += x;
				x = -x;
			}
			extended.Width += x;

			var y = vector.Y;
			if (y < 0)
			{
				extended.Y += y;
				y = -y;
			}
			extended.Height += y;

			return extended;
		}

		public bool Intersects(Rectangle value)
		{
			if (value.X < X + Width && X < value.X + value.Width && value.Y < Y + Height)
			{
				return Y < value.Y + value.Height;
			}
			return false;
		}

		public static FloatRectangle Intersect(FloatRectangle source, FloatRectangle target)
		{
			float sourceOffsetX = source.X + source.Width;
			float targetOffsetX = target.X + target.Width;
			float sourceOffsetY = source.Y + source.Height;
			float targetOffsetY = target.Y + target.Height;
			float highestX = source.X > target.X ? source.X : target.X;
			float highestY = source.Y > target.Y ? source.Y : target.Y;
			float highestOffsetX = sourceOffsetX < targetOffsetX ? sourceOffsetX : targetOffsetX;
			float highestOffsetY = sourceOffsetY < targetOffsetY ? sourceOffsetY : targetOffsetY;

			FloatRectangle rectangle;

			if (highestOffsetX > highestX && highestOffsetY > highestY)
			{
				rectangle.X = highestX;
				rectangle.Y = highestY;
				rectangle.Width = highestOffsetX - highestX;
				rectangle.Height = highestOffsetY - highestY;
			}
			else
			{
				rectangle.X = 0;
				rectangle.Y = 0;
				rectangle.Width = 0;
				rectangle.Height = 0;
			}
			return rectangle;
		}

		public override string ToString()
		{
			return string.Format("X:{0}, Y:{1}, Width:{2}, Height:{3}", X, Y, Width, Height);
		}
	}
}