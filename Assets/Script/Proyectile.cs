using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public Vector2 Direction;
    public float Speed;
    public float Damage;
    public GameObject deathParticle;
    public GameObject floatingTextPrefab;
    private GameObject Canvas;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Direction * Speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" )
        {
            collision.gameObject.GetComponent<SlimeMovement>().TakeDamage(Damage);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            CreateFloatingText();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Collider" )
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void CreateFloatingText()
    {

        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, Canvas.transform);
        go.GetComponent<TMP_Text>().text = Damage.ToString();
    }
}
