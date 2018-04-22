using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField]
        private ScoreUIItem _scoreItem;

        public void Init()
        {
            Debug.Log("Score UI initialized");
        }

        public void AddScoreUI()
        {
            //_scoreItem.GetComponentInChildren<ScoreUIItem>();
            //var LivesItem = Instantiate(_scoreItem, transform);
            _scoreItem.Init();
            _scoreItem.gameObject.SetActive(true);
            Debug.Log("Activate ScoreUI");
        }

    }
}