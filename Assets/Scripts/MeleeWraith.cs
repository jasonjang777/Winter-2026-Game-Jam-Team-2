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

    public override void Attack()
    {
        base.Attack();
        GameManager.Instance.player.applyDamage(attackDamage);
    }
}
