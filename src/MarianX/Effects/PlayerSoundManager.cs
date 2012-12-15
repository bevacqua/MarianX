using System.Collections.Generic;

namespace MarianX.Effects
{
	public class PlayerSoundManager : SoundManager
	{
		private static readonly Sound fall = new Sound(new[]
		{
			"Sounds/fall0",
			"Sounds/fall1",
			"Sounds/fall2",
			"Sounds/fall3",
		});

		private static readonly Sound jump = new Sound(new[]
		{
			"Sounds/jump0",
			"Sounds/jump1",
			"Sounds/jump2",
			"Sounds/jump3"
		});

		private static readonly Sound die = new Sound(new[]
		{
			"Sounds/death0",
			"Sounds/death1",
			"Sounds/death2"
		});

		private static readonly IList<Sound> sounds = new[] { fall, jump, die };

		public PlayerSoundManager()
			: base(sounds)
		{
		}

		public void Jump()
		{
			Play(jump);
		}

		public void Fall()
		{
			Play(fall);
		}

		public void Die()
		{
			Play(die);
		}
	}
}