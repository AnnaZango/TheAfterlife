using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
           
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if(SceneManager.GetActiveScene().name != "Level3")
            {
                other.GetComponent<PlayerController>().LevelIsFinished();
                FindObjectOfType<UIManager>().DisplayVictoryPanel();
            }
            else
            {
                if (FindObjectOfType<EnemyBoss>())
                {
                    Debug.Log("Enemy still alive");
                }
                else
                {
                    other.GetComponent<PlayerController>().LevelIsFinished();
                    FindObjectOfType<UIManager>().LoadNextLevel();
                }              
            }
            
        }
    }
}
