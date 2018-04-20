using UnityEngine;
using System;

namespace TankGame
{
    public class Score : MonoBehaviour
    {
        [SerializeField, Tooltip("Target Points to collect")]
        private int m_iTargetScore;

        private int m_iScore;

        public event Action<int> ScoreChanged;

        public int CurrentScore
        {
            get { return m_iScore; }
            set
            {
                m_iScore = value;
                if (ScoreChanged != null)
                {
                    ScoreChanged(m_iScore);
                }
            }
        }

        public int TargetScore { get { return m_iTargetScore;} }
    }
}
