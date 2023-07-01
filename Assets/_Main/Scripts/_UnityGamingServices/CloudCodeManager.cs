using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.CloudCode;
using DE.Models;
using Newtonsoft.Json;

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
                var userInfo = await CloudCodeService.Instance.CallEndpointAsync<UserInfo>(ApiName.User_GetInfo);

                return new CloudCodeResult
                {
                    IsCompleted = true,
                    Data = userInfo
                };

            }
            catch (CloudCodeException error)
            {
                CloudCodeErrorResult errorResult = HandleCloudCodeError(error);
                return new CloudCodeResult
                {
                    IsCompleted = false,
                    Message = errorResult.message
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

                return new CloudCodeResult
                {
                    IsCompleted = true
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

        public async Task<CloudCodeResult> GetUserCurrencies()
        {

            try
            {
                var userCurrencies = await CloudCodeService.Instance.CallEndpointAsync<UserCurrencies>(ApiName.User_GetCurrencies);

                return new CloudCodeResult
                {
                    IsCompleted = true,
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

        public async Task<CloudCodeResult> GetUserInventory()
        {
            try
            {

                
                string jsonRes = await CloudCodeService.Instance.CallEndpointAsync<string>(ApiName.Item_Get_All);
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonRes);
               

                return new CloudCodeResult
                {
                    IsCompleted = true,
                    Data = items
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

        public async Task<CloudCodeResult> BuyGem(string packId)
        {
            try
            {
                var userGEM = await CloudCodeService.Instance.CallEndpointAsync<string>(ApiName.Buy_GEM,
                new Dictionary<string, object> {
                    {"bundlePackId" , packId}
                });

                return new CloudCodeResult
                {
                    IsCompleted = true,
                    Data = userGEM
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

        public async Task<CloudCodeResult> BuyGold(string packID)
        {
            try
            {
                int userGold = await CloudCodeService.Instance.CallEndpointAsync<int>(ApiName.Buy_GOLD,
                new Dictionary<string, object> {
                    {"bundlePackId" , packID}
                });

                return new CloudCodeResult
                {
                    IsCompleted = true,
                    Data = userGold
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

        public async Task<CloudCodeResult> BuyBox(string packID)
        {
            try
            {
                Item itemReward = await CloudCodeService.Instance.CallEndpointAsync<Item>(ApiName.Buy_Box,
                new Dictionary<string, object> {
                    {"bundlePackId" , packID}
                });

                Debug.Log(itemReward.Name);

                return new CloudCodeResult
                {
                    IsCompleted = true,
                    Data = itemReward
                };

            }
            catch (CloudCodeException err)
            {
                CloudCodeErrorResult errorResult =  HandleCloudCodeError(err);
                Debug.Log(errorResult.message);

                return new CloudCodeResult
                {
                    IsCompleted = false,
                    Message = errorResult.message

                };
            }
        }
        
        public async Task<CloudCodeResult> GetBundlePackConfig()
        {
            try
            {
                BundlePackConfig dataConfig = await CloudCodeService.Instance.CallEndpointAsync<BundlePackConfig>(ApiName.Get_Bundle_Pack_Config);
                return new CloudCodeResult
                {
                    IsCompleted = true,
                    Data = dataConfig
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

        public CloudCodeErrorResult HandleCloudCodeError(CloudCodeException e)
        {
            try
            {
                // extract the JSON part of the exception message
                var trimmedMessage = e.Message;
                trimmedMessage = trimmedMessage.Substring(trimmedMessage.IndexOf('{'));
                trimmedMessage = trimmedMessage.Substring(0, trimmedMessage.LastIndexOf('}') + 1);
            
                return JsonUtility.FromJson<CloudCodeErrorResult>(trimmedMessage);
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
                return new CloudCodeErrorResult {
                    message = "Unknown Error. Please Try again"
                };
            }
        }



    }

    public struct CloudCodeResult
    {
        public bool IsCompleted;
        public string Message;
        public object Data;
    }
    public class CloudCodeErrorResult
    {
        public string status;
        public string message;
    }


}
