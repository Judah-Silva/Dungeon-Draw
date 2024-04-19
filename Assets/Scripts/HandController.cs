using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public int posNum = 5;

    public static List<GameObject> positionsList = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        float stepSize = 1.0f / (posNum + 1); // create spacing

        for (int i = 0; i <= posNum; i++)
        {
            float t = i * stepSize;
            Vector3 interpolatedPos = Vector3.Lerp(startPos.position, endPos.position, t);
            Quaternion interpolatedRot = Quaternion.Slerp(startPos.rotation, endPos.rotation, t);

            // Create a new GameObject at the interpolated position
            GameObject newObj = new GameObject("spawnPoint" + i);
            newObj.transform.position = interpolatedPos;
            newObj.transform.rotation = interpolatedRot;

            positionsList.Add(newObj);
        }
    }
}
