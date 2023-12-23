using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event Action Died;
    public float Signal;
    public int Damage = 5;

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
        playerHealth.Hit += Twitch;
    }
    private void Update()
    {

    }
    private void Twitch()
    {
        
    }

    private void Die()
    {
        Died?.Invoke();
    }
}
