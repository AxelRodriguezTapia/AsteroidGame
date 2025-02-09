using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OffScreenWrapper))]
public class Asteroid : MonoBehaviour
{
    [Header("Set Dynamically")]
    public int size = 3;

    public int minVel = 5;
    public int maxVel = 10;
    public int maxAngularVel = 10;

    Rigidbody rigid;
    OffScreenWrapper offScreenWrapper;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        offScreenWrapper = GetComponent<OffScreenWrapper>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Random.rotation;
        InitVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitVelocity()
    {
        Vector3 vel;

        // The initial velocity depends on whether the Asteroid is currently off screen or not
        if (ScreenBounds.OOB(transform.position))
        {
            // If the Asteroid is out of bounds, just point it toward a point near the center of the sceen
            vel = ((Vector3)Random.insideUnitCircle * 4) - transform.position;
            vel.Normalize();
        }
        else
        {
            // If in bounds, choose a random direction, and make sure that when you Normalize it, it doesn't
            //  have a length of 0 (which might happen if Random.insideUnitCircle returned [0,0,0].
            do
            {
                vel = Random.insideUnitCircle;
                vel.Normalize();
            } while (Mathf.Approximately(vel.magnitude, 0f));
        }

        // Multiply the unit length of vel by the correct speed (randomized) for this size of Asteroid
        vel = vel * Random.Range(this.minVel, this.maxVel) / (float)size;
        rigid.velocity = vel;

        rigid.angularVelocity = Random.insideUnitSphere * this.maxAngularVel;
    }

    public void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;

        if (otherGO.tag == "Bullet" || otherGO.tag == "Player")
        {
            if (transform.parent != null)
            {
                // Delegar la colisión al padre
                transform.parent.GetComponent<Asteroid>().AsteroidHitByBullet(otherGO);
            }
            else
            {
                // Manejar la colisión si no hay padre
                AsteroidHitByBullet(otherGO);
            }
        }
        else if (otherGO.tag == "Asteroid")
        {
            //Destroy(otherGO);
            //Destroy(gameObject);
        }
    }

    void AsteroidHitByBullet(GameObject otherGO)
    {
        if (otherGO.tag == "Player"){
            
        }else if (otherGO.tag == "Bullet")
        {
            Destroy(otherGO);
        }
        

        if (transform.childCount > 0)
        {
            // Dividir el cluster en dos nuevos clusters
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }

            // Crear dos nuevos clusters
            CreateNewCluster(children.GetRange(0, children.Count / 2));
            CreateNewCluster(children.GetRange(children.Count / 2, children.Count - children.Count / 2));
        }

        Destroy(gameObject);
    }

    void CreateNewCluster(List<Transform> children)
    {
        if (children.Count == 0) return;

        // Seleccionar un asteroide al azar para ser el padre del nuevo cluster
        Transform newParent = children[0];
        newParent.SetParent(null);

        // Añadir un Rigidbody al nuevo padre si no tiene uno
        Rigidbody clusterRb = newParent.GetComponent<Rigidbody>();
        if (clusterRb == null)
        {
            clusterRb = newParent.gameObject.AddComponent<Rigidbody>();
        }
        clusterRb.isKinematic = false;
        clusterRb.useGravity = false;

        // Activar el OffScreenWrapper del nuevo padre
        OffScreenWrapper offscreenWrapper = newParent.GetComponent<OffScreenWrapper>();
        if (offscreenWrapper != null)
        {
            offscreenWrapper.enabled = true;
        }

        // Asignar los hijos al nuevo padre
        foreach (Transform child in children)
        {
            if (child != newParent)
            {
                child.SetParent(newParent);
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                offscreenWrapper = child.GetComponent<OffScreenWrapper>();
                if (offscreenWrapper != null)
                {
                    offscreenWrapper.enabled = false;
                }
            }
        }

        // Darle un empujoncito al nuevo padre
        newParent.GetComponent<Asteroid>().InitVelocity();
    }

    void AsteroidHitByPlayer(GameObject otherGO)
    {
        Destroy(gameObject);
    }
}