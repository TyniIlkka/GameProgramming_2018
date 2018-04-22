using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.UI {
    public class EndUI : MonoBehaviour
    {

        [SerializeField]
        private EndUIItem _endUIItemPre;
        [SerializeField]
        private Button _button;


        public void Init()
        {
            Debug.Log("Init EndUI");
        }

        /// <summary>
        /// Enables EndGame UI text and button.
        /// </summary>
        /// <param name="result"></param>
        public void EndGame(bool result)
        {
            
            var _endUIItem = Instantiate(_endUIItemPre, transform);
            _endUIItem.Init(GameManager.Instance.WinOrLose);


            Debug.Log("EndGame");
            _endUIItem.gameObject.SetActive(true);
            _button.transform.SetAsLastSibling();
            _button.gameObject.SetActive(true);
            
            _button.onClick.AddListener(ButtonClick);
            //_button.SetActive(true);
        }

        private void ButtonClick()
        {

            GameManager.Instance.ResetGame();
        }
    }
}
