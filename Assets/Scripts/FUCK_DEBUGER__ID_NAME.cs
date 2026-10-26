using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FUCK_DEBUGER__ID_NAME : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sceneButtonText;
    [SerializeField] private TextMeshProUGUI sceneText;

    private void Start()
    {
        // 醒來的當下，如果場景上有 GameManager，就主動塞給它
        if (GameManager.gm != null)
        {
            GameManager.gm.RegisterUiReferences(sceneButtonText, sceneText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
