using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DE.Models;

namespace DE
{
    public class Slot_Item : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Image _itemFrame;
        [SerializeField] private Button _itemButton;

        private Item _itemConfig;
        private bool _canClick;

        void Start()
        {
            if (_canClick) _itemButton.onClick.AddListener(() => DoSomething());
        }

        public void SetItemConfig(Item item, bool canClick = true)
        {
            _itemConfig = item;
            _itemIcon.sprite = SpriteManager.Instance.GetItemIcon(item.InventoryItemId);
            _itemFrame.sprite = SpriteManager.Instance.GetItemFrame(item.Star);
            _canClick = canClick;

        }

        public void DoSomething()
        {
            Debug.Log("item clicked");
        }



    }

}
