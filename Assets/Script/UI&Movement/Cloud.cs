using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Cloud : MonoBehaviour
{
    private AudioSource audioSource;
    public Vector3 initialPosition;
    public Vector3 finalPosition;
    private GameObject player;
    private Coroutine coroutine;
    private bool isAtStart = true;
    private Animator animator;
    private GameObject childArow;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        childArow = transform.GetChild(0).gameObject;
        player = GameManager.instance.player;
        initialPosition = GameManager.instance.previousLevel.GetComponent<ProceduralTilemap>().cloudStartPosition;
        finalPosition = GameManager.instance.currentLevel.GetComponent<ProceduralTilemap>().cloudEndPosition;
        StartCoroutine(initialPlacement());
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    public IEnumerator initialPlacement()
    {
        audioSource.Play();
        while (Vector3.Distance(transform.position, initialPosition) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, 0.15f);
            yield return new WaitForSeconds(0.01f);
        }

        childArow.SetActive(true);
        changeArrowDirection(finalPosition);
        audioSource.Stop();

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
        EventManager.OnStartLoading();

        audioSource.Play();
        if (isAtStart)
        {

            while (Vector3.Distance(transform.position, finalPosition) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, finalPosition, 0.15f);
                player.transform.position = transform.position + new Vector3(0, -0.6f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            isAtStart = false;
            changeArrowDirection(initialPosition);
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
            changeArrowDirection(finalPosition);
        }
        if ((Vector3)GameManager.instance.currentLevel.GetComponent<ProceduralTilemap>().cloudEndPosition != finalPosition)
        {
            GameManager.instance.enableAllClouds();
        }
        else
        {
            disableCloud();
        }
        audioSource.Stop();
        player.GetComponent<SortingGroup>().sortingOrder = 3;
        EventManager.OnEndLoading();
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

    private void changeArrowDirection(Vector3 objective)
    {
        Vector3 direction = (objective - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        childArow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
