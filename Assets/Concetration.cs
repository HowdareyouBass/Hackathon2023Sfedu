using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concetration : MonoBehaviour
{
    public float Value;
    public event Action Changed;

    private void Update()
    {
        Value = (float)Plug.Relaxation;
        Changed?.Invoke();
    }
}
