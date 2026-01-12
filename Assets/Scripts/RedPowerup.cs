using System;
using UnityEngine;

public class RedPowerup : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explosionRadius = 20f;
    // [SerializeField] private float explosionDamage = 50f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            // Debug.Log("Red Powerup Hit");
            Explode();
        }
    }

    void Explode()
    {
        if(explosionPrefab)
        {
            GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        // Apply damage to enemies in radius

        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in detectedColliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Wraith wraithScript = collider.gameObject.GetComponent<Wraith>();
                wraithScript.Explode();
            }
        }

        Destroy(gameObject);
    }
}
