using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundA : MonoBehaviour
{
public int speed; // rotation speed in degrees/second
public Vector3 A; // point A
public Vector3 B; // point B
Vector3 axis;
public TerrainEditor mydeitor;
public GameObject vaseCollider;
private TrackingInfo tracking;
Vector3 ColliderPosition;

    // Start is called before the first frame update
    void Start()
    {
        speed = 45;
        ColliderPosition = vaseCollider.transform.position;
    }

 void Update(){
   axis = B - A; // calculate axis
   // rotate around point A, perpendicular to axis

    tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
    Vector3 currentPosition = Camera.main.ViewportToWorldPoint(new Vector3(tracking.palm_center.x, tracking.palm_center.y, tracking.depth_estimation * 40f));
    ColliderPosition.x = currentPosition.y;
    ColliderPosition.z = currentPosition.x;
    
   transform.RotateAround(A, axis, speed * Time.deltaTime);
    transform.LookAt(A * Time.deltaTime * speed);


mydeitor.EditTerrain(vaseCollider.transform.position, true, 0.1f, 5);

 }

}

