using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
     // Public fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public int minHitpoint = 0;
    public float pushRecoverySpeed = 0.2f;

    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    //all fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            if(hitpoint <= 3) 
            {
                // Instantiate(effect, transform.position, Quaternion.identity);
                Debug.Log("hitpoint < 3");
            }
            if(hitpoint <= 0) 
            {
                hitpoint = 0;
                Death();
            }

        }
    }
    protected virtual void ReceiveDamage_boss(Damage dmg)
    {
        
        if(Time.time - lastImmune > immuneTime)
        {
            
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.yellow, transform.position, Vector3.zero, 0.5f);

            if(hitpoint <= 0) 
            {
                hitpoint = 0;
                GameManager.instance.ShowText("Death!!!", 25, Color.yellow, transform.position, Vector3.zero, 0.5f);
                Death();
            }
        }
        // else {
            
        //     if(hitpoint < 3) 
        //     {
        //         hitpoint = hitpoint;
        //         transform.localScale = new Vector3(3,3,3);
        //         Debug.Log("resize");
        //     }

        // }
    }
    protected virtual void Death()
    {
        
    }

}
