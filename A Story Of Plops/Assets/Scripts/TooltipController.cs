using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    [SerializeField] GameObject eatTooltip;

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
}
