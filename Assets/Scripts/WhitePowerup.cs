using System.Runtime.CompilerServices;
using UnityEngine;

public class WhitePowerup : MonoBehaviour
{

    [SerializeField] private float orbitRadius = 10f;
    [SerializeField] private Vector3 startingDirectionFromCenter = Vector3.forward;
    [SerializeField] private Vector3 orbitAxis = Vector3.up; 
    [SerializeField] private float orbitSpeed = 10f; 

    [SerializeField] private float healAmount = 20f; 
    private PlayerController controllerScriptRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controllerScriptRef = GameObject.Find("Player").GetComponent<PlayerController>();
        transform.position += orbitRadius * startingDirectionFromCenter;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject orbitPoint = transform.parent.gameObject;
        transform.RotateAround(orbitPoint.transform.position, orbitAxis, orbitSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {   
            //Debug.Log("White Powerup Hit");
            controllerScriptRef.applyHeal(healAmount);
            Destroy(gameObject);
        }
    }
}
