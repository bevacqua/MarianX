using System;

namespace MarianX.Enum
{
	[Flags]
	public enum MoveResult
	{
		None				= 0,
		X					= 1 << 0,
		Y					= 1 << 1,
		BlockedOnNegativeX	= 1 << 2,
		BlockedOnPositiveX	= 1 << 3,
		BlockedOnX			= BlockedOnNegativeX | BlockedOnPositiveX,
		BlockedOnNegativeY	= 1 << 4,
		BlockedOnPositiveY	= 1 << 5,
		BlockedOnY			= BlockedOnNegativeY | BlockedOnPositiveY,
		Blocked				= BlockedOnX | BlockedOnY,
		Died				= 1 << 6,
		FlattenXSpeed		= 1 << 7,
		FlattenYSpeed		= 1 << 8,
		LevelCompleted		= 1 << 9
	}
}