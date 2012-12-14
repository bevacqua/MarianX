using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Interface
{
	public interface ISquareGrid
	{
		void Draw(SpriteBatch spriteBatch, Vector2? offset = null);
	}
}