using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering;

public class Test : MonoBehaviour
{

    [SerializeField] private TMPro.TMP_Text t1;
    [SerializeField] private TMPro.TMP_Text t2;
    [SerializeField] private TMPro.TMP_Text t3;
    [SerializeField] private TMPro.TMP_Text t4;
    [SerializeField] private TMPro.TMP_Text t5;
    [SerializeField] private TMPro.TMP_Text t6;

    private void Update()
    {
        t1.text = BrainBitSignalReader.Relaxation.ToString();
        t2.text = BrainBitSignalReader.Concetration.ToString();
        double cum = BrainBitSignalReader.Concetration + BrainBitSignalReader.Relaxation;
        t3.text = cum.ToString();
        t4.text = (BrainBitSignalReader.Concetration / cum).ToString();
        t5.text = (BrainBitSignalReader.Relaxation / cum).ToString();
        t6.text = BrainBitSignalReader.AreThereArtifacts.ToString();
    }
}