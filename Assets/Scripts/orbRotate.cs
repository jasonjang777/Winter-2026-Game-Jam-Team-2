using UnityEngine;

public class orbRotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        this.transform.rotation = transform.rotation * Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
    }
}
