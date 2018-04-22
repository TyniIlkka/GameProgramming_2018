using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.UI
{
	public class UI : MonoBehaviour
	{
		public static UI Current { get; private set; }

		public HealthUI HealthUI { get; private set; }

        public ScoreUI ScoreUI { get; private set; }

        public LivesUI LivesUI { get; private set; }

        public EndUI EndUI { get; private set; }

        /// <summary>
        /// Init UI.
        /// </summary>
		public void Init()
		{
			Current = this;
			HealthUI = GetComponentInChildren< HealthUI >();
			HealthUI.Init();

            ScoreUI = GetComponentInChildren<ScoreUI>();
            ScoreUI.Init();

            LivesUI = GetComponentInChildren<LivesUI>();
            LivesUI.Init();

            EndUI = GetComponentInChildren<EndUI>();
            EndUI.Init();
		}
	}
}
