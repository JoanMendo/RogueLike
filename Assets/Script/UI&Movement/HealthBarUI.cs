using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private AEnemy parent;
    private float initialHealth;
    private Vector3 initialSize;
    void Start()
    {
        parent = transform.parent.GetComponent<AEnemy>();
        initialHealth = parent.health;
        initialSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale = new Vector3(initialSize.x * parent.health / initialHealth, initialSize.y, initialSize.z);

    }
}
