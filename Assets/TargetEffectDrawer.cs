using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class TargetEffectDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private TargetDetector _targetDetector;

    public void Draw()
    {
        Instantiate(_effectPrefab, _targetDetector.GetTarget());
    }
}
