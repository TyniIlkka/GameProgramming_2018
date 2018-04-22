using UnityEngine;
using UnityEngine.UI;
using TankGame.Localization;
using TankGame.Messaging;
using l10n = TankGame.Localization.Localization;

namespace TankGame.UI
{
    public class EndUIItem : MonoBehaviour
    {

        private Text _text;
        private bool winOrLose;

        private const string WinKey = "win";
        private const string LoseKey = "lose";

        /// <summary>
        /// Initialization of lives ui item.
        /// </summary>
        /// <param name="result">Result of the game is it win or lose.</param>
        public void Init(bool result)
        {
            l10n.LanguageLoaded += OnLanguageChange;
            _text = GetComponentInChildren<Text>();
            winOrLose = result;
            SetText(winOrLose);
        }

        /// <summary>
        /// Updates lives text when language is changed.
        /// </summary>
        /// <param name="currentLang">Current language code</param>
        private void OnLanguageChange(LangCode currentLang)
        {
            SetText(winOrLose);
        }

        /// <summary>
        /// Sets lives text.
        /// </summary>
        /// <param name="result">Is it vicotry or lose.</param>
        private void SetText(bool result)
        {
            string translation;
            if (GameManager.Instance.WinOrLose)
            {
                translation = l10n.CurrentLanguage.GetTranslation(WinKey);
                _text.color = Color.green;
            }
            else
            {
                translation = l10n.CurrentLanguage.GetTranslation(LoseKey);
                _text.color = Color.red;
            }
            
            _text.text = string.Format(translation);
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
            l10n.LanguageLoaded -= OnLanguageChange;
        }
    }
}
