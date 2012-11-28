using MarianX.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class Sprite : Content, IGameContent
	{
		public Vector2 Position;
		public Vector2 Direction;
		public Vector2 Speed;
		
		public Color Tint { get; set; }

		public Sprite(string assetName)
			:base(assetName)
		{
			Position = Vector2.Zero;
			Tint = Color.White;
		}

		public virtual void Initialize()
		{
		}
		
		public virtual void Update(GameTime gameTime)
		{
			float time = gameTime.GetElapsedSeconds();
			Position += Direction * Speed * time;
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Texture.Bounds, Tint, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
		}

		public virtual void Unload()
		{
		}
	}
}