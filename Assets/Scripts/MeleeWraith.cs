using UnityEngine;

public class MeleeWraith : Wraith
{
    [Header("Temp Visual Indicator")]
    [SerializeField] GameObject attackIndicator;
    private float distance;
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
        distance = (playerTransform.position - transform.position).magnitude;
        //Temp Code until we have animations, hence hardcoded numbers
        if (timer > attackCooldown & distance < attackRange)
        {
            timer = 0;
            attacking = true;
            GameManager.Instance.player.applyDamage(attackDamage);
        }
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


}
