using UnityEngine;
using TMPro;

public class ControlsButton : MonoBehaviour
{
    public PlayerController pc; 
    public TextMeshProUGUI buttonText;
    public bool control;

    void Start()
    {
        // Initialize button text
        UpdateButtonText();
    }

    public void ToggleControlScheme()
    {
        control = !control;
        pc.swapInputMethod();
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        // Update the button text to reflect the current control scheme
        buttonText.text = control ? "Current Scheme: Mouse + Keyboard" : "Current Scheme: Keyboard Only";
    }
}
