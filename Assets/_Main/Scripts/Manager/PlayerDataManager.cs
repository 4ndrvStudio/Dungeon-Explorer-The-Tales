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

        public static UnityAction<DataChangeEventName> DataChangeEvent;

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

        void Start()
        {

        }

        public async void UpdateUserInfo()
        {
            CloudCodeResult userInfoResult = await CloudCodeManager.Instance.GetUserInfo();
            if (userInfoResult.IsCompleted)
            {   
                UserInfo userInfo = (UserInfo) userInfoResult.Data;
                _userInfo.UserName = userInfo.UserName;
                _userInfo.Level = userInfo.Level;
                _userInfo.Exp = userInfo.Exp;
                DataChangeEvent.Invoke(DataChangeEventName.User_Info);

            } else {
                Debug.LogError(userInfoResult.Message);
            }

        }
        public void UpdateName(string name)
        {
            _userInfo.UserName = name;
            DataChangeEvent.Invoke(DataChangeEventName.User_Info);
        }

        public async void UpdateCurrencies()
        {
            CloudCodeResult currenciesResult = await CloudCodeManager.Instance.GetUserCurrencies();

            if(currenciesResult.IsCompleted) {
                
                UserCurrencies userCurrencies = (UserCurrencies) currenciesResult.Data;

                _userCurrencies.Energy = userCurrencies.Energy;

                _userCurrencies.Gem = userCurrencies.Gem;

                _userCurrencies.Gold = userCurrencies.Gold;

                DataChangeEvent.Invoke(DataChangeEventName.User_Currency);
                
            } else Debug.LogError(currenciesResult.Message);

           
        }




    }

}
