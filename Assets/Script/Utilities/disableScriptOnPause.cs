using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableScriptOnPause : MonoBehaviour
{
    public MonoBehaviour[] scripts;
    private void OnEnable()
    {
        EventManager.onStartLoading += disableScripts;
        EventManager.onEndLoading += enableScripts;
    }

    private void OnDisable()
    {
        EventManager.onStartLoading -= disableScripts;
        EventManager.onEndLoading -= enableScripts;
    }

    private void disableScripts()
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    private void enableScripts()
    {
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }
}
