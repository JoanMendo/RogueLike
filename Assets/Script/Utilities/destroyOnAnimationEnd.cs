using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnAnimationEnd : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(DestroyOnAnimationEnd());
    }

    
    private IEnumerator DestroyOnAnimationEnd()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
