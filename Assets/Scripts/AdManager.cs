﻿using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Advertisement.Initialize("3771005",false);

        while(!Advertisement.IsReady("video"))
            yield return null;
    }

    public void playVideo()
    {
        Advertisement.Show();
    }

}
