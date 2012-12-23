using MarianX.Core;
using MarianX.Enum;
using MarianX.Interface;
using MarianX.Physics;
using MarianX.World.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Contents
{
	public class Sprite : Content, IGameContent
	{
		public Vector2 Speed;

		public virtual Vector2 Position { get; set; }
		public virtual Direction Direction { get; set; }

		public virtual Vector2 ScreenPosition { get; set; }

		public Color Tint { get; set; }
		public virtual float Tilt { get; set; }

		public Sprite(string assetName)
			: base(assetName)
		{
		}

		public virtual void Initialize()
		{
			Position = Vector2.Zero;
			Speed = Vector2.Zero;
			Direction = Direction.None;
			Tint = Color.White;
		}

		public virtual void UpdateInput(GameTime gameTime)
		{
		}

		public virtual void UpdateOutput(GameTime gameTime)
		{
			Vector2 interpolation = CalculateInterpolation(gameTime);
			UpdatePosition(gameTime, interpolation);
		}

		protected virtual Vector2 CalculateInterpolation(GameTime gameTime)
		{
			float time = gameTime.GetElapsedSeconds();
			Vector2 interpolation = Direction * Speed * time;
			return interpolation;
		}

		protected virtual MoveResult UpdatePosition(GameTime gameTime, Vector2 interpolation)
		{
			Position += interpolation;
			return MoveResult.None;
		}

		public void UpdateScreenPosition(Vector2 screenPosition)
		{
			ScreenPosition = screenPosition;
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, ScreenPosition, Texture.Bounds, Tint, Tilt, Vector2.Zero, Scale, SpriteEffects.None, 0);
		}

		public virtual void Unload()
		{
			GameCore.Instance.RemoveContent(this);
		}
	}
}