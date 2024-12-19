using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableScriptOnProximity : MonoBehaviour
{
    public float distance = 5.0f;
    public MonoBehaviour[] scripts;
    public GameObject target;

    public void OnEnable()
    {
        CharacterControler.onPlayerLoad += addPlayer;
    }

    public void OnDisable()
    {
        CharacterControler.onPlayerLoad -= addPlayer;
    }
    public void addPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        if (target == null)
        {
            return;
        }
        foreach (MonoBehaviour script in scripts)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < distance)
            {
                script.enabled = true;
            }
            else
            { 
                script.enabled = false;
            }
        }
    }
}
