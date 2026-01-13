using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Wraith : MonoBehaviour
{
    [HideInInspector] public int health;
    [HideInInspector] public Transform playerTransform;
    protected float timer;
    protected float distance;
    protected Vector3 horizontalDirectionToPlayer;
    [Header("Starting Health")]
    [SerializeField] protected int maxHealth;
    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float moveSpeed;

    protected bool attacking;

    // Ground check stuff (so it doesn't just walk off the platform while moving around)
    [SerializeField] protected float groundCheckDistance = 0.2f;
    // [SerializeField] protected float groundCheckAngle = 15f;
    [SerializeField] protected float groundCheckForwardOffset = 1f;
    // protected Quaternion groundCheckRotation;
    protected float enemyHeight;
    protected Rigidbody rb;
    
       
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        playerTransform = GameManager.Instance.player.transform;
        timer = 0;
        attacking = false;
        rb = GetComponent<Rigidbody>();
        enemyHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        // groundCheckRotation = Quaternion.AngleAxis(75f, transform.right);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        timer += Time.deltaTime;
        distance = (playerTransform.position - transform.position).magnitude;
        Vector3 rawDirectionToPlayer = playerTransform.position - transform.position;
        rawDirectionToPlayer.y = 0;
        horizontalDirectionToPlayer = Vector3.Normalize(rawDirectionToPlayer);
        FacePlayer();
        if (timer > attackCooldown & distance < attackRange)
        {
            Attack();
        }
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

    public virtual void Attack()
    {
        timer = 0;
        attacking = true;
    }

    protected GameObject getCurrentPlatform()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, mask))
        {
            return hit.collider.gameObject.transform.parent.gameObject;
        }
        return null;
    }

}
