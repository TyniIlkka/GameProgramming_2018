using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Coin : MonoBehaviour
    {

        [SerializeField, Tooltip("Points to give amount.")]
        private int _point = 50;

        private System.Action<Coin> _collissionCallback;

        public void Init(System.Action<Coin> collissionCallback)
        {
            _collissionCallback = collissionCallback;
        }


        private void OnCollisionEnter(Collision collision)
        {
            GameManager.Instance.AddScore(_point);

            Debug.Log("Coins collected for: "+ _point);
            _collissionCallback(this);
        }

        private void Update()
        {
            //TODO: rotation for coins.
        }
    }
}
