using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{
    public float shipSpeed = 10f; // Velocidad de la nave
    private Rigidbody rb; // Referencia al Rigidbody2D del objeto
    // Start is called before the first frame update
    void Start()
    {
        // Obtener la referencia al Rigidbody2D
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
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            // Llamar al método de disparo
            Fire();
        }
    }

    // Método para disparar (puedes implementar la lógica de disparo aquí)
    void Fire()
    {
        Debug.Log("Disparo realizado");
        // Aquí puedes instanciar un proyectil, generar efectos, etc.
    }
}
