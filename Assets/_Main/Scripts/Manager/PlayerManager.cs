using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _spawnerLocation;
        private bool IsSpawned;


        // Update is called once per frame
        void Update()
        {
            // Test Spawn player
            if (View_Gameplay.Instance != null && !IsSpawned) {
                Instantiate(_playerPrefab, _spawnerLocation.position, Quaternion.identity, this.transform);
                IsSpawned = true;
            }
    }
    }

}
