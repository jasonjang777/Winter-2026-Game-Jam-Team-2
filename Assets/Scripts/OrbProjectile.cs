using UnityEngine;

public class OrbProjectile : WraithProjectile
{

    [SerializeField] float rateOfChange;
    [SerializeField] float lowLimit;
    private Vector3 og;
    private bool forwards;
    private float currentScale;
    private float timer;

    public override void Start()
    {
        base.Start();
        og = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
        forwards = true;
        currentScale = 1;
        timer = 0;
    }
    public override void Update()
    {
        base.Update();
    
            timer = 0;
            transform.localScale = new Vector3 (og.x * currentScale, og.y * currentScale, og.z);
            if (forwards)
            {
                currentScale -= 1f / rateOfChange * Time.deltaTime;
            }
            else
            {
                currentScale += 1f / rateOfChange * Time.deltaTime;
            }

            if (currentScale >= 1)
            {
                forwards = true;
                currentScale -= 1f / rateOfChange * Time.deltaTime;
            }
            if (currentScale <= lowLimit)
            {
                forwards = false;
                currentScale += 1f / rateOfChange * Time.deltaTime;
            }
      
    }


}
