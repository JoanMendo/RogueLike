using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Cloud : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 finalPosition;
    private GameObject player;
    private bool isAtStart = true;
    void Start()
    {
        player = GameManager.instance.player;
        initialPosition = GameManager.instance.previousLevel.GetComponent<ProceduralTilemap>().playerPosition;
        finalPosition = GameManager.instance.currentLevel.GetComponent<ProceduralTilemap>().playerPosition;
        StartCoroutine(initialPlacement());
    }

    public IEnumerator initialPlacement()
    {
        while (Vector3.Distance(transform.position, player.transform.position + new Vector3(0, -0.2f, 0)) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, 0.15f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player entered cloud");
            StartCoroutine(CloudMovement());
        }
    }

    public IEnumerator CloudMovement()
    {
        player.GetComponent<SortingGroup>().sortingOrder = 4;
        if (isAtStart)
        {
            while (Vector3.Distance(transform.position, finalPosition) > 0.05f)
            {
                Debug.Log("Moving");
                transform.position = Vector3.MoveTowards(transform.position, finalPosition, 0.15f);
                player.transform.position = transform.position + new Vector3(0, -0.2f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            isAtStart = false;
        }
        else
        {
            while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
            {
                Debug.Log("Moving2");
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, 0.15f);
                player.transform.position = transform.position + new Vector3(0, -0.2f, 0);
                yield return new WaitForSeconds(0.01f);
            }
            isAtStart = true;
        }
        player.GetComponent<SortingGroup>().sortingOrder = 3;

    }

}
