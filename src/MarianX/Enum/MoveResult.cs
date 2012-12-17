using System;

namespace MarianX.Enum
{
	[Flags]
	public enum MoveResult
	{
		None				= 0,
		X					= 1 << 0,
		Y					= 1 << 1,
		BlockedOnX			= 1 << 2,
		BlockedOnY			= 1 << 3,
		Blocked				= BlockedOnX | BlockedOnY,
		Died				= 1 << 4,
	}
}