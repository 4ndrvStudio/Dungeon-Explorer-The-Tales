using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DE
{
    public interface IGlobalService
    {
        void Initialize();
        void Tick();
        void Deinitialize();
    }

    public static class Global
    {
        //Public 
        public static GlobalSettings Settings { get; set; }

        private static bool _isInitialized;


        public static void Quit()
		{
			Deinitialize();

#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}


		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void InitializeSubSystem()
		{
			if (Application.isBatchMode == true)
			{
				UnityEngine.AudioListener.volume = 0.0f;
				// PlayerLoopUtility.RemovePlayerLoopSystems(typeof(PostLateUpdate.UpdateAudio));
			}

#if UNITY_EDITOR
			if (Application.isPlaying == false)
				return;
#endif
			// if (PlayerLoopUtility.HasPlayerLoopSystem(typeof(Global)) == false)
			// {
			// 	PlayerLoopUtility.AddPlayerLoopSystem(typeof(Global), typeof(Update.ScriptRunBehaviourUpdate), BeforeUpdate, AfterUpdate);
			// }

			// Application.quitting -= OnApplicationQuit;
			// Application.quitting += OnApplicationQuit;

			_isInitialized = true;
		}

       private static void Deinitialize()
		{
			if (_isInitialized == false)
				return;

			// for (int i = _globalServices.Count - 1; i >= 0; i--)
			// {
			// 	var service = _globalServices[i];
			// 	if (service != null)
			// 	{
			// 		service.Deinitialize();
			// 	}
			// }
            
			_isInitialized = false;
		}




    }

}
