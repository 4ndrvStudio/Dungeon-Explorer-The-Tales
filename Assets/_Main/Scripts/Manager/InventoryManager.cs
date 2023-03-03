using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DE.Models;

namespace DE
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        [SerializeField] private List<Item> _inventoryItem = new List<Item>();
        [HideInInspector] public List<Item> InventoryItem => _inventoryItem;

        [SerializeField] private List<Item> _equipmentItem = new List<Item>();
        [HideInInspector] public List<Item> EquipmentItem => _equipmentItem;

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
            if (Instance == this) Instance = null;
        }

        public async void UpdateInventory() {
            
            CloudCodeResult itemRes = await CloudCodeManager.Instance.GetUserInventory();

            if (itemRes.IsCompleted) {
                List<Item> listItemRespone = (List<Item>) itemRes.Data;
                _inventoryItem =  listItemRespone.FindAll(item => item.IsEquipped == false);
                _equipmentItem =  listItemRespone.FindAll(item => item.IsEquipped == true);
             }
        }
    }

}
