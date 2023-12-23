using NeuroSDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResistEMOPage : MonoBehaviour, IPage
{
    [SerializeField] private TextMeshProUGUI _StartResistText;

    [SerializeField] private TextMeshProUGUI _O1Value;
    [SerializeField] private TextMeshProUGUI _O2Value;
    [SerializeField] private TextMeshProUGUI _T3Value;
    [SerializeField] private TextMeshProUGUI _T4Value;
    [SerializeField] private Image T4_checkF;
    [SerializeField] private Image T3_checkF;
    [SerializeField] private Image O2_checkF;
    [SerializeField] private Image O1_checkF;

    private IEnumerator _updateValuesCoroutine;
    private BrainBitResistData _lastResistData;
    private readonly object locker = new object();

    private bool _started = false;
    private bool started
    {
        get { return _started; }
        set
        {
            if (value != _started)
            {
                _started = value;
                _StartResistText.text = _started ? "Stop" : "Start";
            }

        }
    }

    private IEnumerator UpdateValues()
    {
        while (true)
        {
            lock (locker)
            {
                _O1Value.text = string.Format("{0:F2} Ohm", _lastResistData.O1);
                if (_lastResistData.O1 < 2500000) { O1_checkF.gameObject.SetActive(false); } else { O1_checkF.gameObject.SetActive(true); };
                _O2Value.text = string.Format("{0:F2} Ohm", _lastResistData.O2);
                if (_lastResistData.O2 < 2500000) { O2_checkF.gameObject.SetActive(false); } else { O2_checkF.gameObject.SetActive(true); };
                _T3Value.text = string.Format("{0:F2} Ohm", _lastResistData.T3);
                if (_lastResistData.T3 < 2500000) { T3_checkF.gameObject.SetActive(false); } else { T3_checkF.gameObject.SetActive(true); };
                _T4Value.text = string.Format("{0:F2} Ohm", _lastResistData.T4);
                if (_lastResistData.T4 < 2500000) { T4_checkF.gameObject.SetActive(false); } else { T4_checkF.gameObject.SetActive(true); };
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdateResist()
    {
        if (started)
        {
            BrainBitController.Instance.StopResist();
        }
        else
        {
            BrainBitController.Instance.StartResist((sample) => {
                lock (locker)
                {
                    _lastResistData = sample;
                }
            });
        }
        started = !started;
    }

    private void OnEnable()
    {
        Enter();
    }

    private void OnDisable()
    {
        Exit();
    }
    public void Enter()
    {
        _updateValuesCoroutine = UpdateValues();
        StartCoroutine(_updateValuesCoroutine);
    }

    public void Exit()
    {
        StopCoroutine(_updateValuesCoroutine);
        BrainBitController.Instance.StopResist();
    }
}
