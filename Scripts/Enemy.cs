using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damagePlayer = 1;
    [SerializeField] int healthPoints = 3;
    [SerializeField] float speedMovement = 2f;
    [SerializeField] float backwardForcePlayerX = 7f;
    [SerializeField] float backwardForcePlayerY = 7f;
    [SerializeField] float timeToSelfDestroy = 1f;

    bool isDead = false;

    [SerializeField] bool isMovementHorizontal = true;

    private void Update()
    {
        if (isMovementHorizontal)
        {
            transform.Translate(speedMovement * Time.deltaTime, 0.0f, 0.0f);
        }
        else
        {
            transform.Translate(0.0f, speedMovement * Time.deltaTime, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BoneProjectile")
        {
            healthPoints--;
            if (healthPoints <= 0)
            {
                Die();
            }
            else if (!isDead)
            {
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("Hurt");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlatformLimit")
        {
            speedMovement = speedMovement * -1;
            if (isMovementHorizontal)
            {                
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }            
        }
    }

    private void Die()
    {
        speedMovement = 0;
        isDead = true;
        Destroy(GetComponentInChildren<CapsuleCollider2D>());
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetTrigger("Die");
        Destroy(this.gameObject, timeToSelfDestroy);
    }

    public int GetEnemyDamage()
    {
        return damagePlayer;
    }

    public float GetBackwardForceX()
    {
        return backwardForcePlayerX;
    }
    public float GetBackwardForceY()
    {
        return backwardForcePlayerY;
    }


}
