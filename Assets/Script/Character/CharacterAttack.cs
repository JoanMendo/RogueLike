using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Proyectile;
    public ParticleSystem ParticleSystem;
    public ScriptableObject weaponSO;
    public bool OnCooldown = false;
    public bool isFlameThrower = false;
    private bool isParticlePlaying = false;
    private PlayerInput attackControls;
    private InputAction attack;
    public Queue<GameObject> proyectilesQueue = new Queue<GameObject>();

    // Update is called once per frame

    private void Start()
    {
        attackControls = GetComponent<PlayerInput>();
        attack = attackControls.actions["CharacterAttack"];
        attack.performed += OnAttackPerformed;
        attack.canceled += OnAttackCanceled;
    }

    public void Update()
    {
        if (isFlameThrower)
        {
            float flamesDirection = Mathf.Atan2(detectCursorPosition().x, detectCursorPosition().y) * Mathf.Rad2Deg - 90;
            ParticleSystem.transform.rotation = Quaternion.Euler(flamesDirection, 90, 90);
            ParticleSystem.startRotation = flamesDirection * Mathf.Deg2Rad;
        }
    }

    public void OnEnable()
    {
        CharacterControler.onPlayerLoad += OnPlayerLoad;
        attack.performed += OnAttackPerformed;
        attack.canceled += OnAttackCanceled;
        

    }
    public void OnDisable()
    {
        if (isParticlePlaying)
        {
            isParticlePlaying = false;
            ParticleSystem.Stop();
            ParticleSystem.GetComponentInChildren<Collider2D>().enabled = false;
        }
        CharacterControler.onPlayerLoad -= OnPlayerLoad;
        attack.performed -= OnAttackPerformed;
        attack.canceled -= OnAttackCanceled;
        

    }

    


    void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (!isFlameThrower)
        {
            if (!OnCooldown) //Si pulsa el click derecho y el cooldown no est� activo
            {
                CreateProyectile();
                
            }
        }
        else if (!isParticlePlaying)
        {
            isParticlePlaying = true;
            ParticleSystem.Play();
            ParticleSystem.GetComponentInChildren<Collider2D>().enabled = true;
        }
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        if (isFlameThrower)
        {
            isParticlePlaying = false;
            ParticleSystem.Stop();
            ParticleSystem.GetComponentInChildren<Collider2D>().enabled = false;
        }
    }

    public void CreateProyectile()
    {
        GameObject proyectile;
        Proyectile.GetComponent<AWeapon>().weaponSO = weaponSO;
        if (proyectilesQueue.Count == 0 || proyectilesQueue.Peek().activeInHierarchy)
        {
            proyectile = Instantiate(Proyectile, transform.position, Quaternion.identity);
        }
        else
        {
            proyectile = proyectilesQueue.Dequeue();
            proyectile.transform.position = transform.position;
            proyectile.SetActive(true); 
        }
        proyectile.GetComponent<AWeapon>().Direction = detectCursorPosition(); //Direcciona el proyectil hacia donde apunte el rat�n
        proyectile.GetComponent<AWeapon>().SetWeapon(weaponSO); //Setea el arma
        proyectilesQueue.Enqueue(proyectile);
        StartCoroutine(Cooldown(proyectile.GetComponent<AWeapon>().AttackSpeed));
    }

    public Vector2 detectCursorPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue(); // Posici�n del mouse en la pantalla
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convertirla a coordenadas del mundo
        Vector3 characterPosition = transform.position;
        Vector2 vect = mousePosition - characterPosition;
        Vector2 normalized = vect / Mathf.Sqrt(vect.x * vect.x + vect.y * vect.y);
        return normalized;
    }

    public IEnumerator Cooldown(float attackSpeed) //Cooldown del arma
    {
        OnCooldown = true;
        yield return new WaitForSeconds(attackSpeed);
        OnCooldown = false;
    }

    public Vector2 randomizeDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += UnityEngine.Random.Range(-35, 35);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
    public void OnPlayerLoad()
    {
        Debug.Log("Player loaded");
        proyectilesQueue.Clear();
    }
}
