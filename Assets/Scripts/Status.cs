using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Status : MonoBehaviour
{

    public static Status instance { get; private set; }
    private GameObject hungerDisplay;
    private GameObject thirstDisplay;
    private TextMeshProUGUI happinessDisplay;
    private TextMeshProUGUI addictionDisplay;

    private void Awake()
    {
        instance = this;

        hungerDisplay = transform.GetChild(0).GetChild(0).gameObject;
        thirstDisplay = transform.GetChild(1).GetChild(0).gameObject;
        happinessDisplay = transform.GetChild(2).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        addictionDisplay = transform.GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateHungerDisplay(float hunger)
    {
        Vector3 width = new Vector3((hunger / 100f), 1, 1);
        hungerDisplay.transform.localScale = width;
    }

    public void UpdateThirstDisplay(float thirst)
    {
        Vector3 width = new Vector3((thirst / 100f), 1, 1);
        thirstDisplay.transform.localScale = width;
    }

    public void UpdateHappinessDisplay(int happiness)
    {
        happinessDisplay.text = happiness.ToString();
    }

    public void UpdateAddictionDisplay(int addiction)
    {
        addictionDisplay.text = addiction.ToString();
    }
}
