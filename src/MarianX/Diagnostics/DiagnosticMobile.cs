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

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			WriteDiagnostic("mpos", Position);
			Diagnostics.Write("stat", State.ToString());
			WriteDiagnostic(" dir", Direction.Vector);
			WriteDiagnostic(" acl", Direction.Acceleration);
			WriteDiagnostic(" spd", Speed);
		}

		private void WriteDiagnostic(string key, Vector2 vector)
		{
			Diagnostics.Write(key, "X:{0:+0000.00;-0000.00} Y:{1:+0000.00;-0000.00}", vector.X, vector.Y);
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