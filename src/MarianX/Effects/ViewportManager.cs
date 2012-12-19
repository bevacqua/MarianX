using System;
using System.Collections.Generic;
using MarianX.Core;
using MarianX.Events;
using MarianX.Interface;
using MarianX.Mobiles;
using MarianX.World.Configuration;
using MarianX.World.Platform;
using Microsoft.Xna.Framework;

namespace MarianX.Effects
{
	public class ViewportManager
	{
		private readonly IList<IGameContent> contents;
		private Vector2 position;
		private Rectangle bounds;

		public Vector2 Position
		{
			get { return position; }
		}

		public ViewportManager()
		{
			contents = new List<IGameContent>();
		}

		public void Initialize()
		{
			bounds = GameCore.Instance.GraphicsDevice.Viewport.Bounds;
			Point location = bounds.Location;

			position = new Vector2(location.X, location.Y);
		}

		public void Manage(IGameContent content)
		{
			contents.Add(content);
		}

		public void CharacterMove(Mobile sender, MoveArgs args)
		{
			UpdatePositionRelativeToOwner(sender);

			// update relative screen position for everything else.
			foreach (IGameContent content in contents)
			{
				UpdateScreenPosition(content);
			}
		}

		public void NpcMove(Mobile sender, MoveArgs args)
		{
			UpdateScreenPosition(sender);
		}

		private void UpdatePositionRelativeToOwner(Mobile sender)
		{
			Vector2 relative = sender.Position - position;

			int left = MagicNumbers.RelativeScreenLeft;
			if (relative.X < left)
			{
				float x = position.X + relative.X - left;
				position.X = Math.Max(x, 0);
			}

			int right = bounds.Width - MagicNumbers.RelativeScreenRight;
			if (relative.X > right)
			{
				float x = position.X + relative.X - right;
				int w = TileMatrix.Instance.Width - bounds.Width;
				position.X = Math.Min(x, w);
			}

			int top = MagicNumbers.RelativeScreenTop;
			if (relative.Y < top)
			{
				float y = position.Y + relative.Y - top;
				position.Y = Math.Max(y, 0);
			}

			int bottom = bounds.Height - MagicNumbers.RelativeScreenBottom;
			if (relative.Y > bottom)
			{
				float y = position.Y + relative.Y - bottom;
				int h = TileMatrix.Instance.Height - bounds.Height;
				position.Y = Math.Min(y, h);
			}
		}

		private void UpdateScreenPosition(IGameContent content)
		{
			Vector2 screenPosition = content.Position - position;
			content.UpdateScreenPosition(screenPosition);
		}
	}
}