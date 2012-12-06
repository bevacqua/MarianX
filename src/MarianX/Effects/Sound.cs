using System.Collections.Generic;
using MarianX.World.Extensions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MarianX.Effects
{
	public class Sound
	{
		private readonly IList<string> soundAssets;
		private readonly IList<SoundEffect> soundEffects;

		public bool Stackable { get; set; }

		public SoundEffectInstance Current { get; private set; }

		public Sound(IList<string> soundAssets)
		{
			this.soundAssets = soundAssets;

			soundEffects = new List<SoundEffect>();
		}

		public void Load(ContentManager content)
		{
			foreach (string soundAsset in soundAssets)
			{
				SoundEffect asset = content.Load<SoundEffect>(soundAsset);
				soundEffects.Add(asset);
			}
		}

		public void Play()
		{
			if (Current != null && Current.State == SoundState.Playing && !Stackable)
			{
				return;
			}
			SoundEffect effect = soundEffects.Random();
			Current = effect.CreateInstance();
			Current.Play();
		}
	}
}