using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void PauseTimeGame()
    {
        Time.timeScale = 0.0f;
    }
}
