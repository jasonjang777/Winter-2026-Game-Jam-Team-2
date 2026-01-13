using UnityEditor.Callbacks;
using UnityEngine;

public class MeleeWraith : Wraith
{
    [Header("Temp Visual Indicator")]
    [SerializeField] GameObject attackIndicator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     health = 3;
    //     // playerTransform = GameManager.Instance.player.transform;
    // }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        //Temp Code until we have animations, hence hardcoded numbers
        if (timer > 1 & attacking)
        {
            attacking = false;
        }
        if (attacking)
        {
            attackIndicator.SetActive(true);
        }
        else
        {
            attackIndicator.SetActive(false);
        }
        // End of "Temp" Code
    }

    public void FixedUpdate()
    {
        // If player is on same platform, move towards them
        if (GameManager.Instance.player.DetectCurrentPlatform() == getCurrentPlatform())
        {
            // Debug.Log("Player Detected");
            // Debug.Log(horizontalDirectionToPlayer);
            // Debug.Log(groundCheckRotation * horizontalDirectionToPlayer);
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(transform.position + (transform.forward * groundCheckForwardOffset), Vector3.down,
            out RaycastHit hit, (enemyHeight / 2) + groundCheckDistance, mask))
            {
                // Debug.Log("Moving towards player");
                Debug.DrawRay(transform.position + (transform.forward * groundCheckForwardOffset), Vector3.down * hit.distance, 
                Color.green, 0.1f);
                rb.MovePosition(transform.position + horizontalDirectionToPlayer * Time.fixedDeltaTime * moveSpeed);
            }
            else
            {
                // Debug.Log("Not moving towards player");
                Debug.DrawRay(transform.position + (transform.forward * groundCheckForwardOffset), Vector3.down * ((enemyHeight / 2) + groundCheckDistance), 
                Color.red, 0.1f);
                // Debug.DrawRay(transform.position, groundCheckRotation * Vector3.down * ((enemyHeight / 2) + groundCheckDistance), 
                // Color.red, 0.1f);
            }
        }
    }

    public override void Attack()
    {
        base.Attack();
        GameManager.Instance.player.applyDamage(attackDamage);
    }
}
