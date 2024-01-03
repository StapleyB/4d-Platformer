using UnityEngine;
using UnityEngine.UI;
using System;

public class FourthDimension : MonoBehaviour {
    
    private float min = 0;
    private float max = 10;
    public float current_4d;
    private float target_4d;
    public float scale;
    public float springiness;
    public Slider bar;
    public PlayerController player;

    void Start()
    {
        bar.minValue = min;
        bar.maxValue = max;
        current_4d = 0;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Z Vertical") != 0 || player.dw != 0)
        {
            target_4d = player.w;
        } else
        {
            target_4d += Input.mouseScrollDelta.y * scale;
            target_4d = Math.Max(min, target_4d);
            target_4d = Math.Min(max, target_4d);
        }

        current_4d += (target_4d - current_4d) / (springiness*1f);
        if (Math.Abs(current_4d - target_4d) < 0.01) { current_4d = target_4d; }
        bar.value = current_4d;

    }

}
