using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    public GameObject ui;
    public AudioSource src;
    public AudioClip hoverAudio;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI vulnerableText;
    public TextMeshProUGUI weakText;
    
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
        shieldText.gameObject.SetActive(false);
        vulnerableText.gameObject.SetActive(false);
        weakText.gameObject.SetActive(false);
        ui.SetActive(false);
    }

    public void DisplayInfo(Entity entity)
    {
        hpText.text = $" - {entity.getHP()} / {entity.maxHP}";

        if (entity.getShield() != 0)
        {
            shieldText.text = $" - {entity.getShield()}";
            shieldText.gameObject.SetActive(true);
        }

        if (entity.getVulnerable() != 0)
        {
            vulnerableText.text = $" - {entity.getVulnerable()}";
            vulnerableText.gameObject.SetActive(true);
        }

        if (entity.getDamageMod() != 0)
        {
            weakText.text = $" - {entity.getDamageMod()}";
            weakText.gameObject.SetActive(true);
        }
    }
}
