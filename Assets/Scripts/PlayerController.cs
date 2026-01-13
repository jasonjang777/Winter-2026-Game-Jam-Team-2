using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/*
    This script provides jumping and movement in Unity 3D - Gatsby (YT)
*/

public class PlayerController : MonoBehaviour
{
    // Camera Rotation
    [SerializeField] private float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    [SerializeField] private Transform cameraTransform;
    
    // Ground Movement
    private Rigidbody rb;
    [SerializeField] private float MoveSpeed = 5f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    [SerializeField] private float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;

    // Resource Management
    [SerializeField] private float healthPoints = 100;
    [SerializeField] private float regenDelay = 3f;
    [SerializeField] private float regenTickSpeed = 0.25f;
    private HealthBarUI healthBar;
    private float maxHP;
    private float startHealthRegeneration = 0f;
    private float nextTick = 0f;

    // Blue Powerup Launch
    [SerializeField] private float launchReducedDrag = 0.9f;
    [SerializeField] private float launchStandardDrag = 0.8f;
    [SerializeField] private float launchIncreasedDrag = 0.7f;
    [SerializeField] private float launchPhysicsMinDuration = 0.5f;
    [SerializeField] private float launchDisableGroundCheckDuration = 0.15f;
    private bool launched = false;
    private bool launchPhysics = false;
    private bool launchDisableGroundCheck = false;

    // Platform Detection (for enemy aggro)
    [SerializeField] private float platformRaycastDist = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        // Hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set Max HP
        healthBar = GameObject.Find("UI/HealthBar").GetComponent<HealthBarUI>();
        maxHP = healthPoints;
        healthBar.setMaxHealth(maxHP);
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        RotateCamera();

        if (Input.GetButtonDown("Jump") && isGrounded && !launched)
        {
            Jump();
        }

        // Checking when we're on the ground and keeping track of our ground check delay
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

        // Regenerate HP up to max if we haven't taken damage recently
        if (healthPoints < maxHP && Time.time >= startHealthRegeneration && Time.time >= nextTick)
        {
            applyHeal(1);
            nextTick = Time.time + regenTickSpeed;
            // Debug.Log("HP: " + healthPoints);
        }

