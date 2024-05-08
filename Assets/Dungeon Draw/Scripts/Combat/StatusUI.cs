using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    public GameObject ui;
    public AudioSource src;
    public AudioClip hoverAudio;
    
    public void ActivateUI(Transform spawnLocation)
    {
        src.clip = hoverAudio;
        src.Play();
        
        Vector3 pos = spawnLocation.position;
        transform.position = new Vector3(pos.x, pos.y + 4f, pos.z); 
        ui.SetActive(true);
    }

    public void HideUI()
    {
        ui.SetActive(false);
    }
}
