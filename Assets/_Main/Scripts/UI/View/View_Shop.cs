using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DE.Models;
using TMPro;


namespace DE
{
    public class View_Shop : UIView
    {
        [SerializeField] private Button _goBackBtn;
        [SerializeField] private Button _goHomeBtn;

        [System.Serializable]
        public class GemPackInfo
        {
            public Button PurchaseButton;
            public TextMeshProUGUI AmountText;
            public TextMeshProUGUI PriceText;
            [HideInInspector] public GemConfig GemConfig;
        }

        [SerializeField] private List<GemPackInfo> _gemPackList = new List<GemPackInfo>();

        [System.Serializable]
        public class GoldPackInfo
        {
            public Button PurchaseButton;
            public TextMeshProUGUI AmountText;
            public TextMeshProUGUI PriceText;
            [HideInInspector] public GoldConfig GoldConfig;
        }
        [SerializeField] private List<GoldPackInfo> _goldPackList = new List<GoldPackInfo>();

        [System.Serializable]
        public class BoxPackInfo
        {
            public Button PurchaseButton;
            public TextMeshProUGUI PriceText;
            [HideInInspector] public BoxConfig BoxConfig;
        }
        [SerializeField] private List<BoxPackInfo> _boxPackList = new List<BoxPackInfo>();



        // Start is called before the first frame update
        void Start()
        {
            Initialize();

            _goBackBtn.onClick.AddListener(() => UIManager.Instance.GoBackTab());
            _goHomeBtn.onClick.AddListener(() => UIManager.Instance.GoHomeTab());

            _gemPackList.ForEach(item =>
                item.PurchaseButton.onClick.AddListener(() => BuyGem(item.GemConfig))
            );

            _goldPackList.ForEach(item =>
                item.PurchaseButton.onClick.AddListener(() => BuyGold(item.GoldConfig))
            );

            _boxPackList.ForEach(item =>
                item.PurchaseButton.onClick.AddListener(() => BuyBox(item.BoxConfig))
            );

        }

        private void Initialize()
        {

            //get config Data
            List<GoldConfig> GoldConfigData = DataConfigManager.Instance.BundlePackConfig.Gold;
            GoldConfigData.Sort((x, y) => x.Amount.CompareTo(y.Amount));

            List<GemConfig> GemConfigData = DataConfigManager.Instance.BundlePackConfig.Gem;
            GemConfigData.Sort((x, y) => x.Amount.CompareTo(y.Amount));

            List<BoxConfig> BoxConfigData = DataConfigManager.Instance.BundlePackConfig.Box;
            BoxConfigData.Sort((x, y) => x.Price.CompareTo(y.Price));


            // initialize Gold: Get data from config and display
            for (int i = 0; i < _goldPackList.Count; i++)
            {
                _goldPackList[i].PriceText.text = GoldConfigData[i].Price.ToString("#,##0");
                _goldPackList[i].AmountText.text = GoldConfigData[i].Amount.ToString("#,##0");
                _goldPackList[i].GoldConfig = GoldConfigData[i];
            }
            // initialize Gem: Get data from config and display
            for (int i = 0; i < _gemPackList.Count; i++)
            {

                // Test section : Change location to get exactly price
                _gemPackList[i].PriceText.text = GemConfigData[i].Price.Vnd.ToString("#,##0") + " VND";
                // Test section

                _gemPackList[i].AmountText.text = GemConfigData[i].Amount.ToString("#,##0");
                _gemPackList[i].GemConfig = GemConfigData[i];
            }

            for (int i = 0; i < _boxPackList.Count; i++)
            {
                // Test section : Change location to get exactly price
                _boxPackList[i].PriceText.text = BoxConfigData[i].Price.ToString("#,##0");
                // Test section
                _boxPackList[i].BoxConfig = BoxConfigData[i];
            }



        }

        private async void BuyBox(BoxConfig boxConfig)
        {

            CloudCodeResult boxResult = await CloudCodeManager.Instance.BuyBox(boxConfig.PackId);
            if (boxResult.IsCompleted)
            {
                Item itemReward = (Item)boxResult.Data;
                Dictionary<string, object> customDictionary = new Dictionary<string, object>() {
                    {"itemConfig", itemReward }
                };

                UIManager.Instance.ShowPopup(PopupName.PopupReward, customDictionary);
                PlayerDataManager.Instance.UpdateCurrencies();
            }
            else
            {
                 Dictionary<string, object> customDictionary = new Dictionary<string, object>() {
                    {"errorType", "Purchase Fail" },
                    {"errorMessage", boxResult.Message }
                };
                  UIManager.Instance.ShowPopup(PopupName.PopupError, customDictionary);

            }

        }

        private async void BuyGem(GemConfig gemConfig)
        {
            CloudCodeResult buyProductResult = await CloudCodeManager.Instance.BuyGem(gemConfig.PackId);

            if (buyProductResult.IsCompleted)
            {

                PlayerDataManager.Instance.UpdateCurrencies();

            }

        }

        private void BuyGold(GoldConfig goldConfig)
        {

            Dictionary<string, object> customDictionary = new Dictionary<string, object> {
                {"amount", goldConfig.Amount},
                {"price", goldConfig.Price},
                {"targetToBuy",  CurrencyName.Gold},
                {"buyBuy", CurrencyName.Gem},
                {"packId", goldConfig.PackId}
            };

            UIManager.Instance.ShowPopup(PopupName.PopupPurchase, customDictionary);


        }


    }

}
