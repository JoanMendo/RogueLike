using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Proyectile;
    public float AttackSpeed;
    private bool OnCooldown = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !OnCooldown) //Si pulsa el click derecho y el cooldown no está activo
        {
            Attack();
            StartCoroutine(Cooldown());
        }
    }

    public void Attack()
    {

        GameObject proyectile = Instantiate(Proyectile, transform.position, Quaternion.identity);
        proyectile.GetComponent<Proyectile>().Direction = detectCursorPosition(); //Direcciona el proyectil hacia donde apunte el ratón


    }

    public Vector2 detectCursorPosition()
    {

        Vector3 mousePosition = Input.mousePosition; // Posición del mouse en la pantalla
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convertirla a coordenadas del mundo

        // Obtener la posición del personaje
        Vector3 characterPosition = transform.position;

        // Calcular la dirección (normalizada)
        Vector2 vect = mousePosition - characterPosition;
        Vector2 normalized = vect / (Mathf.Sqrt(vect.x * vect.x + vect.y * vect.y));
        return normalized;

    }

    public IEnumerator Cooldown() //Cooldown del arma
    {
        OnCooldown = true;
        yield return new WaitForSeconds(AttackSpeed);
        OnCooldown = false;
    }
}
