using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDown : MonoBehaviour
{
     void OnCollisionStay(Collision collision){

          if (collision.gameObject.tag == "Model") {
              Debug.Log("What it Do Lower");
              collision.gameObject.transform.localScale -= new Vector3(0.01f,0.01f,0.01f);
              
          }
    }
}
