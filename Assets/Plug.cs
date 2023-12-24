using System;
using System.Runtime;
using Unity.Mathematics;
using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] private BrainBitSignalReader _signalReader;

    // Actually Concetration
    public static double Concetration = 0.1;
    public static double Desired = 0.5;

    private const double AMOUNT = 0.15f;

    private double _desiredConcetration = 0.5;
    private double _realConcetration = 0.1;

    private void OnEnable()
    {
        if (_signalReader.isActiveAndEnabled)
        {
            Player.Instance.GetComponent<Shooter>().Shot += addDesiredConcetration;
            Player.Instance.GetComponent<Health>().Hit += addDesiredConcetration;
        }
    }
    private void OnDisable()
    {
        if (_signalReader.isActiveAndEnabled)
        {
            Player.Instance.GetComponent<Shooter>().Shot -= addDesiredConcetration;
            Player.Instance.GetComponent<Health>().Hit -= addDesiredConcetration;
        }
    }

    private void FixedUpdate()
    {
        if (_signalReader.isActiveAndEnabled)
        {
            // Debug.Log("Relaxation: " + BrainBitSignalReader.Relaxation.ToString());
            double sum = BrainBitSignalReader.Relaxation + BrainBitSignalReader.Concetration;

            if (BrainBitSignalReader.Relaxation > BrainBitSignalReader.Concetration)
            {
                Concetration = 1.0 - BrainBitSignalReader.Relaxation / sum;
            }
            else
            {
                Concetration = BrainBitSignalReader.Concetration / sum;
            }

            Debug.Log("Concetration: " + Concetration);
        }
        else
        {
            _realConcetration = math.lerp(_realConcetration, _desiredConcetration, 0.02);
            _realConcetration = math.clamp(_realConcetration, 0.05, 1);
            _desiredConcetration -= 0.005;
            _desiredConcetration = math.clamp(_desiredConcetration, 0.05, 1);

            Concetration = _realConcetration;
            Desired = _desiredConcetration;
        }
    }

    private void addDesiredConcetration()
    {
        Debug.Log("ADD)");
        _desiredConcetration += AMOUNT;
    }
}