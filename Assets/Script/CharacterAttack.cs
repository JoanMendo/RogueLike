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

        Vector3 mousePosition = Input.mousePosition; // Posición del mouse en la pantalla
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); // Convertirla a coordenadas del mundo

        // Obtener la posición del personaje
        Vector3 characterPosition = transform.position;

        // Calcular la dirección (normalizada)
        Vector2 direction = (mousePosition - characterPosition).normalized;
        return direction;

    }

    public IEnumerator Cooldown()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(AttackSpeed);
        OnCooldown = false;
    }
}
