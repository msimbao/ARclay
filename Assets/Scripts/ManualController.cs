using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualController : MonoBehaviour
{
    private int slideNumber;
    public GameObject[] slides;
    public GameObject[] toActivate;
    public DisableTrackedVisuals m_SessionOrigin;
    // Start is called before the first frame update
    void Start()
    {
        slideNumber = 1;
        m_SessionOrigin.OnPlacedObject(false);
    }


    public void nextSlide()
    {
        switch(slideNumber)
        {
            case 1:
            slides[1].SetActive(true);
            slideNumber++;
            break;

            case 2:
            slides[2].SetActive(true);
            toActivate[1].SetActive(true);
            slideNumber++;
            break;

            case 3:
            slides[3].SetActive(true);
            toActivate[0].SetActive(true);
            m_SessionOrigin.OnPlacedObject(true);
            slideNumber++;
            break;

            case 4:
            slides[4].SetActive(true);
            toActivate[17].SetActive(true);
            slideNumber++;
            break;

            case 5:
            slides[5].SetActive(true);
            toActivate[2].SetActive(true);
            toActivate[3].SetActive(true);
            toActivate[4].SetActive(true);
            toActivate[5].SetActive(true);

            
            slideNumber++;
            break;

            case 6:
            slides[6].SetActive(true);

            toActivate[6].SetActive(true);
            toActivate[7].SetActive(true);
            toActivate[8].SetActive(true);
            toActivate[9].SetActive(true);

            slideNumber++;
            break;

            case 7:
            slides[7].SetActive(true);

            toActivate[10].SetActive(true);

            toActivate[13].SetActive(false);
            toActivate[14].SetActive(false);
            toActivate[15].SetActive(false);
            toActivate[16].SetActive(false);

            slideNumber++;
            break;

            case 8:
            slides[8].SetActive(true);
            slideNumber++;
            break;

            case 9:
            
            toActivate[10].SetActive(false);

            gameObject.SetActive(false);      
            slideNumber++;
            break;
        }
    }
}
