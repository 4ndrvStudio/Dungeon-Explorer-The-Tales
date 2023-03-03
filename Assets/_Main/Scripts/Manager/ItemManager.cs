using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DE.Models;

namespace DE
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;
        [SerializeField] private List<Item> _userItems = new List<Item>();
        [HideInInspector] public List<Item> UserItems => _userItems;

        [Header("Item Frames")] 
        public List<Sprite> itemFrames = new List<Sprite>();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        public void UpdateUserItem() {
            
        }

        

        
        
        
    }

}
