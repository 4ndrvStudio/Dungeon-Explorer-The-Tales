using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSingleton : MonoBehaviour
{
    public static GlobalSingleton Instance;
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
    void Start() => DontDestroyOnLoad(gameObject);

}
