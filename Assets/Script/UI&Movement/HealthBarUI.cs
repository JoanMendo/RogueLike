using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private AEntity parent;
    private float initialHealth;
    private Vector3 initialSize;
    void Start()
    {
        parent = transform.parent.GetComponent<AEntity>();
        initialHealth = parent.health;
        initialSize = transform.localScale;
    }


    public void updateHealthUI()
    {
        transform.localScale = new Vector3(initialSize.x * parent.health / initialHealth, initialSize.y, initialSize.z);
    }

}
