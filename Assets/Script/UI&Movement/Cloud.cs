using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Cloud : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 finalPosition;
    private GameObject player;
    private Coroutine coroutine;
    private bool isAtStart = true;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.instance.player;
        initialPosition = GameManager.instance.previousLevel.GetComponent<ProceduralTilemap>().cloudStartPosition;
        finalPosition = GameManager.instance.currentLevel.GetComponent<ProceduralTilemap>().cloudEndPosition;
        Debug.Log("Initial: " + initialPosition);
        Debug.Log("Final: " + finalPosition);
        StartCoroutine(initialPlacement());
    }

    public IEnumerator initialPlacement()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, 0.15f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(CloudMovement());
        }
    }

    private IEnumerator CloudMovement()
    {
        player.GetComponent<SortingGroup>().sortingOrder = 4;
        GameManager.instance.disableAllClouds(gameObject);
        if (isAtStart)
        {
            while (Vector3.Distance(transform.position, finalPosition) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, finalPosition, 0.15f);
                player.transform.position = transform.position + new Vector3(0, -0.6f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            isAtStart = false;
        }
        else
        {
            while (Vector3.Distance(transform.position, initialPosition) > 0.4f)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, 0.15f);
                player.transform.position = transform.position + new Vector3(0, -0.6f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            isAtStart = true;
        }
        if ((Vector3)GameManager.instance.currentLevel.GetComponent<ProceduralTilemap>().cloudEndPosition != finalPosition)
        {
            GameManager.instance.enableAllClouds();
        }
        else
        {
            disableCloud();
        }
        player.GetComponent<SortingGroup>().sortingOrder = 3;

    }

    public void disableCloud()
    {
        
        StartCoroutine(disableAnimation());
    }

    public void enableCloud()
    {
        GetComponent<Collider2D>().enabled = true;
        animator.Play("cloudEnable");
    }

    public IEnumerator disableAnimation()
    {
        GetComponent<Collider2D>().enabled = false;
        animator.Play("cloudDisable");
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);

    }




}
