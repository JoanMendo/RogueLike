using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControler : AEntity
{
    public static event Action onPlayerLoad;
    public AudioClip characterDamaged;
    private Rigidbody2D rb;
    private Vector2 direction;
    public GameObject head;
    public GameObject body;
    public Sprite [] heads;
    private PlayerInput movementControls;
    private InputAction movement;
    private static CharacterControler instance;


    public void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        movementControls = GetComponent<PlayerInput>();
        movement = movementControls.actions["CharacterMovement"];
    }
    public void OnEnable()
    {
        direction = Vector2.zero;
        movement.performed += OnMovementPerformed;
        movement.canceled += OnMovementCanceled;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        
    }
    public void OnDisable()
    {
        direction = Vector2.zero;
        movement.performed -= OnMovementPerformed;
        movement.canceled -= OnMovementCanceled;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        maxHealth = health;
        rb = GetComponent<Rigidbody2D>();
        onPlayerLoad?.Invoke();
    } 

    private void Update()
    {
        rb.velocity = direction.normalized * speed;
    }

    public void OnMovementPerformed(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        rb.velocity = direction.normalized * speed;
        UpdateDirectionSprite();
    }

  public void OnMovementCanceled(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        rb.velocity = direction.normalized * speed;
        UpdateDirectionSprite();
    }

    private void UpdateDirectionSprite()
    {
        if (direction.x > 0) // Movimiento a la derecha
        {
            head.GetComponent<SpriteRenderer>().sprite = heads[0];
        }
        else if (direction.x < 0) // Movimiento a la izquierda
        {
            head.GetComponent<SpriteRenderer>().sprite = heads[0];
        }
        else if (direction.y > 0) // Movimiento hacia arriba
        {
            head.GetComponent<SpriteRenderer>().sprite = heads[1];
        }
        else if (direction.y < 0) // Movimiento hacia abajo
        {
            head.GetComponent<SpriteRenderer>().sprite = heads[2];
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded");
        onPlayerLoad?.Invoke();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        SoundManager.instance.PlayGlobalSound(characterDamaged);

    }

    public override void Die()
    {
        health = maxHealth;
        onTakeDamage.Invoke();
        GameManager.instance.resetGameManager();
        transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene("Lobby");
    }

}
