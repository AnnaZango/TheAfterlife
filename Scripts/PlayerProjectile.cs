using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float timeToSelfDestroy = 0.2f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                float backwardForceEnemyX = other.gameObject.GetComponent<Enemy>().GetBackwardForceX();
                float backwardForceEnemyY = other.gameObject.GetComponent<Enemy>().GetBackwardForceY();

                if (this.gameObject.GetComponent<Rigidbody2D>().velocity.x >= 0)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-backwardForceEnemyX, backwardForceEnemyY);
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(backwardForceEnemyX, backwardForceEnemyY);
                }
            }
            
            Destroy(this.gameObject, timeToSelfDestroy);
        }
    }
       

}
