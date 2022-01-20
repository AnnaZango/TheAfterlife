using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZoneTrigger : MonoBehaviour
{
    [SerializeField] GameObject musicBackground = null;
    [SerializeField] GameObject enemyBoss = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (AudioSource audioSounce in musicBackground.GetComponents<AudioSource>())
            {
                audioSounce.volume = 0.0f;               
            }
            GetComponent<AudioSource>().Play();
            if(enemyBoss != null)
            {
                enemyBoss.GetComponent<Animator>().SetTrigger("PlayerNear");
            }            
        }
    }
}
