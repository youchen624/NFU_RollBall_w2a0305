using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;
//using System.Collections;

public class GameManager : MonoBehaviour
{
    public static bool GOD_MODE = false;

    [SerializeField] static public SFXManager SFX;

    public enum UIButtonType {
        None,
        PauseButton,
        ResumeButton
     };

    public static bool APP_QUIT_NOW = false;

    public static GameManager gm;
    public static bool startMenu_ID_name_btn = false;
    public static bool shouldStart = false;
    public static bool gameStarting = false;
    public static bool shouldReturn = false;
    public static bool shouldGameOver = false;
    public static bool gameOver_lose = false;

    public static bool isPaused = false;
    public static bool PRButton_pressed = false;

    CinemachineVirtualCamera vCam;
    CinemachineVirtualCamera vCam2;

    private static TextMeshProUGUI nameButtonText;
    private static TextMeshProUGUI nameText;

    private Dictionary<UIButtonType, Button> sceneButtons = new Dictionary<UIButtonType, Button>();


    static bool menuShow_ID = false;

    //private static GameManager _gm;
    // FUCK BUG || FUCK BUG || FUCK BUG || FUCK BUG || FUCK BUG BEGIN
    //public static GameManager gm
    //{
    //    get
    //    {
    //        // 如果別人呼叫 gm 但發現是空的，主動去場景找
    //        if (_gm == null)
    //        {
    //            _gm = FindFirstObjectByType<GameManager>();

    //            // 如果場景真的沒人放 GameManager
    //            if (_gm == null)
    //            {
    //                Debug.LogError("FUCk DAMN");
    //                // FUCK OVER DELE ALLL
    //            }
    //        }
    //        return _gm;
    //    }
    //}       // FUCK BUG || FUCK BUG || FUCK BUG || FUCK BUG || FUCK BUG END
    public void RegisterButton(UIButtonType type, Button button) {
        if (button == null) Debug.LogError("unknown button");
        sceneButtons[type] = button;
    }
    public void RegisterUiReferences(TextMeshProUGUI btnText, TextMeshProUGUI tText)
    {
        nameButtonText = btnText;
        nameText = tText;
        Debug.Log("[GM] UI 物件已成功主動綁定N");
    }
    private void Awake()
    {
        if (gm != null)
        {
            Debug.LogWarning($"[GM] 發現重複的實體，正在刪除物件：{gameObject.name}，目前留下的 GM 是：{gm.gameObject.name}");
            Destroy(gameObject);
            return;
        }
        gm = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.gm.FindVirtualCameras();
        //ID_Name_Button_FUCKER();
    }

    // Start is called before the first frame update
    void Start()
    {
        SFX = FindAnyObjectByType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (APP_QUIT_NOW)
        {
            Debug.Log("GameQuit");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if (gameStarting && PRButton_pressed)
        {
            PRButton_pressed = false;
            if (isPaused)
            {
                sceneButtons[UIButtonType.PauseButton].gameObject.SetActive(false);
                sceneButtons[UIButtonType.ResumeButton].gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                sceneButtons[UIButtonType.PauseButton].gameObject.SetActive(true);
                sceneButtons[UIButtonType.ResumeButton].gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (shouldStart)
        {
            shouldStart = false;
            gameStarting = true;
            StartCoroutine(awaitThenChangeScene("MyScene1", 1f));
        }
        if (shouldGameOver)
        {
            shouldGameOver = false;
            gameStarting = false;
            StartCoroutine(awaitThenChangeScene("MyScene3", 5f));
        }
        if (startMenu_ID_name_btn) {
            startMenu_ID_name_btn = false;
            //ID_Name_Button_FUCKER();    // FUCKING TRASH PROBLEM SOLVE
            if (menuShow_ID) {
                nameButtonText.text = "學號";
                nameText.text = "MADE BY: 陳宥儒";
            } else
            {
                nameButtonText.text = "姓名";
                nameText.text = "MADE BY: 41343131";
            };
            menuShow_ID = !menuShow_ID;
        }
    }
    IEnumerator awaitThenChangeScene(string sceneName, float awaitDuration)
    {
        // Debug.Log("test1");
        yield return new WaitForSeconds(awaitDuration);
        // Debug.Log("test2");
        SceneManager.LoadScene(sceneName);
    }


    /*
    void ID_Name_Button_FUCKER() {
        nameButtonText = GameObject.FindGameObjectWithTag("ID_NameButton")?.GetComponent<TextMeshProUGUI>();
        nameText = GameObject.FindGameObjectWithTag("ID_NameText")?.GetComponent<TextMeshProUGUI>();
        if (nameButtonText == null) Debug.Log("Fuck");
    }
    */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindVirtualCameras();
        //ID_Name_Button_FUCKER();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void FindVirtualCameras() {
        if (vCam == null)
        {
            vCam = GameObject.FindGameObjectWithTag("VCam")?.GetComponent<CinemachineVirtualCamera>();
            if (vCam == null) print("FUCK");
        }
        if (vCam2 == null)
        {
            vCam2 = GameObject.FindGameObjectWithTag("VCam2")?.GetComponent<CinemachineVirtualCamera>();
            if (vCam == null) print("FUCK");
            vCam2.gameObject.SetActive(false);
        }
    }

    public void SwitchToVirtualCamera(string vCamName)
    {
        if (vCam==null || vCam2==null)
        {
            Debug.LogError("vCam not found");
            FindVirtualCameras();
        }
        switch (vCamName)
        {
            case "VCam":
                vCam.gameObject.SetActive(true);
                vCam2.gameObject.SetActive(false);
                break;
            case "VCam2":
                vCam2.gameObject.SetActive(true);
                vCam.gameObject.SetActive(false);
                break;
            default: break;
        }
    }

}
