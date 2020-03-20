using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    [SerializeField] GameObject eatTooltip;
    [SerializeField] GameObject controlsTooltip;

    public void ShowEatTooltip(bool shouldShow)
    {
        if (shouldShow)
        {
            eatTooltip.SetActive(true);
        }
        else
        {
            eatTooltip.SetActive(false);
        }
    }

    public void ShowMovementControls(bool shouldShow)
    {
        if (shouldShow)
        {
            controlsTooltip.SetActive(true);
        }
        else
        {
            controlsTooltip.SetActive(true);
        }
    }

    private void TimerToHide()
    {
        ShowMovementControls(false);
    }
}
