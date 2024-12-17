using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Proyectile;
    public ParticleSystem ParticleSystem;
    public float AttackSpeed;
    public bool OnCooldown = false;
    public bool isParticleSystem = false;
    private bool isParticlePlaying = false;
    private PlayerInput attackControls;
    private InputAction attack;

    // Update is called once per frame

    private void Start()
    {
        attackControls = GetComponent<PlayerInput>();
        attack = attackControls.actions["Attack"];
        attack.performed += OnAttackPerformed;
        attack.canceled += OnAttackCanceled;
    }

    void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (!isParticleSystem)
        {
            if (!OnCooldown) //Si pulsa el click derecho y el cooldown no está activo
            {
                CreateProyectile();
                StartCoroutine(Cooldown());
            }
        }
        else if (!isParticlePlaying)
        {
            isParticlePlaying = true;
            ParticleSystem.Play();
            ParticleSystem.GetComponentInChildren<Collider2D>().enabled = true;
        }
        float flamesDirection = Mathf.Atan2(detectCursorPosition().x, detectCursorPosition().y) * Mathf.Rad2Deg - 90;
        ParticleSystem.transform.rotation = Quaternion.Euler(flamesDirection, 90, 90);
        ParticleSystem.startRotation = flamesDirection * Mathf.Deg2Rad;
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        if (isParticleSystem)
        {
            isParticlePlaying = false;
            ParticleSystem.Stop();
            ParticleSystem.GetComponentInChildren<Collider2D>().enabled = false;
        }
    }

    public void CreateProyectile()
    {

        GameObject proyectile = Instantiate(Proyectile, transform.position, Quaternion.identity);
        proyectile.GetComponent<AWeapon>().Direction = detectCursorPosition(); //Direcciona el proyectil hacia donde apunte el ratón
    }



    public Vector2 detectCursorPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue(); // Posición del mouse en la pantalla
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convertirla a coordenadas del mundo
        Vector3 characterPosition = transform.position;
        Vector2 vect = mousePosition - characterPosition;
        Vector2 normalized = vect / Mathf.Sqrt(vect.x * vect.x + vect.y * vect.y);
        return normalized;

    }

    public IEnumerator Cooldown() //Cooldown del arma
    {
        OnCooldown = true;
        yield return new WaitForSeconds(AttackSpeed);
        OnCooldown = false;
    }

    public Vector2 randomizeDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += Random.Range(-35, 35);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
