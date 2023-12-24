using System.Runtime;
using Unity.Mathematics;
using UnityEngine;

public class Plug : MonoBehaviour
{
    public static double Relaxation = 0.1;
    public static double Desired = 0.5;

    private const double AMOUNT = 0.25f;

    private double _desiredConcetration = 0.5;
    private double _realConcetration = 0.1;

    private void OnEnable()
    {
        Player.Instance.GetComponent<Shooter>().Shot += addDesiredConcetration;
        Player.Instance.GetComponent<Health>().Hit += addDesiredConcetration;
    }
    private void OnDisable()
    {
        Player.Instance.GetComponent<Shooter>().Shot -= addDesiredConcetration;
        Player.Instance.GetComponent<Health>().Hit -= addDesiredConcetration;

    }

    private void FixedUpdate()
    {
        _realConcetration = math.lerp(_realConcetration, _desiredConcetration, 0.02);
        _realConcetration = math.clamp(_realConcetration, 0.05, 1);
        _desiredConcetration -= 0.005;
        _desiredConcetration = math.clamp(_desiredConcetration, 0.05, 1);

        Relaxation = _realConcetration;
        Desired = _desiredConcetration;
    }

    private void addDesiredConcetration()
    {
        Debug.Log("ADD)");
        _desiredConcetration += AMOUNT;
    }
}