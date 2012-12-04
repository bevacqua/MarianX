using System;

namespace MarianX.Enum
{
	[Flags]
	public enum MoveResult
	{
		None,
		X		= 1 << 0,
		Y		= 1 << 1,
		Blocked = 1 << 2,
	}
}