using System.Configuration;
using MarianX.Contents;
using MarianX.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Sprites
{
	public class DiagnosticMobile : Mobile
	{
		public DiagnosticMobile(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);

			if (!Config.Diagnostic)
			{
				return;
			}
			spriteBatch.Begin();

			Square hitBox = new Square
			{
				Alpha = 0.4f,
				Color = Color.YellowGreen,
				Bounds = BoundingBox.Bounds
			};
			hitBox.Draw(spriteBatch, BoundingBox.Position);

			Square spriteBox = new Square
			{
				Alpha = 0.4f,
				Color = Color.OrangeRed,
				Bounds = new Rectangle(0, 0, ContentWidth, ContentHeight)
			};
			spriteBox.Draw(spriteBatch, Position);

			spriteBatch.End();
		}
	}
}