using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;
using DE.Models;


namespace DE
{
    public class PlayerDataManager : MonoBehaviour
    {
        public static PlayerDataManager Instance;

        [SerializeField] private UserInfo _userInfo = new UserInfo();
        [HideInInspector] public UserInfo UserInfo => _userInfo;

        [SerializeField] private UserCurrencies _userCurrencies = new UserCurrencies();
        [HideInInspector] public UserCurrencies UserCurrencies => _userCurrencies;

        [SerializeField] private List<EquipmentItemConfig> _userEquipmentItems = new List<EquipmentItemConfig>();
        [HideInInspector] public List<EquipmentItemConfig> UserEquipmentItems => _userEquipmentItems; 
        
        public static UnityAction<string> DataChangeEvent;

        void Awake () {
            if(Instance != this && Instance != null) {
                Destroy(this);
            }
            else Instance = this;
        }
        void OnDestroy() {
            if(Instance == this) Instance = null;
        }

        void Start() {
            UserEquipmentItems userEquipment = new UserEquipmentItems();
            userEquipment.Id = 3001;
            userEquipment.Level = 3;
            userEquipment.Rate = 2;
            List<UserEquipmentItems> listTest =new List<UserEquipmentItems> {userEquipment};
            UpdateEquipmentItems(listTest);
        }
        
        public void UpdateUserInfo(UserInfo userInfo) {
            _userInfo.UserName = userInfo.UserName;
            _userInfo.Level = userInfo.Level;
            _userInfo.Exp = userInfo.Exp;
            DataChangeEvent.Invoke(DataChangeEventName.User_Info);
            
        }
        public void UpdateName(string name ) {
            _userInfo.UserName = name;
            DataChangeEvent.Invoke(DataChangeEventName.User_Info);
        } 
        public void UpdateCurrencies(UserCurrencies userCurrencies) {
           _userCurrencies.Energy = userCurrencies.Energy;
           _userCurrencies.Gem = userCurrencies.Gem;
           _userCurrencies.Gold = userCurrencies.Gold;
           DataChangeEvent.Invoke(DataChangeEventName.User_Currency);
        }

        public void UpdateEquipmentItems(List<UserEquipmentItems> userEquipmentItemList ) {
            _userEquipmentItems.Clear();
            userEquipmentItemList.ForEach(item => {
                EquipmentItemConfig equipmentItemConfig =  EquipmentConfigManager.Instance.AllEquipmentConfigFile.Find(equipmentItemConfig => equipmentItemConfig.Id == item.Id);
                if(equipmentItemConfig != null) {
                        equipmentItemConfig.Level = item.Level;
                        equipmentItemConfig.EquipmentRate = (EquipmentRate) item.Rate;
                        _userEquipmentItems.Add(equipmentItemConfig);
                }
            });
            
            _userEquipmentItems.Sort();
             DataChangeEvent.Invoke(DataChangeEventName.User_Equipment);

        }


    }

}
