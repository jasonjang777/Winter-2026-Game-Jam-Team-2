using UnityEngine;
using UnityEngine.UIElements;

public class Wraith : MonoBehaviour
{
    public int health;
    public Transform playerTransform;
    private float timer;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;
    private float distance;
    private bool attacking;
    [SerializeField] GameObject attackIndicator;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 3;
        playerTransform = GameManager.Instance.player.transform;
        timer = 0;
        distance = 0;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        FacePlayer();
        distance = (playerTransform.position - transform.position).magnitude;
        if (timer > attackCooldown & distance < attackRange)
        {
            timer = 0;
            attacking = true;
            GameManager.Instance.player.applyDamage(20);
        }
        //Temp Code until we have animations, hence hardcoded numbers
        if(timer>1 & attacking)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            --health;
            if (health <= 0)
            {               
                Destroy(gameObject);    
            }

        }
    }

    private void FacePlayer()
    {
        Quaternion look = Quaternion.LookRotation(playerTransform.position - transform.position);

        // For Melee and Ranged Wraiths only (Only look at player on one axis):
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, look.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void explode()
    {
        health -= 2;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
