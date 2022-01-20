using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] float timeToSelfDestroy = 0.2f;
    [SerializeField] int damageBossProjectile = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject, timeToSelfDestroy);
        }        
    }

    public int GetDamageProjectile()
    {
        return damageBossProjectile;
    }

}
