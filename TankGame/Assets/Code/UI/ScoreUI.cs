using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField]
        private ScoreUIItem _scoreUIItemPrefab;

        public void Init()
        {
            Debug.Log("Score UI initialized");
        }

        public void AddUnit(Unit unit)
        {
            var scoreItem = Instantiate(_scoreUIItemPrefab, transform);
            scoreItem.Init(unit);
            scoreItem.gameObject.SetActive(true);
        }

    }
}
