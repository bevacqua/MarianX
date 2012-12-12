using System;
using Microsoft.Xna.Framework;

namespace MarianX.Events
{
	public class MoveArgs : EventArgs
	{
		public Vector2 From { get; set; }
		public Vector2 To { get; set; }
	}
}