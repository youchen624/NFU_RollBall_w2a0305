using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTextCollor : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private int tick = 0;
    public int colorChangeTick = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tick >= colorChangeTick)
        {
            nameText.color = Random.ColorHSV();
            tick = 0;
        } else ++tick;
    }
}
