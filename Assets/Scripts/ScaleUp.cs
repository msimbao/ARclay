using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{

     void OnCollisionStay(Collision collision){

          if (collision.gameObject.tag == "Model") {
              Debug.Log("What it Do Larger");
              collision.gameObject.transform.localScale += new Vector3(0.01f,0.01f,0.01f);
              
          }
    }
}
