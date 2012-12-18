using System;
using System.Collections.Generic;
using MarianX.Core;
using MarianX.Enum;
using MarianX.Interface;
using MarianX.World.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MarianX.Effects
{
	public class SongManager : IGameContent
	{
		private readonly IList<string> assetNames = new[]
		{
			"Music/track0",
			"Music/track1"
		};

		private readonly IList<Song> songs = new List<Song>();

		public void Initialize()
		{
			MediaPlayer.IsRepeating = false;
			MediaPlayer.IsShuffled = false;
			MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
		}

		public void Load(ContentManager content)
		{
			foreach (string assetName in assetNames)
			{
				Song song = content.Load<Song>(assetName);
				songs.Add(song);
			}
		}

		private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
		{
			if (MediaPlayer.State == MediaState.Stopped)
			{
				if (stopping)
				{
					stopping = false;
				}
				else
				{
					PlayRandomSong();
				}
			}
		}

		public void PlayRandomSong()
		{
			Song song = songs.Random();
			MediaPlayer.Play(song);
		}

		private bool stopping;
		private KeyboardState oldState;

		public void UpdateInput(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();
			KeyboardConfiguration kb = new KeyboardConfiguration(keyboardState);

			if (kb.IsKeyPressed(ActionKey.ToggleMusic, oldState))
			{
				if (MediaPlayer.State == MediaState.Stopped)
				{
					PlayRandomSong();
				}
				else
				{
					stopping = true;
					MediaPlayer.Stop();
				}
			}

			oldState = keyboardState;
		}

		public void UpdateOutput(GameTime gameTime)
		{
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
		}

		public void Unload()
		{
		}

		public void UpdateScreenPosition(Vector2 screenPosition)
		{
		}

		public Vector2 Position
		{
			get { throw new NotImplementedException(); }
		}
	}
}