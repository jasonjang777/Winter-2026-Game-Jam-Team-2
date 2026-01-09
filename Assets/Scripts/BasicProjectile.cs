using UnityEngine;

public class BasicProjectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float maxProjectileLifetime = 3f;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        Destroy(gameObject, maxProjectileLifetime);
    }
    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.position = transform.position + (transform.forward * Time.deltaTime * projectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {   
        // Debug.Log("platform hit");
        Destroy(gameObject);
    }

    public float getProjectileLifetime()
    {
        return Time.time - startTime;
    }
}
