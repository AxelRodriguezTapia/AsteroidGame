using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstraX : MonoBehaviour
{
    static private ScreenBounds S; // Private but unprotected Singleton.
    public GameObject[] AsteroidPrefabs;
    public GameObject player; // Referencia al jugador
    public int numberOfAsteroids;
    public float spawnRate = 2.0f;
    public int clusterSize = 5; // Tamaño del grupo de asteroides
    public float clusterRadius = 0.5f; // Radio del grupo de asteroides

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroid()
    {
        Vector3 pos;
        do
        {
            pos = ScreenBounds.RANDOM_ON_SCREEN_LOC;
        } while (Vector3.Distance(pos, player.transform.position) < 5.0f);

        // Seleccionar un asteroide al azar para ser el padre
        var chosenAsteroid = AsteroidPrefabs[UnityEngine.Random.Range(0, AsteroidPrefabs.Length)];
        GameObject asteroidCluster = Instantiate(chosenAsteroid, pos, Quaternion.identity);
        asteroidCluster.name = "AsteroidCluster_" + System.Guid.NewGuid().ToString();

        // Añadir un Rigidbody al objeto padre si no tiene uno
        Rigidbody clusterRb = asteroidCluster.GetComponent<Rigidbody>();
        if (clusterRb == null)
        {
            clusterRb = asteroidCluster.AddComponent<Rigidbody>();
        }
        clusterRb.isKinematic = false;
        clusterRb.useGravity = false;

        for (int i = 0; i < clusterSize; i++)
        {
            Vector3 offset = Random.insideUnitSphere * clusterRadius;
            offset.z = 0; // Asegurarse de que los asteroides estén en el mismo plano
            Vector3 asteroidPos = pos + offset;
            var childAsteroid = AsteroidPrefabs[UnityEngine.Random.Range(0, AsteroidPrefabs.Length)];
            GameObject asteroid = Instantiate(childAsteroid, asteroidPos, Quaternion.identity);
            asteroid.transform.parent = asteroidCluster.transform; // Hacer que el asteroide sea hijo del objeto vacío

            // Hacer que los asteroides hijos sean cinemáticos para que se muevan con el padre
            Rigidbody rb = asteroid.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Desactivar el OffscreenWrapper de los asteroides hijos
            if (asteroid.GetComponent<OffScreenWrapper>() != null)
            {
                asteroid.GetComponent<OffScreenWrapper>().enabled = false;
            }
        }

        // Darle un empujoncito al asteroide padre
        asteroidCluster.GetComponent<Asteroid>().InitVelocity();

        Invoke("SpawnAsteroid", spawnRate);
    }
}