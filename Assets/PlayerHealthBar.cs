using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    private Health _playerHealth;

    private void OnEnable()
    {
        _playerHealth = Player.Instance.GetComponent<Health>();
        _playerHealth.Hit += ChangeHealthBarValue;
    }

    private void ChangeHealthBarValue()
    {
        _renderer.material.SetFloat("_Health", _playerHealth.CurrentHealth / _playerHealth.MaxHealth);
    }
}
