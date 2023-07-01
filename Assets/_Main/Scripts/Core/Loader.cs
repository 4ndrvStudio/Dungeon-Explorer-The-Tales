
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DE
{
    public class Loader : MonoBehaviour
    {
        // PRIVATE MEMBERS

        [SerializeField]
        private string _batchModeScene;
        [SerializeField]
        //private StandaloneConfiguration _batchModeConfiguration;

        // Start is called before the first frame update
        void Start()
        {
            if (Application.isBatchMode)
            {
                //load batchmode;
            }
            else
            {
                SceneManager.LoadScene(Global.Settings.Scene_Login);
            }
        }


    }

}
