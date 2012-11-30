using MarianX.Extensions;
using MarianX.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class Sprite : Content, IGameContent
	{
		public virtual Vector2 Position { get; set; }
		public Vector2 Speed;
		public Direction Direction;

		public Color Tint { get; set; }

		public Sprite(string assetName)
			: base(assetName)
		{
			Speed = Vector2.Zero;
			Direction = Direction.None;
			Tint = Color.White;
		}

		public virtual void Initialize()
		{
			Position = Vector2.Zero;
		}

		public virtual void Update(GameTime gameTime)
		{
			float time = gameTime.GetElapsedSeconds();
			Vector2 interpolation = Direction * Speed * time;
			UpdatePosition(interpolation);
		}

		public virtual void UpdatePosition(Vector2 interpolation)
		{
			Position += interpolation;
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