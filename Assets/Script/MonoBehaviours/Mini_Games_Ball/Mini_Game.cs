using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini_Game : MonoBehaviour
{
    public GameObject miniGamePanel;
    public GameObject visual_joystick;

    //미니게임패널 종료하는기능
    public void ExitMiniGame()
    {
        if(miniGamePanel.activeSelf == false)
        {
            miniGamePanel.SetActive(true);
            visual_joystick.SetActive(false);
        }
        else
        {
            miniGamePanel.SetActive(false);
            visual_joystick.SetActive(true);
        }
    }
}