using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> enemyList = new List<GameObject>();

    void Awake()
    {
        
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
