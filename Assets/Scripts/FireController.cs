using UnityEngine;

public class FireController : MonoBehaviour
{       
    // Projectiles
    [SerializeField] private KeyCode fireKeyCode = KeyCode.Mouse0;
    [SerializeField] private GameObject cameraObject;
    private float nextFireTime = 0f; 


    // Basic Projectile
    [SerializeField] private GameObject basicProjectilePrefab;   
    [SerializeField] private float basicProjectileFireCooldown = 0.5f;


    // Update is called once per frame
    void Update()
    {
        // Fire projectiles
        if(Input.GetKey(fireKeyCode) && Time.time >= nextFireTime)
        {
            FireBasicProjectile();
        }
    }
    
    void FireBasicProjectile()
    {
        GameObject spawnedProjectile = Instantiate<GameObject>(basicProjectilePrefab, transform.position, cameraObject.transform.rotation);
        nextFireTime = Time.time + basicProjectileFireCooldown;
    }
}
