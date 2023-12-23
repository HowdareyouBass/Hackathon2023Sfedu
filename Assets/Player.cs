using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event Action Died;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Health playerHealth = GetComponent<Health>();
        playerHealth.Death += Die;
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
