using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementDirection : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5.0f;
    public float time = 1.2f;



    void Start()
    {
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }

    public IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(time);
        speed = -speed;
        if (enabled)
        yield return ChangeDirection();
    }
}
