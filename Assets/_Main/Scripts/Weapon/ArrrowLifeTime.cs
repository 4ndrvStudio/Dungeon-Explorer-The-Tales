using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrrowLifeTime : MonoBehaviour
{
    
    void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime() {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

}
