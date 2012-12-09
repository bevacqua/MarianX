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

		public FloatRectangle Extended(Vector2 vector)
		{
			FloatRectangle extended = new FloatRectangle(this);
			var x = vector.X;
			if (x < 0)
			{
				extended.X -= x;
			}
			extended.Width += x;

			var y = vector.Y;
			if (y < 0)
			{
				extended.Y -= y;
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

		public static FloatRectangle Intersect(FloatRectangle value1, FloatRectangle value2)
		{
			float num1 = value1.X + value1.Width;
			float num2 = value2.X + value2.Width;
			float num3 = value1.Y + value1.Height;
			float num4 = value2.Y + value2.Height;
			float num5 = value1.X > value2.X ? value1.X : value2.X;
			float num6 = value1.Y > value2.Y ? value1.Y : value2.Y;
			float num7 = num1 < num2 ? num1 : num2;
			float num8 = num3 < num4 ? num3 : num4;

			FloatRectangle rectangle;

			if (num7 > num5 && num8 > num6)
			{
				rectangle.X = num5;
				rectangle.Y = num6;
				rectangle.Width = num7 - num5;
				rectangle.Height = num8 - num6;
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
	}
}