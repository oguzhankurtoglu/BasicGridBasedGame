using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Page1;// Get Main Menu Parent Object
    [SerializeField] private GameObject Page2;// Get GamePlay parent object
    public static event Action OnButtonClicked; // button clicked event for check neighbours situation
    public static void ClickButton() // wrap function for event
    {
        OnButtonClicked?.Invoke();
    }
    public void StartButton() // when press to start button
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }
}
