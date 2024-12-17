using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public static event System.Action onStartLoading;
    public static event System.Action onEndLoading;

    public void OnEnable()
    {
        StartCoroutine(StartLoading());
    }
    public static void OnStartLoading()
    {
        onStartLoading?.Invoke();
    }

    public static void OnEndLoading()
    {
        onEndLoading?.Invoke();
    }

    public IEnumerator StartLoading()
    {
        OnStartLoading();
        yield return new WaitForSeconds(4f);
        OnEndLoading();
    }
}
