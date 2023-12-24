using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharteUpdater : MonoBehaviour
{
    [SerializeField] private ChartManager AlphaRuthm;
    [SerializeField] private ChartManager BetaRuthm;

    private List<double> _alpha;
    private List<double> _beta;

    private void Awake()
    {
        _alpha = new List<double>();
        _beta = new List<double>();
    }

    private void Update()
    {
        _alpha.Add(BrainBitSignalReader.Relaxation);
        _beta.Add(BrainBitSignalReader.Concetration);
    }

    private void FixedUpdate()
    {
        if (_alpha.Count == 0 || _beta.Count == 0)
        {
            return;
        }
        AlphaRuthm.AddData(_alpha.ToArray());
        BetaRuthm.AddData(_beta.ToArray());

        _alpha.Clear();
        _beta.Clear();
    }
}
