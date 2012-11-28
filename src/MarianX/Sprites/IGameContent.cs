using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public interface IGameContent
	{
		void Initialize();
		void Load(ContentManager content);
		void Update(GameTime gameTime);
		void Draw(GameTime gameTime, SpriteBatch spriteBatch);
		void Unload();
	}
}