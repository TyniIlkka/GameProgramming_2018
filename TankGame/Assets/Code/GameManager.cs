using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using TankGame.Localization;
using TankGame.Messaging;
using TankGame.Persistence;
using UnityEngine;
using L10n = TankGame.Localization.Localization;
using UnityEngine.SceneManagement;

namespace TankGame
{
    public class GameManager : MonoBehaviour
    {
        #region Statics

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null && !IsClosing)
                {
                    GameObject gameManagerObject = new GameObject(typeof(GameManager).Name);
                    _instance = gameManagerObject.AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        public static bool IsClosing { get; private set; }

        #endregion

        private List<Unit> _enemyUnit = new List<Unit>();
        private Unit _playerUnit = null;
        private SaveSystem _saveSystem;

        public Score score;
        //[SerializeField, Tooltip("Score to win!")]            //Unused Max Score limit.
        //private int _maxScore;
        [SerializeField, Tooltip("Player Lives:")]
        private int m_iPlayerLives;


        public event Action<int> ScoreChanged;
        //public int MaxScore { get { return _maxScore; } }
        public Score Score { get { return score; } private set { score = value; } }

        public bool WinOrLose{ get; set;}


        public int StartingLives { get; private set; }
        public int PlayerLives
        {
            get { return m_iPlayerLives; }
            private set { m_iPlayerLives = value; }
        }
		public string SavePath
		{
			get { return Path.Combine( Application.persistentDataPath, "save" ); }
		}

		public MessageBus MessageBus { get; private set; }

		protected void Awake()
		{
			if ( _instance == null )
			{
				_instance = this;
			}
			else if ( _instance != this )
			{
				Destroy( gameObject );
				return;
			}

			Init();
		}

		private void OnApplicationQuit()
		{
			IsClosing = true;
		}

		private void OnDestroy()
		{
			L10n.LanguageLoaded -= OnLanguageLoaded;
            IsClosing = true;
		}

		private void Init()
		{
			InitLocalization();

			IsClosing = false;

			MessageBus = new MessageBus();

			var UI = FindObjectOfType< UI.UI >();
			UI.Init();
            UI.ScoreUI.AddScoreUI();
            
			Unit[] allUnits = FindObjectsOfType< Unit >();
			foreach ( Unit unit in allUnits )
			{
				AddUnit( unit );
			}
			_saveSystem = new SaveSystem( new BinaryPersitence( SavePath ) );

            
        }

		private const string LanguageKey = "Language";

		private void InitLocalization()
		{
			LangCode currentLang =
				(LangCode) PlayerPrefs.GetInt( LanguageKey, (int) LangCode.EN );
			L10n.LoadLanguage( currentLang );
			L10n.LanguageLoaded += OnLanguageLoaded;
		}

		private void OnLanguageLoaded( LangCode currentLanguage )
		{
			PlayerPrefs.SetInt( LanguageKey,
				(int) currentLanguage );
		}

		protected void Update()
		{
			bool save = Input.GetKeyDown( KeyCode.F2 );
			bool load = Input.GetKeyDown( KeyCode.F3 );

			if ( save )
			{
				Save();
			}
			else if ( load )
			{
				Load();
			}
		}

		public void AddUnit( Unit unit )
		{
			unit.Init();

			if ( unit is EnemyUnit )
			{
				_enemyUnit.Add( unit );
			}
			// Adding a player unit after the initialization really makes no sense because
			// we can have a reference to only one player unit. Be carefull with this
			else if ( unit is PlayerUnit )
			{
				_playerUnit = unit;
                _playerUnit.Health.SetLives(PlayerLives);
                UI.UI.Current.LivesUI.SetLivesItem(_playerUnit, PlayerLives);
                
			}

			// Add unit's health to the UI.
			UI.UI.Current.HealthUI.AddUnit( unit );
            
		}

        /// <summary>
        /// Sends new Health UI for respawned unit.
        /// </summary>
        /// <param name="unit"></param>
        public void RespawnUnit(Unit unit)
        {
            UI.UI.Current.HealthUI.AddUnit(unit);
        }

		public void Save()
		{
			GameData data = new GameData();
			foreach ( Unit unit in _enemyUnit )
			{
				data.EnemyDatas.Add( unit.GetUnitData() );
			}
			data.PlayerData = _playerUnit.GetUnitData();

			_saveSystem.Save( data );
		}

		public void Load()
		{
			GameData data = _saveSystem.Load();
			foreach ( UnitData enemyData in data.EnemyDatas )
			{
				Unit enemy = _enemyUnit.FirstOrDefault( unit => unit.Id == enemyData.Id );
				if ( enemy != null )
				{
					enemy.SetUnitData( enemyData );
				}
			}

			_playerUnit.SetUnitData( data.PlayerData );
		}

        /// <summary>
        /// Add more score and send scorechange.
        /// </summary>
        /// <param name="amount">Add amount of score to currentscore</param>
        public void AddScore(int amount)
        {
            Score.CurrentScore += amount;
            if (ScoreChanged != null)
            {
                ScoreChanged(Score.CurrentScore);
            }

            if (Score.CurrentScore >= Score.WinScore)
            {
                WinOrLose = true;
                EndGame(true);
            }
        }
        /// <summary>
        /// Player died.
        /// </summary>
        public void PlayerDied()
        {
            PlayerLives--;
            Debug.Log("Lives left: " + PlayerLives);

            if (PlayerLives <= 0)
            {
                WinOrLose = false;
                EndGame(false);
            }
        }

        /// <summary>
        /// Sends EndGame UI init and publish message
        /// </summary>
        /// <param name="status">true = victory, false = lost.</param>
        public void EndGame(bool status)
        {
            UI.UI.Current.EndUI.EndGame(WinOrLose);
            MessageBus.Publish(new GameEndMessage(isWin: WinOrLose));
            Time.timeScale = 0;
        }

        /// <summary>
        /// Reset the game.
        /// </summary>
        public void ResetGame()
        {
            _playerUnit.Health.SetHealth(_playerUnit.StartingHealth);
            IsClosing = true;
            Debug.LogError("Reset new game currently not working");
            SceneManager.LoadScene("level1");
        }
        
	}
}
