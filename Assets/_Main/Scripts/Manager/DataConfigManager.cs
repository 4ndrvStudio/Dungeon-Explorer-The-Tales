using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DE.Models;

namespace DE
{
    public class DataConfigManager : MonoBehaviour
    {
        public static DataConfigManager Instance;
        
        [SerializeField] private BundlePackConfig _bundlePackConfig;
        public BundlePackConfig BundlePackConfig => _bundlePackConfig;

        void Awake()
        {
            if (Instance != this && Instance != null)
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
            if (Instance = this) Instance = null;
        }

        public async void GetBundlePackConfig() {
            
            CloudCodeResult dataConfigRespone =  await CloudCodeManager.Instance.GetBundlePackConfig();

            if(dataConfigRespone.IsCompleted) {

                _bundlePackConfig = (BundlePackConfig) dataConfigRespone.Data;
    
                return;
            } 

            Debug.Log("Can't Get Data Config");
            




        }





    }

}
