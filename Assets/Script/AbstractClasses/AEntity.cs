using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    public float health;
    public float speed;
    private GameObject Canvas;
    public GameObject floatingTextPrefab;

    public void Awake()
    {
        Canvas = GameObject.Find("Canvas");
    }


    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        CreateFloatingText(damage);
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void CreateFloatingText(float Damage)
    {

        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, Canvas.transform);
        go.GetComponent<TMP_Text>().text = Damage.ToString();
    }

}
