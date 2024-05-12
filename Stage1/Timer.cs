using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI gameTime;
    private float myTime;
    private float minute;
    private float second;

    void Update()
    {
        myTime += Time.deltaTime;
        minute = myTime / 60;
        second = myTime % 60;

        gameTime.text = string.Format("{0:D2}:{1:D2}", (int)minute, (int)second);
    }
}
