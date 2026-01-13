using UnityEngine;

public class OrbWraith : Wraith
{

    public override void FacePlayer()
    {
        Quaternion look = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = look;
    }


}
