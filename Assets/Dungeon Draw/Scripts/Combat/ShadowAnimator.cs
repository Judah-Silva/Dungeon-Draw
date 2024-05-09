using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimator : MonoBehaviour
{
    public Transform shadowTransform;
    public float scaleSpeed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        shadowTransform = gameObject.transform;
        StartCoroutine(IdleAnimation());
    }

    IEnumerator IdleAnimation()
    {
        Vector3 originalScale = shadowTransform.localScale;
        
        while (true)
        {
            while (shadowTransform.localScale.x > 2)
            {
                float newScale = shadowTransform.localScale.x - scaleSpeed * Time.deltaTime;
                shadowTransform.localScale = new Vector3(newScale, originalScale.y, originalScale.z);
                yield return null;
            }

            while (shadowTransform.localScale.x < originalScale.x)
            {
                float newScale = shadowTransform.localScale.x + scaleSpeed * Time.deltaTime;
                shadowTransform.localScale = new Vector3(newScale, originalScale.y, originalScale.z);
                yield return null;
            }
        }
    }
}
