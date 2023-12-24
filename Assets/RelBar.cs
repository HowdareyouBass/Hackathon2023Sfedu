using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelBar : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    // Update is called once per frame
    void Update()
    {
        double sum = BrainBitSignalReader.Concetration + BrainBitSignalReader.Relaxation;
        _renderer.material.SetFloat("_Health", (float)(BrainBitSignalReader.Relaxation / sum));
    }
}
