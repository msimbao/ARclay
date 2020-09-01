// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectSpawner : MonoBehaviour
{

    public GameObject[] prefabs;
    public GameObject objectParent;
    public Image pickedColor;
    public GameObject[] blobContainers;
    private Metaball childMetaball;
    private MCBlob mcBlobParent;
    private int parentNumber;

    void Start(){
        objectParent = blobContainers[0];
        parentNumber = 0;
    }

    public void changeParent(int number)
    {
        parentNumber = number;
        objectParent = blobContainers[number];

        for(int blob = 9;blob < 15; blob++){
            Physics.IgnoreLayerCollision(0, blob);
        }
        Physics.IgnoreLayerCollision(0,number+9, false);

    }

    public void makeShape(int prefab){
        var childObject = Instantiate(prefabs[prefab], new Vector3(-25f,50,20f), Quaternion.identity);
        childObject.transform.SetParent(objectParent.transform);
        childObject.layer = parentNumber + 9;
        childObject.transform.localScale  = new Vector3(1,1,1);
        mcBlobParent = objectParent.GetComponent<MCBlob>();
        childMetaball = childObject.GetComponent<Metaball>();

        childMetaball.color = pickedColor.color;

    }

}