        // Game Over 
        if (healthPoints <= 0)
        {
            // Debug.Log("Game Over");
        }
    }
    void FixedUpdate()
    {
        MovePlayer();   
        ApplyJumpPhysics();
    }
    void MovePlayer()
    {

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.linearVelocity;

        // If moving faster than normal (launching), use different physics
        if (Math.Abs(velocity.x) + Math.Abs(velocity.z) > MoveSpeed * Math.Sqrt(2)) 
        {
            launchPhysics = true;
            // Debug.Log("Using launch movement");
            // Debug.Log("X velocity: " + velocity.x);
            // Debug.Log("z velocity: " + velocity.z);
        }
        else
        {
            launchPhysics = launched;
            // Debug.Log("Reverting to normal movement");
            // Debug.Log("X velocity: " + velocity.x);
            // Debug.Log("z velocity: " + velocity.z);
        }

        // velocity.x = targetVelocity.x;
        if (!launchPhysics)
        {
            velocity.x = targetVelocity.x;
        }
        else if (targetVelocity.x == 0)
        {   
            velocity.x = velocity.x * launchStandardDrag;
        }
        else if (velocity.x > 0 && targetVelocity.x > 0)
        {   
            velocity.x = Math.Max(velocity.x * launchReducedDrag, targetVelocity.x);
        }
        else if (velocity.x < 0 && targetVelocity.x < 0)
        {   
            velocity.x = Math.Min(velocity.x * launchReducedDrag, targetVelocity.x);
        }
        else
        {
            //velocity.x = Math.Clamp(velocity.x + targetVelocity.x, -1 * Math.Abs(targetVelocity.x), Math.Abs(targetVelocity.x));
            velocity.x = velocity.x * launchIncreasedDrag;
        }

        // velocity.z = targetVelocity.z;
        
        // if (isGrounded)
        // {
        //    velocity.z = targetVelocity.z; 
        
        if (!launchPhysics)
        {
            velocity.z = targetVelocity.z;
        }
        else if (targetVelocity.z == 0)
        {   
            velocity.z = velocity.z * launchStandardDrag;
        }
        else if (velocity.z > 0 && targetVelocity.z > 0)
        {   
            velocity.z = Math.Max(velocity.z * launchReducedDrag, targetVelocity.z);
        }
        else if (velocity.x < 0 && targetVelocity.x < 0)
        {   
            velocity.z = Math.Min(velocity.z * launchReducedDrag, targetVelocity.z);
        }
        else
        {
            //velocity.x = Math.Clamp(velocity.x + targetVelocity.x, -1 * Math.Abs(targetVelocity.x), Math.Abs(targetVelocity.x));
            velocity.z = velocity.z * launchIncreasedDrag;
        }
        rb.linearVelocity = velocity;

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0 && !launchPhysics)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void Jump()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Initial burst for the jump
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0) 
        {
            // Falling: Apply fall multiplier to make descent faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.linearVelocity.y > 0)
        {
            // Rising: Change multiplier to make player reach peak of jump faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier  * Time.fixedDeltaTime;
        }
    }

    // Blue Powerup Stuff

    // public void Launch(float lockControlsDelay)
    // {
    //     StartCoroutine(ApplyLaunch(lockControlsDelay));
    // }

    // IEnumerator ApplyLaunch(float lockControlsDelay)
    // {
    //     launched = true;
    //     yield return new WaitForSeconds(lockControlsDelay);
    //     launched = false;
    //     yield break;
    // }

    
    // private void LaunchLockMovement()
    // {
    //     launched = true;
    //     isGrounded = false;
    // }

    // private void LaunchResumeMovement()
    // {
    //     launched = false;
    // }

    private Coroutine runningPullCoroutine = null;
    public void StartPullCoroutine(Vector3 pos, float maxForceMagnitude, float minForceMagnitude,
    float startForceScalingDistance, float endForceScalingDistance, float horizontalForceMultiplier, float minControlLock, float maxControlLock)
    {
        if (runningPullCoroutine != null)
        {
            StopCoroutine(runningPullCoroutine);
        }
        runningPullCoroutine = StartCoroutine(PullPlayerTowardsPosition(pos, maxForceMagnitude, minForceMagnitude,
        startForceScalingDistance,  endForceScalingDistance,  horizontalForceMultiplier, minControlLock,  maxControlLock));

    }

    public void StopPullCoroutine()
    {
        if (runningPullCoroutine != null)
        {
            StopCoroutine(runningPullCoroutine);
            runningPullCoroutine = null;
        }
    }
    // if anyone comes across this: i am deeply sorry for this abomination
    IEnumerator PullPlayerTowardsPosition(Vector3 pos, float maxForceMagnitude, float minForceMagnitude,
    float startForceScalingDistance, float endForceScalingDistance, float horizontalForceMultiplier, float minControlLock, float maxControlLock)
    {
        launched = true;
        launchDisableGroundCheck = true;

        Vector3 direction = pos- transform.position;
        Vector3 normalizedDirection = Vector3.Normalize(direction);
        Vector3 actualLaunchDirection = normalizedDirection;
        // actualLaunchDirection.y = 1;

        // Pull force scales linearly with player's distance from powerup
        float maxForceToAdd = maxForceMagnitude - minForceMagnitude;
        float maxControlLockToAdd = maxControlLock - minControlLock;
        float distanceToPlayer = Vector3.Distance(pos, transform.position);
        float maxDistaceAdjusted = endForceScalingDistance - startForceScalingDistance;
        float distanceToPlayerAdjusted = distanceToPlayer - startForceScalingDistance;
        float maxForcePercentage = Mathf.Clamp(distanceToPlayerAdjusted/maxDistaceAdjusted, 0, 1);
        float actualForceToAdd = minForceMagnitude + (maxForcePercentage * maxForceToAdd);
        float actualControlLock = minControlLock + (maxForcePercentage * maxControlLock);

        Debug.Log("Magnitude of force added: " + actualForceToAdd);
        Debug.Log("Duration of movement lock: " + actualControlLock);
        Vector3 launchVector = actualForceToAdd * actualLaunchDirection;
        launchVector.x *= horizontalForceMultiplier;
        if (launchVector.y < 0)
        {
            launchVector.y = 0;
        }
        launchVector.z *= horizontalForceMultiplier;
        rb.linearVelocity = launchVector;
        yield return new WaitForSeconds(launchDisableGroundCheckDuration);
        launchDisableGroundCheck = false;
        yield return new WaitForSeconds(launchPhysicsMinDuration - launchDisableGroundCheckDuration);
        // yield return new WaitUntil(() => playerRB.linearVelocity.magnitude <= minLaunchSpeedThreshold);
        launched = false;
        yield break;
    }
    
    // Health Stuff

    public float getHealth()
    {
        return healthPoints;
    }

    public void applyDamage(float dmg)
    {
        healthPoints = Math.Max(healthPoints - dmg, 0f);
        startHealthRegeneration = Time.time + regenDelay;
        healthBar.setHealth(healthPoints);
        // Debug.Log("HP: " + healthPoints);
    }
    public void applyHeal(float heal)
    {
        healthPoints = Math.Min(healthPoints + heal, maxHP);
        healthBar.setHealth(healthPoints);
        // Debug.Log("HP: " + healthPoints);
    }

    // Enemy player detection
    public GameObject DetectCurrentPlatform()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out hit, platformRaycastDist, mask))
        {
            return hit.collider.gameObject.transform.parent.gameObject;
        }
        return null;
    }
}
