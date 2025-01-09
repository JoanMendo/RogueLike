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
    private InputAction dash;
    private bool isDashing = false;
    private Coroutine dashCoroutine;
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
        dash = movementControls.actions["CharacterDash"];

    }
    public void OnEnable()
    {
        direction = Vector2.zero;
        movement.performed += OnMovementPerformed;
        movement.canceled += OnMovementCanceled;
        dash.performed += OnDashPerformed;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        
    }
    public void OnDisable()
    {
        direction = Vector2.zero;
        movement.performed -= OnMovementPerformed;
        movement.canceled -= OnMovementCanceled;
        dash.performed -= OnDashPerformed;
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
        if (!isDashing)
        rb.velocity = direction.normalized * speed;
        else
            rb.velocity = direction.normalized * speed * 2.5f;
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

    public void OnDashPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Dash");
        if (dashCoroutine == null)
        {
            dashCoroutine = StartCoroutine(Dash());
        }

    }

    private IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(0.15f);
        isDashing = false;
        yield return new WaitForSeconds(0.5f);
        dashCoroutine = null;
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
