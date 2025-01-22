using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenWrapper : MonoBehaviour
{
    private Camera mainCamera; // Cámara principal
    private float screenWidth; // Ancho de la pantalla en unidades del mundo
    private float screenHeight; // Altura de la pantalla en unidades del mundo

    void Start()
    {
        // Obtener la referencia a la cámara principal
        mainCamera = Camera.main;

        // Obtener el ancho y la altura de la pantalla en unidades del mundo
        
    }

    void Update()
    {
        screenWidth = mainCamera.orthographicSize*mainCamera.aspect; 
        screenHeight = mainCamera.orthographicSize;
        // Obtener la posición del objeto en el mundo
        Vector3 objectPosition = transform.position;

        // Verificar si el objeto ha salido por el lado derecho
        if (objectPosition.x > screenWidth)
        {
            // Colocar el objeto en el lado izquierdo
            objectPosition.x = -screenWidth;
        }
        // Verificar si el objeto ha salido por el lado izquierdo
        else if (objectPosition.x < -screenWidth)
        {
            // Colocar el objeto en el lado derecho
            objectPosition.x = screenWidth;
        }

        // Verificar si el objeto ha salido por la parte superior
        if (objectPosition.y > screenHeight)
        {
            // Colocar el objeto en la parte inferior
            objectPosition.y = -screenHeight;
        }
        // Verificar si el objeto ha salido por la parte inferior
        else if (objectPosition.y < -screenHeight)
        {
            // Colocar el objeto en la parte superior
            objectPosition.y = screenHeight;
        }

        // Actualizar la posición del objeto
        transform.position = objectPosition;
    }
}