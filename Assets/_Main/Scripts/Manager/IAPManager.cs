using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

using UnityEngine.Purchasing;

namespace DE
{
    public class IAPManager : MonoBehaviour, IStoreListener
    {

        public static IAPManager Instance;

        private static IStoreController storeController;
        private static IExtensionProvider storeExtensionProvider;

        private static int currentAmount = default;
        //public static string PRODUCT_ID = "your_product_id";

        [SerializeField] private List<string> _productId = new List<string>();


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

        private void Start()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //builder.AddProduct(PRODUCT_ID, ProductType.NonConsumable);

            _productId.ForEach(productId =>
            {
                builder.AddProduct(productId, ProductType.NonConsumable);
            });

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            storeExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("IAPManager Initialization Failed: " + error);
        }

        public static void BuyProduct(string productId, int amount)
        {
            currentAmount = amount;
            if (storeController != null)
            {
                Product product = storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    storeController.InitiatePurchase(product);
                    
                }
                else
                {
                    Debug.Log("BuyProduct FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProduct FAIL. Not initialized.");
            }
        }

        public  PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            Debug.Log(string.Format("ProcessPurchase: ", args.purchasedProduct.definition.id));

            BuyComplete();
            

            return PurchaseProcessingResult.Complete;
        }

        public async void BuyComplete() {

                // CloudCodeResult buyProductResult = await CloudCodeManager.Instance.BuyGem(currentAmount);

                // if (buyProductResult.IsCompleted)
                // {
                //     Debug.Log("Buy complete on Moblie" + (string)buyProductResult.Data);
                    
                // }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
       
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            //throw new System.NotImplementedException();
        }
    }

}

