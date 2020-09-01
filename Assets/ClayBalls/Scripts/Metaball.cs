using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
[ExecuteInEditMode]
public class Metaball : MonoBehaviour
{
    SphereCollider myCollider;

    public float radius;
    public int Type = 1;
    public Color color;

    public float xPosition;
    public float yPosition;
    public float zPosition;

    public float xScale;
    public float yScale;
    public float zScale;

    public float xRotation;
    public float yRotation;
    public float zRotation;


        void Update()
    {

        myCollider = GetComponent<SphereCollider>();

        radius = myCollider.radius;


        xPosition = transform.localPosition.x;
        yPosition = transform.localPosition.y;
        zPosition = transform.localPosition.z;

        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        zScale = transform.localScale.z;

        xRotation = transform.localEulerAngles.x;
        yRotation = transform.localEulerAngles.y;
        zRotation = transform.localEulerAngles.z;

    }

}
