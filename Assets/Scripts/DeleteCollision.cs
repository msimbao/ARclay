using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCollision : MonoBehaviour
{

     void OnCollisionEnter(Collision collision){

          if (collision.gameObject.tag == "Model") {
              Debug.Log("What it Do Delete");
              Destroy(collision.gameObject);
              
          }
    }
}
