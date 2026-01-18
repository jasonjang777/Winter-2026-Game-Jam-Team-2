using UnityEngine;
using System.Collections;
using System.Data;

public class BluePowerup : MonoBehaviour
{
    // [SerializeField] private GameObject playerPrefab;
    
    [SerializeField] private float minForceMagnitude = 100f;
    [SerializeField] private float maxForceMagnitude = 1000f;
    [SerializeField] private float startForceScalingDistance = 1f;
    [SerializeField] private float endForceScalingDistance = 50f;
    [SerializeField] private float horizontalForceMultiplier = 1.5f;

    [SerializeField] private float minControlLock = 0.1f;
    [SerializeField] private float maxControlLock = 0.25f;

    private GameObject playerPrefab;
    // private Rigidbody playerRB;
    private PlayerController playerScript;

    void Start()
    {   
        playerPrefab = GameObject.Find("Player");
        // playerRB = playerPrefab.GetComponent<Rigidbody>();
        playerScript = playerPrefab.GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {   
            Debug.Log("Blue Powerup Hit");
            playerScript.StartPullCoroutine(transform.position, maxForceMagnitude, minForceMagnitude,
        startForceScalingDistance,  endForceScalingDistance,  horizontalForceMultiplier, minControlLock,  maxControlLock);
            // PullPlayer();
            Destroy(gameObject);
        }
    }

    // IEnumerator PullPlayer()
    // {
    //     playerScript.LaunchLockMovement();
    //     Vector3 direction = transform.position - playerPrefab.transform.position;
    //     Vector3 normalizedDirection = Vector3.Normalize(direction);
    //     Vector3 actualLaunchDirection = normalizedDirection;
    //     actualLaunchDirection.y = 1;

    //     // Pull force scales linearly with player's distance from powerup
    //     float maxForceToAdd = maxForceMagnitude - minForceMagnitude;
    //     float maxControlLockToAdd = maxControlLock - minControlLock;
    //     float distanceToPlayer = Vector3.Distance(transform.position, playerPrefab.transform.position);
    //     float maxDistaceAdjusted = endForceScalingDistance - startForceScalingDistance;
    //     float distanceToPlayerAdjusted = distanceToPlayer - startForceScalingDistance;
    //     float maxForcePercentage = Mathf.Clamp(distanceToPlayerAdjusted/maxDistaceAdjusted, 0, 1);
    //     float actualForceToAdd = minForceMagnitude + (maxForcePercentage * maxForceToAdd);
    //     float actualControlLock = minControlLock + (maxForcePercentage * maxControlLock);

    //     Debug.Log("Magnitude of force added: " + actualForceToAdd);
    //     Debug.Log("Duration of movement lock: " + actualControlLock);
    //     playerRB.linearVelocity = actualForceToAdd * actualLaunchDirection;
    //     yield return new WaitForSeconds(actualControlLock);
    //     // yield return new WaitUntil(() => playerRB.linearVelocity.magnitude <= minLaunchSpeedThreshold);

    //     playerScript.LaunchResumeMovement();
    //     Destroy(gameObject);
    //     yield break;
    // }

    // void PullPlayer()
    // {
    //     Vector3 direction = transform.position - playerPrefab.transform.position;
    //     Vector3 normalizedDirection = Vector3.Normalize(direction);

    //     // Pull force scales linearly with player's distance from powerup
    //     float maxForceToAdd = maxForceMagnitude - minForceMagnitude;
    //     float maxControlLockToAdd = maxControlLock - minControlLock;
    //     float distanceToPlayer = Vector3.Distance(transform.position, playerPrefab.transform.position);
    //     float maxDistaceAdjusted = endForceScalingDistance - startForceScalingDistance;
    //     float distanceToPlayerAdjusted = distanceToPlayer - startForceScalingDistance;
    //     float maxForcePercentage = Mathf.Clamp(distanceToPlayerAdjusted/maxDistaceAdjusted, 0, 1);
    //     float actualForceToAdd = minForceMagnitude + (maxForcePercentage * maxForceToAdd);
    //     float actualControlLock = minControlLock + (maxForcePercentage * maxControlLock);

    //     Debug.Log("Magnitude of force added: " + actualForceToAdd);
    //     playerRB.linearVelocity = actualForceToAdd * normalizedDirection;
    //     Destroy(gameObject);
    // }

}
