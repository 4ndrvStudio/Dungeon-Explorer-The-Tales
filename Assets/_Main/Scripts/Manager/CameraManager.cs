using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

namespace DE
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;
        [SerializeField] private CinemachineVirtualCamera _cineCam;
        private CinemachineBasicMultiChannelPerlin _cineNoise;
        // Start is called before the first frame update
        
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
        void Start()
        {
            _cineNoise = _cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ShakeCam(float noiseFactor, float time)
        {

            DOTween.To(() => _cineNoise.m_AmplitudeGain, x => _cineNoise.m_AmplitudeGain = x, noiseFactor, 0f).OnComplete(async () =>
            {
                float delayTime = time * 1000f;
                await Task.Delay((int)delayTime);
                DOTween.To(() => _cineNoise.m_AmplitudeGain, x => _cineNoise.m_AmplitudeGain = x, 0f, 0f);
            });

        }

        public void SetFollowPos (Transform pos) => _cineCam.Follow = pos;
    }

}
