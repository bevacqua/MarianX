using Microsoft.Xna.Framework;

namespace MarianX.World.Extensions
{
	public static class GameTimeExtensions
	{
		public static float GetElapsedSeconds(this GameTime gameTime)
		{
			float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
			return seconds;
		}
	}
}