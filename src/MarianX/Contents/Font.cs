using System;
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

		public void Update(GameTime gameTime)
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
			throw new NotImplementedException();
		}

		public Vector2 Position
		{
			get { throw new NotImplementedException(); }
		}
	}
}