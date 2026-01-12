using UnityEngine;

public class OrbWraith : MonoBehaviour
{
    public int health;
    public Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 1;
        playerTransform = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void FacePlayer()
    {
        Quaternion look = Quaternion.LookRotation(playerTransform.position - transform.position);

        // For orb only (Only look at player on one axis):
        transform.rotation = look;
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
