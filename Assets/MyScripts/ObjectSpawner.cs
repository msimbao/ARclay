using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] prefabs;
    public Transform objectParent;

    public void SpawnCube()
    {
            var childObject = Instantiate(prefabs[0], new Vector3(75, 0, 0), Quaternion.identity);
            childObject.transform.SetParent(objectParent);
    }

        public void SpawnCylinder()
    {
            var childObject =  Instantiate(prefabs[1], new Vector3(75, 0, 0), Quaternion.identity);
            childObject.transform.SetParent(objectParent);
    }

        public void SpawnPlane()
    {
            var childObject = Instantiate(prefabs[2], new Vector3(75, 0, 0), Quaternion.identity);
            childObject.transform.SetParent(objectParent);
    }

        public void SpawnSphere()
    {
            var childObject = Instantiate(prefabs[3], new Vector3(75, 0, 0), Quaternion.identity);
            childObject.transform.SetParent(objectParent);
    }

}
