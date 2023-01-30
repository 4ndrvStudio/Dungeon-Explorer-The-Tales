using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private GameObject _weaponHolderRight;
        [SerializeField] private GameObject _weaponHolderLeft;

        // Update is called once per frame
        void Update()
        {
            StateCheck();
        }
        void StateCheck()
        {
            _weaponHolderRight.SetActive(!_playerInput.AimJoy.IsPressed);
            _weaponHolderLeft.SetActive(_playerInput.AimJoy.IsPressed);
        }

        public void EnableWeaponCol() => _weaponHolderRight.GetComponentInChildren<BoxCollider>().enabled = true;

        public void DisableWeaponCol() => _weaponHolderRight.GetComponentInChildren<BoxCollider>().enabled = false;

    }
}

