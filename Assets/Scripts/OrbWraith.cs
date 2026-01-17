using UnityEngine;

public class OrbWraith : Wraith
{
    [SerializeField] float engageRange;
    public bool aggroed;
    [Header("Projectile Settings")]
    [SerializeField] GameObject orbProjectile;
    [SerializeField] GameObject projectileOrigin;
    private LayerMask mask;

    public override void Start()
    {
        base.Start();
        mask = LayerMask.GetMask("Ground");
        aggroed = false;
    }

    public override void Update()
    {
        base.Update();

        //Temp Code until we have animations, hence hardcoded numbers
        if (timer > 1 & attacking)
        {
            attacking = false;
            GameObject projectile = Instantiate<GameObject>(orbProjectile, projectileOrigin.transform.position, Quaternion.LookRotation(playerTransform.position - transform.position));
            projectile.GetComponent<WraithProjectile>().SetAttackDamage(attackDamage);
        }
        // End of "Temp" Code
        if (distance < attackRange)
        {
            aggroed=true;
        }
        if (distance > detectionRange)
        {  
            aggroed=false;
        }
    }
    public override void FacePlayer()
    {
        Quaternion look = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = look;
    }

    public void FixedUpdate()
    {
        
        if (aggroed)
        {
            if (distance > attackRange * .75)
            {
                rb.MovePosition(transform.position + (playerTransform.position - transform.position).normalized * Time.fixedDeltaTime * moveSpeed);
                lastMoved = Time.time;
            }
            else if (distance < attackRange * .5)
            {
                rb.MovePosition(transform.position + (playerTransform.position - transform.position).normalized * Time.fixedDeltaTime * moveSpeed  * -1);
                lastMoved = Time.time;
            }
            else 
            {
                rb.MovePosition(transform.position + Vector3.Cross( horizontalDirectionToPlayer, Vector3.up).normalized * Time.fixedDeltaTime * moveSpeed);
                lastMoved = Time.time;
            }
        }
    }
    public override void Attack()
    {
        base.Attack();
        GameObject projectile = Instantiate<GameObject>(orbProjectile, projectileOrigin.transform.position, Quaternion.LookRotation(playerTransform.position - transform.position));
        projectile.GetComponent<WraithProjectile>().SetAttackDamage(attackDamage);
    }

    private void Hover()
    {
        if (!attacking)
        {

        }
    }
}
