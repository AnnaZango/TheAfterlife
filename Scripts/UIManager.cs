using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel = null;
    [SerializeField] GameObject gameOverPanel = null;
    [SerializeField] GameObject victoryPanel = null;

    [SerializeField] GameObject brainBar = null;
    [SerializeField] GameObject numProjectilesText = null;
    [SerializeField] GameObject numCoinsText = null;

    int currentSceneIndex;

    int statePausePanel = 0;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        DisablePanels();

        SetNumProjectilesText();
        SetNumCoinsText();
        SetBrainSliderValueText();

        statePausePanel = 0;
    }
    private void Start()
    {
        if (!brainBar) { return; }
        Slider brainBarSlider = brainBar.GetComponent<Slider>();
        brainBarSlider.value = GeneralConfiguration.GetCurrentNumLives();
        brainBarSlider.interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (statePausePanel == 0)
            {
                statePausePanel = 1;
                //Time.timeScale = 0.0f;
            } else 
            {
                statePausePanel = 0;
                
            } 
            DisplayPausePanel(statePausePanel);
        }
    }


    private void DisablePanels()
    {
        FindPanel(pausePanel);
        FindPanel(gameOverPanel);
        FindPanel(victoryPanel);
    }

    private void FindPanel(GameObject panelToCheck)
    {
        if(!panelToCheck)
        {
            Debug.LogWarning("Panel unassigned or nonexistent");
            return;
        }
        else
        {
            panelToCheck.SetActive(false);
        }        
    }

    
    public void SetNumProjectilesText()
    {
        if (!numProjectilesText) { return; }
        numProjectilesText.GetComponent<TextMeshProUGUI>().text = GeneralConfiguration.GetNumProjectiles().ToString();
    }

    public void SetNumCoinsText()
    {
        if(!numCoinsText) { return; }
        numCoinsText.GetComponent<TextMeshProUGUI>().text = GeneralConfiguration.GetNumCoins().ToString();
    }

    public void SetBrainSliderValueText()
    {
        if (!brainBar) { return; }
        Slider brainBarSlider = brainBar.GetComponent<Slider>();
        brainBarSlider.value = GeneralConfiguration.GetCurrentNumLives();
    }       

    public int GetBrainBarValue()
    {
        return Mathf.RoundToInt(brainBar.GetComponent<Slider>().value);
    }
          
    public void DisplayPausePanel(int status)
    {
        if(pausePanel == null) { return; }
        if (!pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
        }

        switch (status)
        {
            case 1:
                pausePanel.GetComponent<Animator>().SetInteger("statePausePanel", 1);
                break;            
            default:
                pausePanel.GetComponent<Animator>().SetInteger("statePausePanel", 0);
                Time.timeScale = 1;
                break;
        }     
        
    }

    public void DisplayVictoryPanel()
    {
        if (victoryPanel == null) { return; }
        victoryPanel.SetActive(true);
    }

    public void DisplayGameOverPanel()
    {
        if(gameOverPanel == null) { return; }
        gameOverPanel.SetActive(true);
    }

    public void LoadFirstLevel()
    {
        GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetNumMaxLives());
        GeneralConfiguration.SetNumProjectiles(GeneralConfiguration.GetNumMaxProjectiles());
        SceneManager.LoadScene("Level1");
    }

    public void LoadNextLevel()
    {
        //GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetNumMaxLives());
        //If you want to keep lives from previous level:
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSameScene()
    {
        GeneralConfiguration.SetCurrentNumLives(GeneralConfiguration.GetNumMaxLives());
        GeneralConfiguration.SetNumCoins(0);
        GeneralConfiguration.SetNumProjectiles(GeneralConfiguration.GetNumMaxProjectiles());
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You exited the game");
    }

}
