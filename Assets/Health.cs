using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action Death;
    public event Action<float> Reduced;
    public event Action Hit;

    [SerializeField] private float _maxHealth;
    public float CurrentHealth{get; private set;}

    public void Reduce(float amount)
    {
        if (amount < 0){ throw new System.ArgumentOutOfRangeException("Health reduced cannot be negative"); }

        CurrentHealth -= amount;
        Reduced?.Invoke(amount);
        Hit?.Invoke();
        if (CurrentHealth <= 0)
        {
            Death?.Invoke();
            CurrentHealth = 0;
        }
    }

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }
}
