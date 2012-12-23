using System;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace MarianX.World.Platform
{
	public class MobileRecord
	{
		public string TypeName { get; set; }

		public Type Type
		{
			get { return Assembly.GetEntryAssembly().GetType(TypeName); }
		}

		public int X { get; set; }
		public int Y { get; set; }

		public Vector2 Position
		{
			get { return new Vector2(X * Tile.Width, Y * Tile.Height); }
		}
	}
}