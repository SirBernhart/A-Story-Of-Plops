using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHidder : MonoBehaviour
{
    private void Start()
    {
        ShowCursor(false);
    }

    public void ShowCursor(bool shouldShow)
    {
        if (shouldShow)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
