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
        if (Input.GetMouseButtonDown(0) && !OnCooldown)
        {
            Attack();
            StartCoroutine(Cooldown());
        }
    }

    public void Attack()
    {

        GameObject proyectile = Instantiate(Proyectile, transform.position, Quaternion.identity);
        proyectile.GetComponent<Proyectile>().Direction = detectCursorPosition();


    }

    public Vector2 detectCursorPosition()
    {

        Vector3 mousePosition = Input.mousePosition; // Posici�n del mouse en la pantalla
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convertirla a coordenadas del mundo

        // Obtener la posici�n del personaje
        Vector3 characterPosition = transform.position;

        // Calcular la direcci�n (normalizada)
        Vector2 vect = mousePosition - characterPosition;
        Debug.Log(vect);
        Vector2 direction = (vect).normalized;
        Debug.Log(direction);
        Vector2 normalized = vect / (Mathf.Sqrt(vect.x * vect.x + vect.y * vect.y));
        Debug.Log(normalized);
        return normalized;

    }

    public IEnumerator Cooldown()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(AttackSpeed);
        OnCooldown = false;
    }
}
