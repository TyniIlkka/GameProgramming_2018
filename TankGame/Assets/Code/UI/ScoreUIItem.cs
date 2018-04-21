﻿using TankGame.Localization;
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
        private int _score;
        private const string ScoreKey = "score";

        protected void OnDestroy()
        {
            UnregisterEventListeners();
        }

        public void Init()
        {
            l10n.LanguageLoaded += OnLanguageChange;
            _text = GetComponentInChildren<Text>();
            _score = GameManager.Instance.CurrentScore;
            GameManager.Instance.ScoreChanged += SetText;

            SetText(_score);
        }

        private void OnScoreChange(int score)
        {
            SetText(score);
        }

        private void OnLanguageChange(LangCode currentLang)
        {
            SetText(GameManager.Instance.CurrentScore);
        }

        private void SetText(int currentScore)
        {
            string translation = l10n.CurrentLanguage.GetTranslation(ScoreKey);
            
            _text.text = string.Format(translation, GameManager.Instance.CurrentScore.ToString());
        }

        private void UnregisterEventListeners()
        {
            GameManager.Instance.ScoreChanged -= OnScoreChange;
            l10n.LanguageLoaded -= OnLanguageChange;
        }
    }
}