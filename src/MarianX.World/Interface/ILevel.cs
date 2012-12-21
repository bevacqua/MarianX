using Microsoft.Xna.Framework;

namespace MarianX.World.Interface
{
	public interface ILevel
	{
		int Level { get; }
		Vector2 Start { get; }

		int ScreenTop { get; }
		int ScreenLeft { get; }
		int ScreenBottom { get; }
		int ScreenRight { get; }
	}
}