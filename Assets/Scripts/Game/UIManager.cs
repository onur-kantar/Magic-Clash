using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject buttonsParent;
    [SerializeField] GameObject joystick;

    public void ActiveButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }
    public void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
    public void ActiveJoystick()
    {
        joystick.SetActive(true);

    }
    public void DisableJoystick()
    {
        joystick.SetActive(false);

    }
    public void ScrollDown()
    {
        buttonsParent.SetActive(false);
    }
    public void ScrollUp()
    {
        buttonsParent.SetActive(true);
    }
}
