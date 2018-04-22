using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankGame.Messaging;

namespace TankGame
{
    public class UnitReSpawner : MonoBehaviour
    {

        [SerializeField, Tooltip("How long player tank take to respawn")]
        private float _playerTankRespawnTime;

        [SerializeField, Tooltip("How long enemy tanks take to respawn")]
        private float _enemyTankRespawnTime;

        private ISubscription<UnitDiedMessage> _unitDiedSubscription;

        private List<Unit> _deadTanks = new List<Unit>();       //List of tanks which are destroyed.
        private List<float> _timers = new List<float>();        //List of destroyed tanks timers.

        private void Start()
        {
            _unitDiedSubscription = GameManager.Instance.MessageBus.Subscribe<UnitDiedMessage>(OnUnitDied);
            
        }

        /// <summary>
        /// Counts down Timers on units waiting respawn
        /// </summary>
        private void Update()
        {
            for (int i = 0; i < _deadTanks.Count; i++)
            {
                _timers[i] -= Time.deltaTime;
                if (_timers[i] <= 0)
                {
                    _deadTanks[i].gameObject.SetActive(true);
                    _deadTanks[i].Respawn();

                    // Message to HealthUI to tell that unit is respawned
                    //GameManager.Instance.MessageBus.Publish(new RespawnedTank(_deadTanks[i]));
                    GameManager.Instance.RespawnUnit(_deadTanks[i]);
                    _deadTanks.RemoveAt(i);
                    _timers.RemoveAt(i);
                }
            }


        }

        /// <summary>
        /// Waits call to add units to deadlist
        /// </summary>
        /// <param name="msg">UnitDiedMessage</param>
        private void OnUnitDied(UnitDiedMessage msg)
        {
            if (msg.DeadUnit is PlayerUnit)
            {
                if (msg.DeadUnit.Health.CurrentLives <= 0)
                {
                    Debug.Log("Lost");
                    GameManager.Instance.WinOrLose = false;
                    GameManager.Instance.EndGame(false);
                    return;
                }
                _deadTanks.Add(msg.DeadUnit);
                _timers.Add(_playerTankRespawnTime);
            }
            else
            {
                _deadTanks.Add(msg.DeadUnit);
                _timers.Add(_enemyTankRespawnTime);
            }
        }


    }
}

