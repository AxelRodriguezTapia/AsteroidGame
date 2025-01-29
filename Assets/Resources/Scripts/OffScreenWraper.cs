using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;

public class OffScreenWrapper : MonoBehaviour
{
    private Camera mainCamera; // Cámara principal
    private ScreenBounds screenBounds; // Límites de la pantalla

    void Start()
    {
        // Obtener la referencia a la cámara principal
        mainCamera = Camera.main;
        screenBounds = GameObject.FindGameObjectWithTag("OnScreenBounds").GetComponent<ScreenBounds>();
        // Obtener el ancho y la altura de la pantalla en unidades del mundo
    }

    void Update()
    {
        
    }
    void OnTriggerExit(Collider other)
    {
        //InverseTransformPoint(transform.position); Relativa
        //TransformPoint(transform.position); Posicio Global
        Vector3 pos = screenBounds.transform.InverseTransformPoint(transform.position);
        if (Math.Abs(pos.x) > 0.5)
        {
            pos.x *= -1;
        }
        if (Math.Abs(pos.y) > 0.5)
        {
            pos.y *= -1;
        }
        
        gameObject.transform.position = screenBounds.transform.TransformPoint(pos);

        
    }
}