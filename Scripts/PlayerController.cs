using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float fire;
    [SerializeField] float speedHorizontal = 1.0f;
    [SerializeField] float jumpingForce = 1.0f;

    [SerializeField] bool isJumping = false;
    [SerializeField] bool isDead = false;

    //Projectile settings
    [SerializeField] Transform projectileCreatorL = null;
    [SerializeField] Transform projectileCreatorR = null;
    [SerializeField] GameObject projectilePrefab = null;
    [SerializeField] float coolDownShoot = 1f;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float timeDestroyProjectile = 5f;
    private bool isShooting = false;
    private GameObject projectileInstance;


    //Sounds
    [SerializeField] GameObject soundJump = null;
    [SerializeField] GameObject soundShot = null;
    [SerializeField] GameObject soundBrainPickup = null;
    [SerializeField] GameObject soundCoinPickup = null;
    [SerializeField] GameObject soundBonePickup = null;
    [SerializeField] GameObject soundPlayerDie = null;
    [SerializeField] GameObject soundPlayerHurt = null;
    [SerializeField] GameObject soundWin = null;
    [SerializeField] GameObject soundLose = null;

    [SerializeField] GameObject accumulator = null;
    
    SpriteRenderer spritePlayer;
    Animator animatorPlayer;
    UIManager accumulatorUIManager;

    private void Awake()
    {
        spritePlayer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        animatorPlayer = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        accumulatorUIManager = accumulator.GetComponent<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        isShooting = false;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        DetectInput();

        MoveHorizontal();

        if (!isJumping)
        {
            if (vertical > 0)
            {
                Jump();
            }
        }

        if (fire > 0)
        {
            Fire();
        }
    }

    private void DetectInput()
    {
        horizontal = Input.GetAxis("Horizontal") * speedHorizontal * Time.deltaTime;
        vertical = Input.GetAxis("Jump");
        fire = Input.GetAxis("Fire1");
    }

    private void MoveHorizontal()
    {
        this.gameObject.transform.Translate(horizontal, 0.0f, 0.0f);
        FlipCharacter();
        SetAnimations();
    }

    private void Jump()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, jumpingForce);
        isJumping = true;
        soundJump.GetComponent<AudioSource>().Play();
        animatorPlayer.SetInteger("StatePlayer", 2);
    }
    
    private void Fire()
    {
        if (!isShooting)
        {
            if (GeneralConfiguration.GetNumProjectiles() <= 0)
            {
                Debug.Log("No bones left");
            }
            else
            {
                isShooting = true;
                soundShot.GetComponent<AudioSource>().Play();
                animatorPlayer.SetTrigger("Shoot");
                GeneralConfiguration.SetNumProjectiles(GeneralConfiguration.GetNumProjectiles() - 1);
                accumulatorUIManager.SetNumProjectilesText();
            }
        }
    }

       
    private void FlipCharacter()
    {
        if (horizontal < 0.0f)
        {
            spritePlayer.flipX = true;
        }
        else if (horizontal > 0.0f)
        {
            spritePlayer.flipX = false;
        }
    }
    
    private void SetAnimations()
    {
        if (horizontal != 0.0f && !isJumping)
        {
            animatorPlayer.SetInteger("StatePlayer", 1);
        }
        else if (!isJumping)
        {
            animatorPlayer.SetInteger("StatePlayer", 0);
        }
    }
     
    

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDead) { return; }

        if(other.gameObject.tag == "Enemy")
        {
            DealWithEnemyDamage(other);
        } 
        else if(other.gameObject.name == "BoneLR" || other.gameObject.name == "BoneUD")
        {
            transform.SetParent(other.gameObject.transform);            
        }
        
    }

    

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "BoneLR" || other.gameObject.name == "BoneUD")
        {
            this.gameObject.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ////To prevent wall jump, feet box collider 2D as trigger to detect ground
        if (other.gameObject.tag == "Ground")
        {
            isJumping = false;
            animatorPlayer.SetInteger("StatePlayer", 0);
        }

        //Hurting elements & boss projectile
        if (other.gameObject.tag == "HurtingElement")
        {            
            GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetCurrentNumLives() - GeneralConfiguration.GetDamageHurtingElements());
            accumulatorUIManager.SetBrainSliderValueText();
            Die();
        }
        else if (other.gameObject.tag == "BossProjectile")
        {
            //deal with damage projectile boss
            int damageBossProjectile = other.GetComponent<BossProjectile>().GetDamageProjectile();
            GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetCurrentNumLives() - damageBossProjectile);
            accumulatorUIManager.SetBrainSliderValueText();
            CheckIfDead();
        }
            

        //pickups
        if (other.gameObject.tag == "BonePickup")
        {
            BonePickedUp(other);
        }
        else if (other.gameObject.tag == "CoinBag")
        {
            CoinPickedUp(other);
        }
        else if (other.gameObject.tag == "Brain")
        {
            BrainPickedUp(other);
        }
    }

    private void DealWithEnemyDamage(Collision2D other)
    {
        int damageByEnemy = 0;
        float backwardForceEnemyX = 0;
        float backwardForceEnemyY = 0;

        if (other.gameObject.GetComponent<Enemy>())
        {
            damageByEnemy = other.gameObject.GetComponent<Enemy>().GetEnemyDamage();
            backwardForceEnemyX = other.gameObject.GetComponent<Enemy>().GetBackwardForceX();
            backwardForceEnemyY = other.gameObject.GetComponent<Enemy>().GetBackwardForceY();
        } else if (other.gameObject.GetComponent<EnemyBoss>())
        {
            damageByEnemy = other.gameObject.GetComponent<EnemyBoss>().GetEnemyDamage();
            other.gameObject.GetComponent<EnemyBoss>().GetBackwardForceX();
            other.gameObject.GetComponent<EnemyBoss>().GetBackwardForceY();
        }
        
        GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetCurrentNumLives() - damageByEnemy);
        accumulatorUIManager.SetBrainSliderValueText();        

        if (!spritePlayer.flipX)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-backwardForceEnemyX, backwardForceEnemyY);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(backwardForceEnemyX, backwardForceEnemyY);
        }

        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if(GeneralConfiguration.GetCurrentNumLives() > GeneralConfiguration.GetNumMinLives())
        {
            animatorPlayer.SetTrigger("Hurt");
            soundPlayerHurt.GetComponent<AudioSource>().Play();
        }
        else
        {
            if (isDead) { return; }
            Die();
        }
    }

    private void Die()
    {
        if (isDead) { return; }
        isDead = true;
        soundPlayerDie.GetComponent<AudioSource>().Play();
        animatorPlayer.SetTrigger("Die");        
        GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetNumMinLives());
        Invoke("ShowGameOverPanel", 1f);
        Destroy(this.gameObject.GetComponent<Rigidbody2D>(), 3.0f);
    }

    private void ShowGameOverPanel()
    {
        soundLose.GetComponent<AudioSource>().Play();
        accumulatorUIManager.DisplayGameOverPanel();
    }

    private void BrainPickedUp(Collider2D other)
    {
        GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetCurrentNumLives() + GeneralConfiguration.GetNumBrainsPickup());

        if (GeneralConfiguration.GetCurrentNumLives() > GeneralConfiguration.GetNumMaxLives())
        {
            GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetNumMaxLives());
        }

        accumulatorUIManager.SetBrainSliderValueText();
        soundBrainPickup.GetComponent<AudioSource>().Play();
        Destroy(other.gameObject);
    }
    
    private void CoinPickedUp(Collider2D other)
    {
        GeneralConfiguration.SetNumCoins(GeneralConfiguration.GetNumCoins() + GeneralConfiguration.GetNumCoinsPickup());
        accumulatorUIManager.SetNumCoinsText();
        soundCoinPickup.GetComponent<AudioSource>().Play();
        Destroy(other.gameObject);
        
    }
    
    private void BonePickedUp(Collider2D other)
    {
        GeneralConfiguration.SetNumProjectiles(GeneralConfiguration.GetNumProjectiles() + GeneralConfiguration.GetNumProjectilesPickup());

        if (GeneralConfiguration.GetNumProjectiles() > GeneralConfiguration.GetNumMaxProjectiles())
        {
            GeneralConfiguration.SetNumProjectiles(GeneralConfiguration.GetNumMaxProjectiles());
        }

        accumulatorUIManager.SetNumProjectilesText();
        soundBonePickup.GetComponent<AudioSource>().Play();
        Destroy(other.gameObject);
    }
    

    public void LevelIsFinished()
    {
        if (SceneManager.GetActiveScene().name != "Level3")
        {
            soundWin.GetComponent<AudioSource>().Play();
        }        
        isDead = true;
    }


    public void InstantiateProjectile()
    {
        if (!spritePlayer.flipX)
        {
            projectileInstance = Instantiate(projectilePrefab, projectileCreatorR.position, Quaternion.identity);
            projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0.0f);
        }
        else
        {
            projectileInstance = Instantiate(projectilePrefab, projectileCreatorL.position, Quaternion.Euler(0.0f, 0.0f, 180.0f));
            projectileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0.0f);
        }

        Destroy(projectileInstance, timeDestroyProjectile);

        Invoke("FinishShot", coolDownShoot);
    }
    
    
    private void FinishShot()
    {
        isShooting = false;
    }

}
