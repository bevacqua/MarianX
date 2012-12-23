using System;
using System.Collections.Generic;
using MarianX.Backgrounds;
using MarianX.Effects;
using MarianX.Interface;
using MarianX.Items;
using MarianX.Mobiles.NPC;
using MarianX.Mobiles.Player;
using MarianX.Physics;
using MarianX.UI;
using MarianX.World.Platform;

namespace MarianX.Core
{
	public class LevelManager
	{
		private static readonly LevelManager instance;

		public static LevelManager Instance
		{
			get { return instance; }
		}

		static LevelManager()
		{
			instance = new LevelManager(GameCore.Instance);
		}

		private readonly GameCore game;

		private readonly IList<LevelBackground> levels;
		private Marian marian;
		private SongManager songManager;

		public int LevelIndex { get; private set; }

		public int Count
		{
			get { return 3; }
		}

		private LevelManager(GameCore game)
		{
			this.game = game;

			levels = new List<LevelBackground>();
			LevelIndex = -1;
		}

		public void Initialize()
		{
			InitializeMap();
			LifeManager.Instance.SetLives(Config.Lives);
			SetLevelByIndex(Config.Start);
		}

		private void InitializeMap()
		{
			if (levels.Count > 0) // sanity.
			{
				return;
			}

			for (int i = 1; i <= Count; i++)
			{
				LevelBackground level = new LevelBackground(i);
				levels.Add(level);
			}
		}

		public void AdvanceLevel()
		{
			SetLevelByIndex(++LevelIndex);
		}

		public void SetLevelByIndex(int index)
		{
			LevelBackground target = levels[index];
			TileMatrix.Use(target);

			LevelIndex = index;
			LoadLevel(target);
		}

		private void LoadLevel(LevelBackground background)
		{
			game.ClearContent();
			game.ViewportManager.Clear();

			game.AddAndTrack(background);

			InitializeMarian();
			InitializeEffects();

			AddAndTrackNpcs();
			AddAndTrackItems();

			game.AddAndTrack(marian);
			game.InitializeBase();

			LifeManager.Instance.Initialize();

			game.AddPersistantContent();
		}

		private void AddAndTrackNpcs()
		{
			IList<NpcRecord> records = TileMatrix.GetNpcLocations();

			foreach (NpcRecord record in records)
			{
				Npc npc = (Npc)Activator.CreateInstance(record.Type);
				npc.StartPosition = record.Position;
				game.AddAndTrack(npc);
			}
		}

		private void AddAndTrackItems()
		{
			IList<ItemRecord> records = TileMatrix.GetItemMetadata();

			foreach (ItemRecord record in records)
			{
				Item item = (Item)Activator.CreateInstance(record.Type);
				item.StartPosition = record.Position;
				game.AddAndTrack(item);
			}
		}

		private void InitializeMarian()
		{
			if (marian == null)
			{
				marian = new Marian();
				marian.Move += game.ViewportManager.CharacterMove;
			}

			var collisionDetection = marian.Movement.CollisionDetection as IGameContent;
			if (collisionDetection != null)
			{
				game.AddContent(collisionDetection);
			}

			game.DynamicCollisionDetection = new DynamicCollisionDetectionEngine(marian);
		}

		private void InitializeEffects()
		{
			if (songManager == null)
			{
				songManager = new SongManager();
			}
			game.AddContent(songManager);
		}
	}
}