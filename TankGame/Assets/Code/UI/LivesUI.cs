﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.UI
{
    public class LivesUI : MonoBehaviour
    {

        [SerializeField]
        private LivesUIItem _livesUIItem;

        public void Init()
        {
            Debug.Log("Lives UI initialized");
        }

        public void SetLivesItem(Unit unit)
        {
            _livesUIItem.Init(unit);
            _livesUIItem.gameObject.SetActive(true);
        }
    }
}
