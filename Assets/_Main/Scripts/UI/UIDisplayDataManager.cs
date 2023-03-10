using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DE.Models;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


namespace DE
{
    public class UIDisplayDataManager : MonoBehaviour
    {
        [System.Serializable]
        public class UserInfoUI
        {
            public string Name;
            public TextMeshProUGUI UserName;
            public TextMeshProUGUI Level;
            public Slider Exp;
        }

        [System.Serializable]
        public class UserCurrenciesUI
        {
            public string Name;
            public TextMeshProUGUI Energy;
            public TextMeshProUGUI Gold;
            public TextMeshProUGUI Gem;

        }
        [System.Serializable]
        public class UserEquipmentUI
        {
            public ViewName ViewName;
            public GameObject ContentUI;
            public GameObject ItemPrefab;
        }


        [SerializeField] private List<UserInfoUI> _userInfoUIList = new List<UserInfoUI>();

        [SerializeField] private List<UserCurrenciesUI> _userCurrenciesUIList = new List<UserCurrenciesUI>();

        [SerializeField] private List<UserEquipmentUI> _userEquipmentUIList = new List<UserEquipmentUI>();

        private List<GameObject> _userEquipmentItemContainer = new List<GameObject>();

        void OnEnable()
        {
            PlayerDataManager.DataChangeEvent += DataChangeListener;
        }

        void OnDisable()
        {
            PlayerDataManager.DataChangeEvent -= DataChangeListener;
        }


        private void DataChangeListener(DataChangeEventName dataChangeEventName)
        {
            switch (dataChangeEventName)
            {
                case DataChangeEventName.User_Info:
                    UpdateUserInfo();
                    break;
                case DataChangeEventName.User_Currency:
                    UpdateUserCurrencies();
                    break;
                case DataChangeEventName.User_Equipment:
                    UpdateUserEquipment();
                    break;
            }
        }


        private void UpdateUserInfo()
        {
            _userInfoUIList.ForEach(userInfoUI =>
            {
                int level = PlayerDataManager.Instance.UserInfo.Level;
                int exp = PlayerDataManager.Instance.UserInfo.Exp;
                float targetProgress = (float)exp / ((float)level * 100);
                //update to UI;
                if (userInfoUI.UserName != null)
                    userInfoUI.UserName.text = PlayerDataManager.Instance.UserInfo.UserName;

                if (userInfoUI.Level != null)
                    userInfoUI.Level.text = level.ToString();

                if (userInfoUI.Exp != null)
                    userInfoUI.Exp.value = targetProgress;
            });
        }

        private void UpdateUserCurrencies()
        {
            _userCurrenciesUIList.ForEach(userCurrenciesUI =>
            {
                if (userCurrenciesUI.Energy != null)

                    //DOTween.To(() => int.Parse(userCurrenciesUI.Energy.text), x => userCurrenciesUI.Energy.text = x.ToString() + "/30", PlayerDataManager.Instance.UserCurrencies.Energy,2f);
                    userCurrenciesUI.Energy.text = PlayerDataManager.Instance.UserCurrencies.Energy.ToString() + "/30";


                if (userCurrenciesUI.Gold != null)
                    //DOTween.To(() => int.Parse(userCurrenciesUI.Gold.text), x => userCurrenciesUI.Gold.text = x.ToString() , PlayerDataManager.Instance.UserCurrencies.Energy,2f);
                    userCurrenciesUI.Gold.text = PlayerDataManager.Instance.UserCurrencies.Gold.ToString("#,##0");


                if (userCurrenciesUI.Gem != null)
                    userCurrenciesUI.Gem.text = PlayerDataManager.Instance.UserCurrencies.Gem.ToString("#,##0");

            });
        }

        private void UpdateUserEquipment()
        {
            // _userEquipmentItemContainer.ForEach(item => Destroy(item));
            // _userEquipmentItemContainer.Clear();

            // _userEquipmentUIList.ForEach(userEquipmentUI =>
            // {
            //     PlayerDataManager.Instance.UserEquipmentItems.ForEach(equipmentItem =>
            //     {
            //         GameObject item = Instantiate(userEquipmentUI.ItemPrefab, Vector3.zero, Quaternion.identity, userEquipmentUI.ContentUI.transform);
            //         item.GetComponent<Slot_Item>().SetItemData(equipmentItem, userEquipmentUI.ViewName);
            //         _userEquipmentItemContainer.Add(item);
            //     });
            // });
        }

    }

}
