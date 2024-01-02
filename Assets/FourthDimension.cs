using UnityEngine;
using UnityEngine.UI;

public class FourthDimension : MonoBehaviour {
    
    private float min = 0;
    private float max = 10;
    public float current_4d;
    private float target_4d;
    public float scale;
    public float springiness;
    public Slider bar;

    void Start()
    {
        bar.minValue = min;
        bar.maxValue = max;
        current_4d = 0;
    }

    void Update()
    {
        target_4d += Input.mouseScrollDelta.y * scale;

        if (target_4d < min) { target_4d = min; }
        if (target_4d > max) { target_4d = max; }

        current_4d += (target_4d - current_4d) / (springiness*1f);
        if (current_4d - min < 0.01) { current_4d = min; }
        if (max - current_4d < 0.01) { current_4d = max; }
        bar.value = current_4d;

    }

}
