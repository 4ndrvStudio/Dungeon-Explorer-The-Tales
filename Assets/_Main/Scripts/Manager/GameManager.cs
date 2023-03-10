using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using DE.Models;
using System;


namespace DE
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [Header("Authentication Info")]
        public string PlayerId;
        
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

        public void Start()
        {

            Application.targetFrameRate = 300;
           // await UnityServices.InitializeAsync();
             
        }
        public async Task LoginAnonymous()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                PlayerId  = AuthenticationService.Instance.PlayerId;
                
                PlayerDataManager.Instance.UpdateUserInfo();
                PlayerDataManager.Instance.UpdateCurrencies();
                InventoryManager.Instance.UpdateInventory();
                DataConfigManager.Instance.GetBundlePackConfig();


        
            }
            catch (AuthenticationException ex)
            {
                // Compare error code to AuthenticationErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }
            catch (RequestFailedException ex)
            {
                // Compare error code to CommonErrorCodes
                // Notify the player with the proper error message
                Debug.LogException(ex);
            }

        }

    }
}
