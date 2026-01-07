using UnityEngine;

public class BasicProjectile : MonoBehaviour
{

    [SerializeField] private float projectileSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.position = transform.position + (transform.forward * Time.deltaTime * projectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {   
        Debug.Log("platform hit");
        Destroy(gameObject);
    }
}
