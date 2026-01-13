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
}
