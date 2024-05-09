using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public GameObject ui;
    public AudioSource src;
    public AudioClip hoverAudio;

    public GameObject panel;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI vulnerableText;
    public TextMeshProUGUI weakText;
    public TextMeshProUGUI frailText;
    public TextMeshProUGUI artifactText;
    
    public void ActivateUI(Entity entity, Vector3 spawnLocation)
    {
        src.clip = hoverAudio;
        src.Play();
        
        transform.position = spawnLocation;
        
        DisplayInfo(entity);
    }

    public void HideUI()
    {
        DisableText(shieldText);
        DisableText(vulnerableText);
        DisableText(weakText);
        DisableText(frailText);
        DisableText(artifactText);
        ui.SetActive(false);
    }

    void DisplayInfo(Entity entity)
    {
        hpText.text = $"HP - {entity.getHP()} / {entity.maxHP}";

        if (entity.getShield() != 0)
        {
            shieldText.text = $"Shield - {entity.getShield()}";
            EnableText(shieldText);
        }

        if (entity.getVulnerable() != 0)
        {
            vulnerableText.text = $"Vulnerable - {entity.getVulnerable()}";
            EnableText(vulnerableText);
        }

        if (entity.getWeak() != 0)
        {
            weakText.text = $"Weak - {entity.getWeak()}";
            EnableText(weakText);
        }
        
        if (entity.getFrail() != 0)
        {
            frailText.text = $"Frail - {entity.getFrail()}";
            EnableText(frailText);
        }
        
        if (entity.getArtifact() != 0)
        {
            artifactText.text = $"Artifact - {entity.getArtifact()}";
            EnableText(artifactText);
        }
        
        Debug.Log("Info edited");
        
        AdjustElements();
        ui.SetActive(true);
        Debug.Log("UI shown");
    }

    void DisableText(TextMeshProUGUI textObj)
    {
        textObj.transform.parent.gameObject.SetActive(false);
    }

    void EnableText(TextMeshProUGUI textObj)
    {
        textObj.transform.parent.gameObject.SetActive(true);
    }

    void AdjustElements()
    {
        GridLayoutGroup grid = GetComponentInChildren<GridLayoutGroup>(true);
        if (grid == null)
        {
            Debug.Log("grid does not exist :(");
            return;
        }

        int childCount = 0;
        foreach (Transform child in panel.transform)
        {
            if (child.gameObject.activeSelf)
            {
                childCount += 1;
            }
        }
        
        if (childCount > 3)
        {
            grid.cellSize = new Vector2(48.5f, 40 / 3f);
            grid.padding.left = 3;
            grid.padding.right = 3;
        }
        else
        {
            grid.cellSize = new Vector2(100f, 40 / 3f);
            grid.padding.left = 0;
            grid.padding.right = 0;
        }
    }
}
