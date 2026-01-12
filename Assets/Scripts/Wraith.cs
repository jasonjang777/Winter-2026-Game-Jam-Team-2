using UnityEngine;

public class Wraith : MonoBehaviour
{
    [SerializeField] protected int startingHealth;
    [SerializeField] protected Transform playerTransform;
    [HideInInspector] public int health;

    protected void Start()
    {
        health = startingHealth;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            --health;
            Debug.Log(gameObject.name + " now has " + health + " HP!");
            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }
    }
    public void Explode()
    {
        health -= 2;
        Debug.Log(gameObject.name + " now has " + health + " HP!");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
