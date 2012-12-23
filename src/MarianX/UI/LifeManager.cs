using System.Collections.Generic;
using MarianX.Core;

namespace MarianX.UI
{
	public class LifeManager
	{
		private static readonly LifeManager instance;

		public static LifeManager Instance
		{
			get { return instance; }
		}

		static LifeManager()
		{
			instance = new LifeManager(GameCore.Instance);
		}

		private readonly GameCore game;
		private readonly IList<Life> lives;

		public int Count
		{
			get { return lives.Count; }
		}

		public LifeManager(GameCore game)
		{
			this.game = game;
			lives = new List<Life>();
		}

		public void SetLives(int amount)
		{
			int currentCount = Count;

			if (amount > currentCount)
			{
				for (int i = 0; i < amount - currentCount; i++)
				{
					Add();
				}
			}
			else if (amount < currentCount)
			{
				for (int i = 0; i < currentCount - amount; i++)
				{
					Subtract();
				}
			}
		}

		public void Initialize()
		{
			foreach (Life life in lives)
			{
				life.Initialize();
				game.AddContent(life);
			}
		}

		public void Add()
		{
			Life life = new Life(lives.Count + 1);
			lives.Add(life);
		}

		public bool Subtract()
		{
			if (lives.Count > 0)
			{
				Life life = lives[lives.Count - 1];
				life.Unload();
				lives.Remove(life);
			}

			return lives.Count > 0;
		}
	}
}