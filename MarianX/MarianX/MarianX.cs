using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarianX
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MarianX : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;

		private ScrollingBackground background;

		public MarianX()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			IList<string> backgroundAssets = new[]
			{
				"Backgrounds/congress",
				"Backgrounds/eiffel",
				"Backgrounds/island",
				"Backgrounds/louvre",
				"Backgrounds/ny",
				"Backgrounds/opera",
				"Backgrounds/panama"
			};
			background = new ScrollingBackground(backgroundAssets);
			background.Initialize();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

			background.Load(Content);

			base.LoadContent();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}

			background.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			background.Draw(spriteBatch);

			base.Draw(gameTime);
		}
	}

	public class Sprite
	{
		private readonly string assetName;

		private Texture2D texture;
		private float scale;

		public Texture2D Texture
		{
			get { return texture; }
			private set
			{
				texture = value;
				InvalidateTextureScale();
			}
		}

		public float Scale
		{
			get { return scale; }
			private set
			{
				scale = value;
				InvalidateTextureScale();
			}
		}

		public Rectangle TextureSize { get; private set; }
		public Rectangle ActualSize { get; private set; }

		private void InvalidateTextureScale()
		{
			if (texture == null)
			{
				TextureSize = Rectangle.Empty;
				ActualSize = Rectangle.Empty;
			}
			else
			{
				TextureSize = Texture.Bounds;
				ActualSize = new Rectangle(0, 0, (int)(TextureSize.Width * Scale), (int)(TextureSize.Height * Scale));
			}
		}

		public Vector2 Position;
		public Color Tint { get; set; }

		public Sprite(string assetName)
		{
			this.assetName = assetName;

			Scale = 1f;
			Position = Vector2.Zero;
			Tint = Color.White;
		}

		public void Load(ContentManager content)
		{
			Texture = content.Load<Texture2D>(assetName);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position, Texture.Bounds, Tint, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
		}
	}

	public class ScrollingBackground
	{
		private readonly IList<string> assetNames;
		private readonly IList<Sprite> sprites;

		public ScrollingBackground(IList<string> assetNames)
		{
			this.assetNames = assetNames;
			sprites = new List<Sprite>();
		}

		public void Initialize()
		{
			foreach (string assetName in assetNames)
			{
				Sprite sprite = new Sprite(assetName);
				sprites.Add(sprite);
			}
		}

		public void Load(ContentManager content)
		{
			Sprite previous = null;

			foreach (Sprite sprite in sprites)
			{
				sprite.Load(content);

				if (previous != null)
				{
					sprite.Position = new Vector2(previous.Position.X + previous.ActualSize.Width, previous.Position.Y);
				}
				previous = sprite;
			}
		}

		public void Update(GameTime gameTime)
		{
			Sprite previous = sprites.Last();
			Vector2 direction = new Vector2(-1, 0);
			Vector2 speed = new Vector2(160, 0);
			float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

			foreach (Sprite sprite in sprites)
			{
				if (sprite.Position.X < -sprite.ActualSize.Width)
				{
					sprite.Position.X = previous.Position.X + previous.ActualSize.Width;
				}

				sprite.Position += direction * speed * time;
				previous = sprite;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			foreach (Sprite sprite in sprites)
			{
				sprite.Draw(spriteBatch);
			}

			spriteBatch.End();
		}
	}
}
