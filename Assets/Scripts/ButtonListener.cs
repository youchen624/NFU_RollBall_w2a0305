using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class ButtonListener : MonoBehaviour
{
    public UIButtonType buttonType = UIButtonType.None;
    //GameManager gm;
    // Start is called before the first frame update
    void Start()
    {

        switch (gameObject.name)
        {
            /*
            case "StartButton":
                GameManager.shouldStart = true; break;
            // gm.GetComponent<GameManager>().shouldStart = true; break;
            case "QuitButton":
                GameManager.APP_QUIT_NOW = true; break;
            // gm.GetComponent<GameManager>().APP_QUIT_NOW = true; break;
            case "ID_NameButton":
                GameManager.startMenu_ID_name_btn = true; break;
            */
            case "PauseButton":
                buttonType = UIButtonType.PauseButton; break;
            case "ResumeButton":
                buttonType = UIButtonType.ResumeButton;
                if (!GameManager.isPaused) gameObject.SetActive(false);
                break;

            default: break;
        }
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (buttonType != UIButtonType.None) {
            GameManager.gm.RegisterButton(buttonType, GetComponent<Button>());
        }
    }

    public void OnButtonClicked(string btnName)
    {
        Debug.Log($"{btnName} clicked");
        switch (btnName)
        {
            case "StartButton":
                GameManager.shouldStart = true; break;
                // gm.GetComponent<GameManager>().shouldStart = true; break;
            case "QuitButton":
                GameManager.APP_QUIT_NOW = true; break;
            // gm.GetComponent<GameManager>().APP_QUIT_NOW = true; break;
            case "ID_NameButton":
                GameManager.startMenu_ID_name_btn = true; break;
            case "PauseButton":
                GameManager.isPaused = true; GameManager.PRButton_pressed = true; break;
            case "ResumeButton":
                GameManager.isPaused = false; GameManager.PRButton_pressed = true; break;

            default: break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
