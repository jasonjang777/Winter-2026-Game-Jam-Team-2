using UnityEngine;

public class WraithProjectile : BasicProjectile
{
    private float attackDamage=0;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.gameObject.tag == "Player") 
        {
            GameManager.Instance.player.applyDamage(attackDamage);
        }
    }
    public void SetAttackDamage(float ad)
    {
        attackDamage = ad;
    }
}
