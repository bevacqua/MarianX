using System;
using Microsoft.Xna.Framework;

namespace MarianX.Mobiles
{
	public class MoveEventArgs : EventArgs
	{
		public Vector2 From { get; set; }
		public Vector2 To { get; set; }
	}
}