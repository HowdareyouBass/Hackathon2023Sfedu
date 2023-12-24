using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relax : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    private Relaxation _playerRelax;

    private void OnEnable()
    {
        _playerRelax = Player.Instance.GetComponent<Relaxation>();
        _playerRelax.Changed += ChangeConcetrationBarValue;
    }

    private void ChangeConcetrationBarValue()
    {
        _renderer.material.SetFloat("_Health", _playerRelax.Value);
    }
}
