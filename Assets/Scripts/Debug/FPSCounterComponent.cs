using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FPSCounterComponent : MonoBehaviour {
    private Text text;
    private int counter = 0;
    private int second;
    private int lastFrameTotal;

    private int totalFrames;
    private int totalSeconds;

    void Start () {
        text = GetComponent<Text>();
    }
	
	void Update () {
        int currentSecond = Mathf.FloorToInt(Time.time);
        if (second != currentSecond)
        {
            lastFrameTotal = counter;
            totalFrames += lastFrameTotal;
            totalSeconds++;

            counter = 0;
            second = currentSecond;
        }
        counter += 1;
        text.text = lastFrameTotal + "(" + counter + ")" + Environment.NewLine + totalFrames / Mathf.Max(1,totalSeconds);
    }
}
