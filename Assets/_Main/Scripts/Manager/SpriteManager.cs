using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE
{
    public class SpriteManager : MonoBehaviour
    {
        public static SpriteManager Instance;

        [SerializeField] private List<Sprite> _currency = new List<Sprite>();

        [SerializeField] private List<Sprite> _itemFrame = new List<Sprite>();

        [System.Serializable]
        public class ItemIcon
        {
            public string ItemId;
            public Sprite Sprite;
        }

        [SerializeField] private List<ItemIcon> _itemIcon = new List<ItemIcon>();

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


        public Sprite GetItemIcon(string itemId)
        {
            ItemIcon item = _itemIcon.Find(item => item.ItemId == itemId);
            return item.Sprite;
        }

        public Sprite GetCurrencySprite(CurrencyName currencyName) => _currency[(int)currencyName];

        public Sprite GetItemFrame(int star) => _itemFrame[star - 1];


    }

}

