using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DE.Models
{   
   
    /* Equipment ID Define 
        WeaponS from 1001 -> 2000
        WeaponL from 2001 -> 3000
        Armour from 3001 -> 4000
        Necklace from 4001 -> 5000
        Gloves from 5001 -> 6000
        Boots from 6001 -> 7000
    */

    [System.Serializable]
    public class Item {
        public EquipmentSlot EquipmentSlot;
        public string Name;
        public string PlayersInventoryItemId;
        public string InventoryItemId;
        public bool IsEquipped;
        public int Level;
        public int Star;
        public int Value;       
    }
       public class ItemTest {
        public int EquipmentSlot;
        public string Name;
        public string PlayersInventoryItemId;
        public string InventoryItemId;
        public bool IsEquipped;
        public int Level;
        public int Star;
        public int Value;       
    }


    [System.Serializable]
    public class UserInfo
    {
        public string UserName;
        public int Level;
        public int Exp;
    }
    [System.Serializable]
    public class UserCurrencies
    {
        public int Energy = default;
        public int Gold = default;
        public int Gem = default;
    }
    

    ///============== ==> config 

    [System.Serializable]
    public class BoxConfig {
        public string PackId;
        public int Rate;
        public int Price;
        public int RewardRate;
        public int RewardStar;
    }
     [System.Serializable]
    public class GoldConfig {
        public string PackId;
        public int Amount;
        public int Price;
    }
    public class GemPrice {
        public int Vnd;
    }

    [System.Serializable]
    public class GemConfig {
        public string PackId;
        public int Amount;
        public GemPrice Price;
    }

    [System.Serializable]
    public class BundlePackConfig {
        public List<BoxConfig> Box;
        public List<GoldConfig> Gold;
        public List<GemConfig> Gem;
      
    }
    ///config <========= =================


}
