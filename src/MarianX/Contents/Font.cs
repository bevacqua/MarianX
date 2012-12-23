using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class Font : IGameContent
	{
		public static implicit operator SpriteFont(Font x)
		{
			return x.sprite;
		}

		private readonly string name;
		private SpriteFont sprite;

		public Font(string name)
		{
			this.name = name;
		}

		public void Initialize()
		{
		}

		public void Load(ContentManager content)
		{
			sprite = content.Load<SpriteFont>(name);
		}

		public void UpdateInput(GameTime gameTime)
		{
		}

		public void UpdateOutput(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
		}

		public void Unload()
		{
		}

		public void UpdateScreenPosition(Vector2 screenPosition)
		{
		}

		public Vector2 Position
		{
			get { return Vector2.Zero; }
		}

		public void Write(string message, Vector2 position, SpriteBatch spriteBatch, Color color)
		{
			SpriteFont spriteFont = this;
			spriteBatch.DrawString(spriteFont, message, position, color);
		}

		public Vector2 MeasureString(string message)
		{
			SpriteFont spriteFont = this;
			return spriteFont.MeasureString(message);
		}
	}
}