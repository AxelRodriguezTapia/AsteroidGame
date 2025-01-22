using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPointAtMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la posición del ratón en el espacio de la cámara
        Vector3 mousePos = Input.mousePosition;

        // Ajustar la distancia del ratón desde la cámara (en este caso, z = 10, según la posición de la cámara)
        mousePos.z = 10f;  // Ajusta esta distancia dependiendo de tu cámara y cómo está configurada

        // Convertir la posición del ratón de la pantalla a coordenadas del mundo
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calcular la dirección desde el objeto hacia el ratón
        Vector3 direction = worldMousePos - transform.position;
        direction.z = 0;  // Mantener la rotación en 2D (no rotar en el eje Z)

        // Calcular la rotación en el plano 2D
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplicar la rotación solo en el eje Z
        transform.rotation = Quaternion.Euler(-angle, 90, -90);
    }
}