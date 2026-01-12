using UnityEngine;

public class OrbWraith : Wraith
{
    // public int health;
    // public Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     health = 1;
    //     // playerTransform = GameManager.Instance.player.transform;
    // }

    // Update is called once per frame



    public override void FacePlayer()
    {
        Quaternion look = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = look;
    }


}
