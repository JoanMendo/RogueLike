using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
    using UnityEngine;

public class moveUIOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public GameObject objectToMove;
    private Vector3 initialPosition;
    public Vector3 distanceToMove;
    public float speed;
    private Coroutine coroutine;

    public void Start()
    {
        initialPosition = objectToMove.GetComponent<RectTransform>().localPosition;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(MoveObjectToPosition(distanceToMove));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(MoveObjectToPosition(initialPosition));
    }
    public IEnumerator MoveObjectToPosition(Vector3 targetPosition)
    {
       
        float step = speed * Time.deltaTime;
        while (Vector3.Distance(objectToMove.GetComponent<RectTransform>().localPosition, targetPosition) > 2f)
        {

            objectToMove.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(objectToMove.GetComponent<RectTransform>().localPosition, targetPosition, step);
            yield return null;
        }
    }
}
