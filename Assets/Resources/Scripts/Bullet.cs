using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody2D de la bala
    private Vector2 direction; // Ángulo de la dirección de la bala

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = rb.velocity * speed;
        // Destruir la bala después de un tiempo (por ejemplo, 5 segundos)
        Invoke("DestroyBullet",2f);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}