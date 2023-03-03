using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
namespace DE {
    public class InitializeUGS : MonoBehaviour {
    
    public string environment = "production";
 
    async void Start() {
        try {
            var options = new InitializationOptions()
                .SetEnvironmentName(environment);
 
            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception) {
            Debug.Log(exception);
        }
    }
}
}
