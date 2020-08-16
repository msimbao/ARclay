using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject arcamera;

    void Start(){
        transform.position = arcamera.transform.position;
        transform.rotation = arcamera.transform.rotation;
    }

    void Update ()
    {
        transform.position = arcamera.transform.position;
        transform.rotation = arcamera.transform.rotation;

    }
}