namespace MarianX.Physics
{
	public class BoundingBox2 : AxisAlignedBoundingBox
	{
		private readonly int width;
		private readonly int height;

		public override int HitBoxWidth
		{
			get { return width; }
		}

		public override int HitBoxHeight
		{
			get { return height; }
		}

		public BoundingBox2(int width, int height)
		{
			this.width = width;
			this.height = height;
		}
	}
}