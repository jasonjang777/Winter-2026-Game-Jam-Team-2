using UnityEngine;

public class BasicProjectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private float maxProjectileLifetime = 3f;

    private float startTime;

    public virtual void Start()
    {
        startTime = Time.time;
        Destroy(gameObject, maxProjectileLifetime);
    }
    // Update is called once per frame
    public virtual void Update()
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
