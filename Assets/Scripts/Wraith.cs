using UnityEngine;
using UnityEngine.UIElements;

public class Wraith : MonoBehaviour
{
    [HideInInspector] public int health;
    [HideInInspector] public Transform playerTransform;
    protected float timer;
    [Header("Starting Health")]
    [SerializeField] protected int maxHealth;
    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackDamage;


    protected bool attacking;
       
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        playerTransform = GameManager.Instance.player.transform;
        timer = 0;
        attacking = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        timer += Time.deltaTime;
        FacePlayer();
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

    public virtual void FacePlayer()
    {
        Quaternion look = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, look.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void Explode()
    {
        health -= 2;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
