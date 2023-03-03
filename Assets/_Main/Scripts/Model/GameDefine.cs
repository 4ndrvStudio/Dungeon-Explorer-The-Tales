using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DE
{
   
    public enum CurrencyName {
        Gold,
        Gem,
        Energy
    }

    public class SceneName
    {
        public static string Scene_Login = "Scene_Login";
        public static string Scene_Home = "Scene_Home";
        public static string Prototype = "Prototype";
    }
    public class ApiName {
        public static string User_SetName = "user-set-name";
        public static string User_GetInfo = "user-get-info";
        public static string User_GetCurrencies = "user-get-currencies";
        
        public static string Buy_GEM = "buy-gem";
        public static string Buy_GOLD = "buy-gold";
        public static string Buy_Box = "buy-box";

        public static string Item_Get_All  = "item-get-all";

        public static string Get_Bundle_Pack_Config = "get-bundle-pack-config";


    }
    public enum DataChangeEventName {
        User_Info,
        User_Currency,
        User_Equipment,
        User_Inventory
    }

    public enum IAPProductID {
        gem_pack_80,
        gem_pack_500,
        gem_pack_1200,
        gem_pack_2500,
        gem_pack_6500,
        gem_pack_14000,
    }

    public enum ViewName
    {
        None,
        Login,
        Home,
        Shop,
        Equipment,
        Stage,
        Battle,
        Gameplay,
        Evolve,
        Loading,
    }

    public enum PopupName
    {
        None,
        SetName,
        Settings,
        UserInfo,
        Pause,
        GameplayDeath,
        PopupPurchase,
        PopupReward,
        PopupError
    }

    public enum EquipmentSlot {
        None = 0, 
        WeaponS = 1,  
        WeaponL = 2,
        Armour = 3,
        Necklace = 4,
        Gloves = 5,
        Boots = 6
    }

    [System.Serializable]
    public class EquipmentStats {
        public StatName StatName;
        public int Payload;
    }

    public enum StatName {
        HP, 
        MP,
        ATK,
        LR,
    }

   
}

