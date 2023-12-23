using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class TargetEffectDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private TargetDetector _targetDetector;

    private void OnEnable()
    {
        _targetDetector.TargetChanged += Draw;
    }
    private void OnDisable()
    {
        _targetDetector.TargetChanged -= Draw;
    }

    private void Draw()
    {
        Instantiate(_effectPrefab, _targetDetector.CurrentTarget);
    }
}
