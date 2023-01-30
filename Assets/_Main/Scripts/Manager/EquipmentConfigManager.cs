using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DE.Models;

namespace DE
{
    public class EquipmentConfigManager : MonoBehaviour
    {
        public static EquipmentConfigManager Instance;

        [SerializeField] private List<EquipmentItemConfig> _allEquipmentConfigFile = new List<EquipmentItemConfig>();
        public List<EquipmentItemConfig> AllEquipmentConfigFile => _allEquipmentConfigFile;

        void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(this);
            }
            else Instance = this;
        }
        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

    }

}
