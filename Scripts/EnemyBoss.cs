using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] int damagePlayer = 1;
    [SerializeField] int healthPoints = 7;
    [SerializeField] float backwardForcePlayerX = 7f;
    [SerializeField] float backwardForcePlayerY = 7f;
    [SerializeField] float timeToSelfDestroy = 1f;

    //Projectile
    [SerializeField] float projectileSpeedMin = 3f;
    [SerializeField] float projectileSpeedMax = 6f;
    [SerializeField] float timeSelfDestroyProjectile = 4f;
    [SerializeField] Transform projectileSpawnLocation = null;
    [SerializeField] GameObject projectileBoss = null;
    private GameObject projectileBossInstance = null;

    private GameObject player;

    bool isDead = false;

    bool isFacingRight = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {            
            if (isFacingRight)
            {
                isFacingRight = false;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }            
        }
        else
        {
           if (!isFacingRight)
            {
                isFacingRight = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BoneProjectile")
        {
            healthPoints--;
            if (healthPoints <= 0)
            {
                if (!isDead)
                {
                    Die();
                }                
            }
            else if (!isDead)
            {
                GetComponent<AudioSource>().Play();
                GetComponent<Animator>().SetTrigger("Hurt");
            }
        }
    }

    private void Die()
    {
        Destroy(GetComponent<Rigidbody2D>());
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

    public void InstantiateProjectile()
    {
        float speed = Random.Range(projectileSpeedMin, projectileSpeedMax);        

        if (player.transform.position.x < transform.position.x)
        {           
            projectileBossInstance = Instantiate(projectileBoss, projectileSpawnLocation.position, Quaternion.identity);
            projectileBossInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        }
        else
        {
            projectileBossInstance = Instantiate(projectileBoss, projectileSpawnLocation.position, Quaternion.Euler(0, 180, 0));            
            projectileBossInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        }
        
        Destroy(projectileBossInstance, timeSelfDestroyProjectile);
    }

}
