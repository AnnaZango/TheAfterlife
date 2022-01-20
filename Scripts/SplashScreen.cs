using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float timeToLoadNextScene = 2.0f;

    void Start()
    {
        StartCoroutine(LoadMainMenuScene());        
    }

    IEnumerator LoadMainMenuScene()
    {
        yield return new WaitForSeconds(timeToLoadNextScene);
        SceneManager.LoadScene("Main_Menu");
    }
    
}
