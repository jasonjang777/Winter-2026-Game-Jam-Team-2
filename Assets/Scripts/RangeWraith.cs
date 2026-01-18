using UnityEngine;

public class RangeWraith : Wraith
{
    [SerializeField] GameObject wraithProjectile;
    [SerializeField] GameObject projectileOrigin;

    public override void Attack()
    {
        base.Attack();


        GameObject projectile = Instantiate<GameObject>(wraithProjectile, projectileOrigin.transform.position, Quaternion.LookRotation(playerTransform.position - transform.position));
        projectile.GetComponent<WraithProjectile>().SetAttackDamage(attackDamage);
    }

        public void FixedUpdate()
    {
        // If player is on same platform & wraith is facing player, move AWAY from them
        if ((GameManager.Instance.player.DetectCurrentPlatform() == getCurrentPlatform() || distance <= detectionRange) && 
        Mathf.Abs(Quaternion.Dot(targetRotation, transform.rotation)) > 0.99f)
        {
            // Debug.Log("Player Detected");
            // Debug.Log(horizontalDirectionToPlayer);
            // Debug.Log(groundCheckRotation * horizontalDirectionToPlayer);
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(transform.position + (transform.forward * groundCheckForwardOffset * -1), Vector3.down,
            out RaycastHit hit, (enemyHeight / 2) + groundCheckDistance, mask))
            {
                // Debug.Log("Moving away from player");
                Debug.DrawRay(transform.position + (transform.forward * groundCheckForwardOffset), Vector3.down * hit.distance, 
                Color.green, 0.1f);
                // Debug.Log("Enemy moving");
                rb.MovePosition(transform.position + horizontalDirectionToPlayer * Time.fixedDeltaTime * moveSpeed * -1);
                lastMoved = Time.time;
            }
            // else
            // {
            //     // Debug.Log("Not moving towards player");
            //     Debug.DrawRay(transform.position + (transform.forward * groundCheckForwardOffset), Vector3.down * ((enemyHeight / 2) + groundCheckDistance), 
            //     Color.red, 0.1f);
            //     // Debug.DrawRay(transform.position, groundCheckRotation * Vector3.down * ((enemyHeight / 2) + groundCheckDistance), 
            //     // Color.red, 0.1f);
            // }
        }
        else if (Mathf.Abs(Quaternion.Dot(targetRotation, transform.rotation)) > 0.99f) // Wander behavior
        {
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(transform.position + (transform.forward * groundCheckForwardOffset), Vector3.down,
            out RaycastHit hit, (enemyHeight / 2) + groundCheckDistance, mask))
            {
                Debug.DrawRay(transform.position + (transform.forward * groundCheckForwardOffset), Vector3.down * hit.distance, 
                Color.green, 0.1f);
                // Debug.Log("Enemy moving");
                rb.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
                nextRandomRotation = Time.time + randomRotationCooldown;
                lastMoved = Time.time;
            }
        } 
    }
}
