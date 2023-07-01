using System;
using UnityEngine;

namespace DE
{
    [Serializable]
    [CreateAssetMenu(fileName = "GlobalSettings", menuName ="DE/Global Settings")]
    public class GlobalSettings
    {
        public string Scene_Loader = "scene_loader";
        public string Scene_Loading = "scene_loading";
        public string Scene_Login = "scene_login";
        public string Scene_Home = "scene_home";


        // public AgentSettings        Agent;
        // public MapSettings          Map;
        // public NetworkSettings      Network;
        // public OptionsData          DefaultOptions;
    }

}
