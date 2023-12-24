using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concetration : MonoBehaviour
{
    public float Value;
    public event Action Changed;

    private void Awake()
    {
        Value = 0.05f;
    }

    private void Update()
    {
        Value = (float)Plug.Concetration;
        Changed?.Invoke();
    }
}
