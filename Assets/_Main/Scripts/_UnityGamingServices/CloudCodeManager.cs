using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudCode;
using Unity.Services.CloudSave;
using DE.Models;

namespace DE
{
    public class CloudCodeManager : MonoBehaviour
    {
        public static CloudCodeManager Instance;

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

      
        public async Task<CloudCodeResult> GetUserInfo()
        {
            try
            {

                UserInfo userInfo = await CloudCodeService.Instance.CallEndpointAsync<UserInfo>(ApiName.User_GetInfo);

                return new CloudCodeResult
                {
                    IsCompleted = true,
                    Data = userInfo
                };

            }
            catch (CloudCodeException error)
            {
                Debug.LogError(error);
                return new CloudCodeResult
                {
                    IsCompleted = false,
                    Message = error
                };
            }
            catch (Exception error)
            {
                Debug.LogError(error);
                return new CloudCodeResult
                {
                    IsCompleted = false
                };
            }
        }
        
        public async Task<CloudCodeResult> SetUserName(string name)
        {
            try
            {
                await CloudCodeService.Instance.CallEndpointAsync<string>(
                    ApiName.User_SetName, 
                    new Dictionary<string, object> { { "name", name } }
                );
    
                return new CloudCodeResult {
                    IsCompleted= true
                };
            }
            catch (CloudCodeException err)
            {
                Debug.Log(err);
                return new CloudCodeResult
                {
                    IsCompleted = false
                };
            }
        }
        
        public async Task<CloudCodeResult> GetUserCurrencies() {

            try
            {
                UserCurrencies userCurrencies = await CloudCodeService.Instance.CallEndpointAsync<UserCurrencies>(ApiName.User_GetCurrencies);

                return new CloudCodeResult {
                    IsCompleted= true,
                    Data = userCurrencies
                };

            }
            catch (CloudCodeException err)
            {
                Debug.Log(err);
                return new CloudCodeResult
                {
                    IsCompleted = false
                };
            }

        }
        
    }

    public struct CloudCodeResult
    {
        public bool IsCompleted;
        public CloudCodeException Message;
        public object Data;
    }


}
