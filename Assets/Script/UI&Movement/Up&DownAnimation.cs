using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownAnimation : MonoBehaviour
{
    public float speed = 5.0f;
    public float time = 1.2f;

    void Start()
    { 
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }

    public IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(time);
        speed = -speed;
        yield return ChangeDirection();
    }
}
