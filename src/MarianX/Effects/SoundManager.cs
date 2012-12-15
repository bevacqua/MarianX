using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MarianX.Effects
{
	public class SoundManager
	{
		private readonly IList<Sound> sounds;

		public SoundManager(IList<Sound> sounds)
		{
			this.sounds = sounds;
		}

		public void Load(ContentManager content)
		{
			foreach (Sound sound in sounds)
			{
				sound.Load(content);
			}
		}

		protected void Play(Sound sound)
		{
			sound.Play();
		}

		public void StopAll()
		{
			foreach (Sound sound in sounds)
			{
				sound.Stop();
			}
		}
	}
}