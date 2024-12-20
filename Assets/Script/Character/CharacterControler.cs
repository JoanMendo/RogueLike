using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControler : AEntity
{
    public static event Action onPlayerLoad;
    private Rigidbody2D rb;
    
    private Vector2 direction;
    public GameObject head;
    public GameObject body;
    public Sprite [] heads;
    private PlayerInput movementControls;
    private InputAction movement;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        onPlayerLoad?.Invoke();
        movementControls = GetComponent<PlayerInput>();
        movement = movementControls.actions["CharacterMovement"];
        movement.performed += OnMovementPerformed;
        movement.canceled += OnMovementCanceled;

        DontDestroyOnLoad(gameObject);
    }

    public void OnDisable()
    {
        direction = Vector2.zero;
        movement.performed -= OnMovementPerformed;
        movement.canceled -= OnMovementCanceled;
    }

    public void OnEnable()
    {
        direction = Vector2.zero;
        movement.performed += OnMovementPerformed;
        movement.canceled += OnMovementCanceled;
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
}
