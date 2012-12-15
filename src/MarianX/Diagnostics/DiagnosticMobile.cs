using MarianX.Contents;
using MarianX.Core;
using MarianX.Mobiles;
using MarianX.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarianX.Diagnostics
{
	public class DiagnosticMobile : Mobile
	{
		public DiagnosticMobile(string assetName, SpriteSheetSettings settings)
			: base(assetName, settings)
		{
		}

		public override void UpdateOutput(GameTime gameTime)
		{
			base.UpdateOutput(gameTime);
			Diagnostics.Write("mpos", Position);
			Diagnostics.Write("stat", State);
			Diagnostics.Write(" dir", Direction.Vector);
			Diagnostics.Write(" spd", Speed);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (Config.Diagnostic)
			{
				DrawDiagnosticHitBox(spriteBatch);
				return;
			}
			base.Draw(gameTime, spriteBatch);
		}

		private void DrawDiagnosticHitBox(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			Square hitBox = new Square
			{
				Alpha = 0.8f,
				Color = Color.OrangeRed,
				Bounds = (Rectangle)BoundingBox.Bounds
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