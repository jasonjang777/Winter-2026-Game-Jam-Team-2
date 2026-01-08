using UnityEditor.Build;
using UnityEngine;

public class FireController : MonoBehaviour
{       
    // Projectiles
    [SerializeField] private KeyCode fireKeyCode = KeyCode.Mouse0;
    [SerializeField] private GameObject cameraObject;
    private float nextFireTime = 0f; 

    [SerializeField] private float minHealthThreshold = 15f;
    // Basic Projectile
    [SerializeField] private GameObject basicProjectilePrefab;   
    [SerializeField] private float basicProjectileFireCooldown = 0.5f;
    [SerializeField] private float basicProjectileCost = 2.5f;

    // Player Script
    public PlayerController controllerScriptRef;


    // Update is called once per frame
    void Update()
    {
        // Fire projectiles
        float currPlayerHealth = controllerScriptRef.getHealth();
        if(Input.GetKey(fireKeyCode) && Time.time >= nextFireTime && 
        currPlayerHealth - basicProjectileCost >= minHealthThreshold) // Prevent player from depleting their own HP too much
        {
            FireBasicProjectile();
        }
    }
    
    void FireBasicProjectile()
    {
        GameObject spawnedBasicProjectile = Instantiate<GameObject>(basicProjectilePrefab, transform.position, cameraObject.transform.rotation);
        nextFireTime = Time.time + basicProjectileFireCooldown;
        controllerScriptRef.applyDamage(basicProjectileCost);
    }
}
