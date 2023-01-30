using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public class AnimatorHook : MonoBehaviour
    {
        [SerializeField] private WeaponHandler _weaponHandle;

        public void OnEnableWeaponCol() => _weaponHandle.EnableWeaponCol();

        public void OnDisableWeaponCol() => _weaponHandle.DisableWeaponCol();

    }

}
