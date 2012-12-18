using Microsoft.Xna.Framework;

namespace MarianX.World.Extensions
{
	public interface ILevel
	{
		int Level { get; }
		Vector2 Start { get; }
	}
}