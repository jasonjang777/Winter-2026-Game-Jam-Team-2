using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class FireController : MonoBehaviour
{       
    // Projectiles
    [SerializeField] private KeyCode fireKeyCode = KeyCode.Mouse0;
    [SerializeField] private GameObject cameraObject;
    private float nextFireTime = 0f; 

    [SerializeField] private float minHealthThreshold = 15f;
    [SerializeField] private float weaponSwitchCooldown = 1f;
    private float nextSwitchTime = 0f;
    private int activeProjectileCode = 1;

    // Basic Projectile
    [SerializeField] private GameObject basicProjectilePrefab;   
    [SerializeField] private float basicProjectileFireCooldown = 0.5f;
    [SerializeField] private float basicProjectileCost = 2.5f;
    [SerializeField] private KeyCode basicProjectileKeybind = KeyCode.Alpha1;


    // Platform Gun
    [SerializeField] private GameObject platformProjectilePrefab;   
    [SerializeField] private GameObject spawnedPlatformPrefab;   
    [SerializeField] private float platformProjectileFireCooldown = 2f;
    [SerializeField] private float platformProjectileCost = 5f;
    [SerializeField] private float spawnPlatformCost = 20f;
    [SerializeField] private float spawnPlatformInitialDelay = 0.25f;
    [SerializeField] private Vector3 platformScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private KeyCode platformProjectileKeybind = KeyCode.Alpha2;
    [SerializeField] private KeyCode spawnPlatformKeybind = KeyCode.Mouse1;

    // Player Script
    public PlayerController controllerScriptRef;

    // Update is called once per frame
    void Update()
    {
        // Switch Weapons

        // if(activeProjectileCode != 1 && Input.GetKey(basicProjectileKeybind) && Time.time >= nextSwitchTime)
        // {
        //     SwitchProjectile(1);
        // }
        // else if(activeProjectileCode != 2 && Input.GetKey(platformProjectileKeybind) && Time.time >= nextSwitchTime)
        // {
        //     SwitchProjectile(2);
        // }

        // Fire projectiles
        float currPlayerHealth = controllerScriptRef.getHealth();
        if(Input.GetKey(fireKeyCode) && Time.time >= nextFireTime && activeProjectileCode == 1 &&
        currPlayerHealth - basicProjectileCost >= minHealthThreshold) // Prevent player from depleting their own HP too much
        {
            FireBasicProjectile();
        }
        
        // else if(Input.GetKey(fireKeyCode) && Time.time >= nextFireTime && activeProjectileCode == 2 &&
        // currPlayerHealth - platformProjectileCost >= minHealthThreshold)
        // {
        //     FirePlatformProjectile();
        // }

        // Spawn platform from projectile (ENSURE ONLY 1 PLATFORM PROJECTILE IS ACTIVE AT ALL TIMES)
        // else if(Input.GetKey(spawnPlatformKeybind) 
        // && currPlayerHealth - spawnPlatformCost >= minHealthThreshold)
        // {
        //     SpawnPlatform();
        // }
    }
    
    void SwitchProjectile(int code)
    {
        activeProjectileCode = code;
        Debug.Log(activeProjectileCode);
        nextSwitchTime = Time.time + weaponSwitchCooldown;
    }
    void FireBasicProjectile()
    {
        GameObject spawnedBasicProjectile = Instantiate<GameObject>(basicProjectilePrefab, transform.position, cameraObject.transform.rotation);
        nextFireTime = Time.time + basicProjectileFireCooldown;
        controllerScriptRef.applyDamage(basicProjectileCost);
    }

    void FirePlatformProjectile()
    {
        GameObject existingPlatformProjectile = GameObject.Find("PlatformProjectile(Clone)");
        if(existingPlatformProjectile)
        {
            Destroy(existingPlatformProjectile);
        }
        GameObject spawnedPlatformProjectile = Instantiate<GameObject>(platformProjectilePrefab, transform.position, cameraObject.transform.rotation);
        nextFireTime = Time.time + platformProjectileFireCooldown;
        controllerScriptRef.applyDamage(platformProjectileCost);
    }

    bool platformSpawnQueued = false;
    void SpawnPlatform()
    {
        GameObject existingPlatformProjectile = GameObject.Find("PlatformProjectile(Clone)");
        if(existingPlatformProjectile && !platformSpawnQueued) {
            BasicProjectile ProjectileScript = existingPlatformProjectile.GetComponent<BasicProjectile>();
            if (ProjectileScript.getProjectileLifetime() >= spawnPlatformInitialDelay)
            {
                Vector3 currentRotation = existingPlatformProjectile.transform.rotation.eulerAngles;
                GameObject spawnedPlatform = Instantiate<GameObject>(spawnedPlatformPrefab, existingPlatformProjectile.transform.position, 
                Quaternion.Euler(0, currentRotation.y, 0));
                spawnedPlatform.transform.localScale = platformScale;
                Destroy(existingPlatformProjectile);
                controllerScriptRef.applyDamage(spawnPlatformCost);
            }
            else
            {
                StartCoroutine(SpawnPlatformAfterDelay(existingPlatformProjectile, 
                spawnPlatformInitialDelay - ProjectileScript.getProjectileLifetime()));
            }
        }
    }

    IEnumerator SpawnPlatformAfterDelay(GameObject existingPlatformProjectile, float delay)
    {
        platformSpawnQueued = true;
        yield return new WaitForSeconds(delay);
        Vector3 currentRotation = existingPlatformProjectile.transform.rotation.eulerAngles;
        GameObject spawnedPlatform = Instantiate<GameObject>(spawnedPlatformPrefab, existingPlatformProjectile.transform.position, 
        Quaternion.Euler(0, currentRotation.y, 0));
        spawnedPlatform.transform.localScale = platformScale;
        Destroy(existingPlatformProjectile);
        controllerScriptRef.applyDamage(spawnPlatformCost);
        platformSpawnQueued = false;
        yield break;
    }
}
