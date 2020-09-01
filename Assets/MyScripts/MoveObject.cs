using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

       //The list of colliders currently inside the trigger
    public  List<Collider> TriggerList = new List<Collider>();
    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureContinuous point;
    private ManoGestureTrigger click;
    private HandSide side;

    private string handTag = "Player";

    private GameObject clayParent;

    // Start is called before the first frame update
    void Start()
    {
        clayParent = GameObject.Find("Clay");
        HandSide side = HandSide.Palmside;
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        point = ManoGestureContinuous.POINTER_GESTURE;
    }

  /// <summary>
    /// 
    /// </summary>
    /// <param name="other">The collider that stays</param>
    void OnTriggerStay()
    {
        MoveWhenGrab();
    }

  /// <summary>
    /// If grab is performed while hand collider is in the cube.
    /// The cube will follow the hand.
    /// </summary>
    private void MoveWhenGrab()
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
        {
        TriggerList[0].gameObject.transform.position = gameObject.transform.position;
        }

        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
        {
            if(TriggerList[0].gameObject.transform.localScale.x > 0.5f)
            {
            // TriggerList[0].gameObject.transform.Rotate(Vector3.up * Time.deltaTime * 30, Space.World);
            TriggerList[0].gameObject.transform.localScale -= new Vector3(0.005f,0.005f,0.005f);
            }
      

        }
        else if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == point)
        {
            TriggerList[0].gameObject.transform.localScale += new Vector3(0.005f,0.005f,0.005f);

        }

        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
        {
            DeleteTopObject();
        }
        // else if(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.hand_side == side){
        //     Destroy(TriggerList[0].gameObject);
        // }

    }


 
 //called when something enters the trigger
 void OnTriggerEnter(Collider other)
 {
     //if the object is not already in the list
     if(!TriggerList.Contains(other))
     {
         //add the object to the list
         TriggerList.Add(other);
     }
 }
 
 //called when something exits the trigger
 void OnTriggerExit(Collider other)
 {
     //if the object is in the list
     if(TriggerList.Contains(other))
     {
         //remove it from the list
         TriggerList.Remove(other);
     }
 }

 public void DeleteTopObject()
 {
     if(TriggerList[0].gameObject){
     Destroy(TriggerList[0].gameObject);
     TriggerList.Clear();
     }
 }

}
