using UnityEngine;

public class RangeWraith : MonoBehaviour
{
    public int health;
    public Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 2;
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
