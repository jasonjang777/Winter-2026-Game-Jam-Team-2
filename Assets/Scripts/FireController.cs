using UnityEngine;

public class FireController : MonoBehaviour
{       
    // Projectiles
    [SerializeField] private KeyCode fireKeyCode = KeyCode.Mouse0;
    [SerializeField] private GameObject basicProjectilePrefab;   
    [SerializeField] private GameObject cameraObject;

    // Update is called once per frame
    void Update()
    {
        // Fire projectiles
        if(Input.GetKeyDown(fireKeyCode))
        {
            FireBasicProjectile();
        }
    }
    
    void FireBasicProjectile()
    {
        GameObject spawnedProjectile = Instantiate<GameObject>(basicProjectilePrefab, transform.position, cameraObject.transform.rotation);
        
    }
}
