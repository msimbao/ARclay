using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCollision : MonoBehaviour
{
    public MoveObject triggerList;

     void OnTriggerEnter(Collider collision){

          if (collision.gameObject.tag == "Model") {

            // Destroy(triggerList.TriggerList[0].gameObject);
            // Destroy(collision.gameObject);
            // triggerList.TriggerList.Clear();
            print("Object Deleted");
  
          }
    }
}
