using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DE.Models;

namespace DE
{
    public class View_Equipment : UIView
    {
        [SerializeField] private Button _goBackBtn;
        [SerializeField] private Button _goHomeBtn;

        [SerializeField] private GameObject ItemUIPrefab;
        [SerializeField] private GameObject ContentPanel;

        [SerializeField] private List<GameObject> _inventoryItem = new List<GameObject>();
        [SerializeField] private List<GameObject> _eqipmentItem = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            _goBackBtn.onClick.AddListener(() => UIManager.Instance.GoBackTab());
            _goHomeBtn.onClick.AddListener(() => UIManager.Instance.GoHomeTab());
        }
    
        void Awake()
        {
         
            //InventoryManager.DataChangeEvent += DisplayData;
        }
        void OnDestroy()
        {
           // InventoryManager.DataChangeEvent -= DisplayData;
        }

        private void DisplayData()
        {
            _inventoryItem.Clear();
            _eqipmentItem.Clear();
            
            List<Item> inventoryItem = InventoryManager.Instance.InventoryItem.FindAll(item => item.IsEquipped == false);
            Debug.Log(inventoryItem.Count);
            inventoryItem.Sort((x, y) =>
            {
                int compareResult = x.Star.CompareTo(y.Star);
                if (compareResult == 0)
                {
                    compareResult = x.Name.CompareTo(y.Name);
                }
                return compareResult;
            });
            inventoryItem.ForEach(item => {
                GameObject itemObject = Instantiate(ItemUIPrefab,ContentPanel.transform);
                itemObject.GetComponent<Slot_Item>().SetItemConfig(item,false);
               
                _inventoryItem.Add(itemObject);
            });
           
        

        }

    }

}
