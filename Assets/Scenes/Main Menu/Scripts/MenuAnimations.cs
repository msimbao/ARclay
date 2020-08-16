using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimations : MonoBehaviour
{
 private GameObject panel_Foo;
 Animation anim_Foo ;
public GameObject[] manualPanels;
     
 void OnEnable()
 {
     panel_Foo = gameObject;
     anim_Foo = panel_Foo.GetComponent<Animation> ();
     manualPanels = GameObject.FindGameObjectsWithTag("ManualPanel");
 }
 
 public void FadeAway()
 {
     
     anim_Foo.Play("FadeAway");
 }

  public void FadeBack()
 {

        foreach (GameObject panel in manualPanels)
        {
        anim_Foo = panel.GetComponent<Animation> ();
        anim_Foo.Rewind("FadeAway");
        }
     
 }

}
