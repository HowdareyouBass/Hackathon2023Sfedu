using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{
    public Button button;
    public bool foundCalibrations = false;
    public void WorkButton(List<bool> calibrations)//gavno
    {
        bool flag = true;
        foreach (var calibration in calibrations)
            if (!calibration)
                flag = false;
        button.enabled = flag;
    }
}
