using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private TargetDetector _targetDetector;

    public void KillTarget()
    {
        _targetDetector.CurrentTarget.GetComponent<Health>().Reduce(1000000);
    }
}