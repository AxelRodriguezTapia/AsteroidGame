using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{
    public float shipSpeed = 10f; // Velocidad de la nave
    private Rigidbody rb; // Referencia al Rigidbody del objeto
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto de disparo (donde se instanciará la bala)

    // Start is called before the first frame update
    void Start()
    {
        // Obtener la referencia al Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener las entradas de movimiento horizontal y vertical
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        // Crear un vector para la dirección de movimiento
        Vector2 moveDirection = new Vector2(horizontal, vertical);

        // Aplicar la velocidad al Rigidbody para mover el objeto
        rb.velocity = moveDirection * shipSpeed;

        // Detectar si el jugador presiona el botón de disparo
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            // Llamar al método de disparo
            Fire();
        }
    }

    // Método para disparar
    void Fire()
    {
        Debug.Log("Disparo realizado");

        // Instanciar la bala en el punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Obtener el Rigidbody de la bala y darle velocidad en la dirección en la que está mirando la torreta
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = firePoint.up * 10f; // Ajusta la velocidad según sea necesario
    }
}