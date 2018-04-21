using TankGame.Localization;
using TankGame.Messaging;
using UnityEngine;
using UnityEngine.UI;
using l10n = TankGame.Localization.Localization;

namespace TankGame.UI
{
    public class LivesUIItem : MonoBehaviour
    {

        private Unit _unit;
        private Text _text;
        private int _maxLives;

        private const string LivesKey = "lives";

        /// <summary>
        /// Initialization of lives ui item.
        /// </summary>
        /// <param name="unit">Unit which lives are displayed</param>
        public void Init(Unit unit, int lives)
        {
            l10n.LanguageLoaded += OnLanguageChange;
            _unit = unit;
            _text = GetComponentInChildren<Text>();
            _maxLives = lives;

            _unit.Health.LivesChanged += OnLivesChange;
            SetText(lives);
        }

        /// <summary>
        /// Updates lives text when lives amount changes.
        /// </summary>
        /// <param name="lives">Current lives amount</param>
        private void OnLivesChange(int lives)
        {
            SetText(lives);
        }

        /// <summary>
        /// Updates lives text when language is changed.
        /// </summary>
        /// <param name="currentLang">Current language code</param>
        private void OnLanguageChange(LangCode currentLang)
        {
            SetText(_unit.Health.CurrentLives);
        }

        /// <summary>
        /// Sets lives text.
        /// </summary>
        /// <param name="lives">Current lives</param>
        private void SetText(int lives)
        {
            string translation = l10n.CurrentLanguage.GetTranslation(LivesKey);
            _text.text = string.Format(translation, lives, _maxLives);
        }

        private void OnDestroy()
        {
            UnregisterEventListeners();
        }

        /// <summary>
        /// Stops listening event listeners.
        /// </summary>
        private void UnregisterEventListeners()
        {
            _unit.Health.LivesChanged -= OnLivesChange;
            l10n.LanguageLoaded -= OnLanguageChange;
        }
    }
}