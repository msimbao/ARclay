using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Intro");

        if (objs.Length > 1)
        {
            Destroy(objs[1]);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}