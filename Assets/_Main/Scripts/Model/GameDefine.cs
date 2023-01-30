using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DE
{
   

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
    }
    public class DataChangeEventName {
        public static string User_Info = "User_Info";
        public static string User_Currency = "User_Currency";
        public static string User_Equipment = "User_Equipment";
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
        GameplayDeath
    }

    public enum EquipmentType {
        WeaponS,
        WeaponL,
        Armour,
        Necklace,
        Gloves,
        Boots
    }
     public enum EquipmentRate {
        Normal = 1,
        Good = 2 , 
        Better = 3,
        Excellent = 4
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

