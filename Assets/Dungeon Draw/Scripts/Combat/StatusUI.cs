using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    public GameObject ui;
    
    public void ActivateUI(Transform spawnLocation)
    {
        Vector3 pos = spawnLocation.position;
        transform.position = new Vector3(pos.x, pos.y + 2f, pos.z); 
        // offset will need to be changed if entity locations change
        ui.SetActive(true);
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }
}
