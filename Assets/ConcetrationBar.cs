using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcetrationBar : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    private Concetration _playerConcetration;

    private void OnEnable()
    {
        _playerConcetration = Player.Instance.GetComponent<Concetration>();
        _playerConcetration.Changed += ChangeConcetrationBarValue;
    }

    private void ChangeConcetrationBarValue()
    {
        _renderer.material.SetFloat("_Health", _playerConcetration.Value);
    }
}
