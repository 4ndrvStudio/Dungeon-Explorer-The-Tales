using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DE.Models;

namespace DE
{

    public class Popup_Purchase : UIPopup
    {

        [SerializeField] private Image _targetSprite;
        [SerializeField] private Image _buyBuySprite;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _pricingText;
        [SerializeField] private Button _purchaseButton;

        public override void Start()
        {
            base.Start();
            _purchaseButton.onClick.AddListener(() => ConfirmBuyItem());
        }

        public override void Show(Dictionary<string, object> customProperties)
        {
            base.Show(customProperties);

            CurrencyName targetName = (CurrencyName)customProperties["targetToBuy"];
            CurrencyName buyBuy = (CurrencyName)customProperties["buyBuy"];


            _targetSprite.sprite = SpriteManager.Instance.GetCurrencySprite(targetName);
            _buyBuySprite.sprite = SpriteManager.Instance.GetCurrencySprite(buyBuy);
            _amountText.text =  customProperties["amount"].ToString();
            _pricingText.text = customProperties["price"].ToString();
        }

        private void ConfirmBuyItem()
        {
            switch((CurrencyName)_customProperties["targetToBuy"]) {
                case CurrencyName.Gold : BuyGold();
                    break;
            }

            Hide();
        }

        private async void BuyGold() {
            
            string packId =_customProperties["packId"].ToString();
            CloudCodeResult buyItemResult = await CloudCodeManager.Instance.BuyGold(packId);

            if (buyItemResult.IsCompleted)
            {
                PlayerDataManager.Instance.UpdateCurrencies();
            }
            else
            {
                Debug.Log(buyItemResult.Message);
            }
        }

        public override void Hide()
        {
            base.Hide();
            _targetSprite.sprite = null;
            _buyBuySprite.sprite = null;
            _amountText.text =null;
            _pricingText.text = null;

        }




    }

}