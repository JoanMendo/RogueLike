using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class augmentSizeOnMouseOverObject : MonoBehaviour
{

    public float scaleMultiplier = 1.5f;
    public float scaleTime = 0.5f;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isMouseOver = false;
    private Coroutine coroutine;

    void Start()
    {

        originalScale = transform.localScale;
        targetScale = originalScale * scaleMultiplier;


    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnMouseEnter()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(augmentSize());
    }

    private void OnMouseExit()
    {
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
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, (scaleMultiplier - 1) / scaleTime * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator reduceSize()
    {
        while (Vector3.Distance(transform.localScale, originalScale) > 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, originalScale, (scaleMultiplier - 1) / scaleTime * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
