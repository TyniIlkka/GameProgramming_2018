using TankGame.Localization;
using TankGame.Messaging;
using UnityEngine;
using UnityEngine.UI;
using l10n = TankGame.Localization.Localization;

namespace TankGame.UI
{
    public class ScoreUIItem : MonoBehaviour
    {

        // The component which draws the text to the UI.
        private Text _text;

        private const string ScoreKey = "score";

        private ISubscription<ScoreChangedMessage> _scoreChangedSubscription;

        protected void OnDestroy()
        {
            UnregisterEventListeners();
        }

        public void Init(Unit unit)
        {
            l10n.LanguageLoaded += OnLanguageLoaded;
            _text = GetComponentInChildren<Text>();


            SetText(GameManager.Instance.CurrentScore);
        }

        private void OnLanguageLoaded(LangCode currentLang)
        {
            SetText(_unit.Health.CurrentHealth);
        }

        private void OnScoreChange(int amount)
        {
            SetText(amount);
        }

        private void UnregisterEventListeners()
        {
            l10n.LanguageLoaded -= OnLanguageLoaded;
        }

        private void SetText(int currentScore)
        {
            string translation = l10n.CurrentLanguage.GetTranslation(ScoreKey);
            int maxScore = GameManager.Instance.MaxScore;
            _text.text = string.Format(translation, currentScore, maxScore);
        }
        // A reference to the unit which health this component draws to the UI.
        private Unit _unit;
    }
}