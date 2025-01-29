using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector3 pos = gameObject.transform.InverseTransformPoint(other.transform.position);
        if(pos.x < -0.5 && pos.y < -0.5)
        {
            gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
       
        if(pos.x > 0.5 && pos.y < -0.5)
        {
            gameObject.transform.position = new Vector3(-pos.x, pos.y, pos.z);
        }

        if(pos.x < -0.5 && pos.y > 0.5){
            gameObject.transform.position = new Vector3(-pos.x, pos.y, pos.z);

        }

        if(pos.x  0.5 && pos.y > 0.5){
            gameObject.transform.position = new Vector3(-pos.x, pos.y, pos.z);

        }
    }
}