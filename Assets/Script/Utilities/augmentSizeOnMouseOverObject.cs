using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class augmentSizeOnMouseOverObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float scaleMultiplier = 1.1f;
    public float scaleTime = 0.2f;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private Coroutine coroutine;
    private bool isUIElement = false;
    private bool isGrowing = false;

    void Start()
    {
        if (GetComponent<RectTransform>() != null)
        {
            isUIElement = true;
        }
        if (!isUIElement)
        {
            originalScale = transform.localScale;
            targetScale = originalScale * scaleMultiplier;
        }
        else
        {
            originalScale = GetComponent<RectTransform>().localScale;
        }
        targetScale = originalScale * scaleMultiplier;
    }

    void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            if (isGrowing && gameObject.activeInHierarchy)
            {
                isGrowing = false;
                StartCoroutine(reduceSize());
            }
        }
    }


    private void OnMouseOver()
    {
        if (!isGrowing)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            if (enabled)
            {
                isGrowing = true;
                coroutine = StartCoroutine(augmentSize());
            }
        }
       
           
    }


    private void OnMouseExit()
    {
        if (isGrowing)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            if (enabled && isGrowing)
            {
                isGrowing = false;
                coroutine = StartCoroutine(reduceSize());
            }
        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if  (!enabled)
        {
            return;
        }
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(augmentSize());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!enabled)
        {
            return;
        }
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(reduceSize());
    }
    public IEnumerator augmentSize()
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            if (!isUIElement)
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, (scaleMultiplier - 1) / scaleTime * Time.deltaTime);
            else 
            {
                GetComponent<RectTransform>().localScale = Vector3.MoveTowards(GetComponent<RectTransform>().localScale, targetScale, (scaleMultiplier - 1) / scaleTime * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator reduceSize()
    {
        while (Vector3.Distance(transform.localScale, originalScale) > 0.01f)
        {
            if (!isUIElement)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, originalScale, (scaleMultiplier - 1) / scaleTime * Time.deltaTime);
            }

            else 
            {
                GetComponent<RectTransform>().localScale = Vector3.MoveTowards(GetComponent<RectTransform>().localScale, originalScale, (scaleMultiplier - 1) / scaleTime * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
