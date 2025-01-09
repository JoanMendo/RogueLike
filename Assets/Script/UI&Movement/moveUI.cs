using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUI : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5.0f;
    public float time = 1.5f;

    private RectTransform rectTransform;
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        StartCoroutine(ChangeDirection());
    }

    private void Update()
    {
        rectTransform.localPosition += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }

    public IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(time);
        speed = -speed;
        if (enabled)
            yield return ChangeDirection();
    }
}
