using MarianX.Map.Builder;

namespace MarianX.Map
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var builder = new FullMapBuilder();
			builder.FullBuild();
		}
	}
}
