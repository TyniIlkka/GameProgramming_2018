using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public class CoinSpawner : MonoBehaviour {

        [SerializeField, Tooltip("How many Coins should be on the field at the same time.")]
        private int m_iPoolSize = 25;

        [SerializeField, Tooltip("Spawn Delay for Coins")]
        private float m_fSpawnDelay = 1.5f;

        [SerializeField, Tooltip("Collectable Prefab")]
        private Coin m_cCoinPrefab;

        [SerializeField]
        public Vector3 m_vStart = new Vector3(5, 0, 5);

        [SerializeField]
        public Vector3 m_vEnd = new Vector3(-5, 0, -5);

        private List<Vector3> points = new List<Vector3>();
        private List<float> m_lfPositionsX = new List<float>();
        private List<float> m_lfPositionsZ = new List<float>();
        private Pool<Coin> m_pCoins;

        //Minimum and Maximum values for coins to spawn.
        private float m_fMaxX;
        private float m_fMinX;
        private float m_fMaxZ;
        private float m_fMinZ;

        private float m_fTimer;

        private void Awake()
        {
            m_pCoins = new Pool<Coin>(m_iPoolSize, false ,m_cCoinPrefab, InitCoins);
            Init();
        }

        private void Init()
        {
            points.Add(m_vStart);
            points.Add(m_vEnd);

            for (int i = 0; i < points.Count; i++)
            {
                m_lfPositionsX.Add(points[i].x);
                m_lfPositionsZ.Add(points[i].z);
            }
            SpawnRange();
        }

        private void Update()
        {
            bool result = false;
            m_fTimer += Time.deltaTime;

            if (m_fTimer >= m_fSpawnDelay)
            {
                result = true;
                m_fTimer = 0;
            }

            if (result)
            {
                Spawn();
            }
        }

        private void InitCoins(Coin coin)
        {
            coin.Init(CoinsCollected);
        }

        private void Spawn()
        {
            Coin coin = m_pCoins.GetPooledObject();
            if (coin != null)
            {
                coin.transform.position = RandomSpawnPoint();
            }
        }

        private void SpawnRange()
        {
            m_fMaxX = GetTopPoint(m_lfPositionsX);
            m_fMaxZ = GetTopPoint(m_lfPositionsZ);

            m_fMinX = GetLowPoint(m_lfPositionsX);
            m_fMinZ = GetLowPoint(m_lfPositionsZ);
        }

        float GetTopPoint(List<float>coords)
        {
            float result = coords[0];

            for (int i = 0; i < coords.Count; i++)
            {
                if (coords[i] > result)
                {
                    result = coords[i];
                }
            }
            return result;
        }

        float GetLowPoint(List<float> coords)
        {
            float result = coords[0];
            for (int i = 0; i < coords.Count; i++)
            {
                if (coords[i] < result)
                {
                    result = coords[i];
                }
            }
            return result;
        }

        private Vector3 RandomSpawnPoint()
        {
            float x = Random.Range(m_fMinX, m_fMaxX);
            float z = Random.Range(m_fMinZ, m_fMaxZ);

            return new Vector3(x, 0.5f, z);
        }

        private void CoinsCollected(Coin coin)
        {
            if (!m_pCoins.ReturnObject(coin))
            {
                Debug.LogError("ERROR: Failed to return Coin back to pool");
            }
        }
    }
}
