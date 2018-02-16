using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CameraFollow : MonoBehaviour, ICameraFollow
    {

        [SerializeField,]
        private float _distance;

        [SerializeField, Tooltip("Angle for the camera ")]
        private float _angle;

        [SerializeField, Tooltip("Which gameobject to follow:")]
        private Transform _target;

        //Cameras CurrentPosition
        private Vector3 _cameraPosition;


        #region Interfaces
        public void SetAngle(float angle)
        {
            _angle = angle;
        }

        public void SetDistance(float distance)
        {
            _distance = distance;
        }

        public void SetTarget(Transform targetTransform)
        {
            _target = targetTransform;
        }
        #endregion

        private void Update()
        {

            Vector3 _tmpPosition = _target.position;
            Vector3 _frwdDirection = _target.forward;
            //Trigonometry:
            //Distance is longest side of the triangle.
            //HorizontalDistance is Sin from conrner and hypotenuse
            //Height is sqrt from (hypotenusa^2 - a^2)
            float angle = Mathf.Deg2Rad * (_angle);
            float horizontalDistance = Mathf.Sin(angle) * _distance;
            float height = Mathf.Sqrt((_distance * _distance) - (horizontalDistance * horizontalDistance));

            

            _cameraPosition = _tmpPosition;

            Vector3 direction = _frwdDirection;
            direction.y = 0;
            direction.Normalize();
            direction = -direction * horizontalDistance;

            _cameraPosition.x += direction.x;
            _cameraPosition.y += height;
            _cameraPosition.z += direction.z;


            gameObject.transform.position = _cameraPosition;
            gameObject.transform.LookAt(_target);
        }

    }
}