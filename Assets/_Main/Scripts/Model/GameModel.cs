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

    [CreateAssetMenu(fileName = "Item Config", menuName = "Game Data/Equipment Item Config")]
    public class EquipmentItemConfig : ScriptableObject {
        public Sprite Icon;
        public string Name;
        public int Id;
        public EquipmentType WeaponType;
        [HideInInspector] public EquipmentRate EquipmentRate;
        [HideInInspector] public int Level = 1;
        public List<EquipmentStats> EquipmentStats;
    }

    public struct UserEquipmentItems {
        public int Id;
        public int Level;
        public int Rate;
    }
    

    [System.Serializable]
    public struct UserInfo
    {
        public string UserName;
        public int Level;
        public int Exp;
    }
    [System.Serializable]
    public struct UserCurrencies
    {
        public int Energy;
        public int Gold;
        public int Gem;
    }


     /* Equipment ID Define 
        WeaponS from 1001 -> 2000
        WeaponL from 2001 -> 3000
        Armour from 3001 -> 4000
        Necklace from 4001 -> 5000
        Gloves from 5001 -> 6000
        Boots from 6001 -> 7000
    */




}
