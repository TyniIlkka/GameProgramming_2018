using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Coin : MonoBehaviour
    {

        [SerializeField, Tooltip("Points to give amount.")]
        private int _point = 50;
        [SerializeField, Tooltip("Rotate Speed")]
        private float rotateSpeed;
        private System.Action<Coin> _collissionCallback;

        public void Init(System.Action<Coin> collissionCallback)
        {
            _collissionCallback = collissionCallback;
        }

        private void OnTriggerEnter(Collider coll)
        {
            Debug.Log("Coins collected for: " + _point);
            GameManager.Instance.AddScore(_point);
            _collissionCallback(this);
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        }
    }
}
