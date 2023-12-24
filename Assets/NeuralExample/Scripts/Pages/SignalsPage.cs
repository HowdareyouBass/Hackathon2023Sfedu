using UnityEngine;
using SignalMath;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using NeuroSDK;

public class SignalsPage : MonoBehaviour, IPage
{
    [SerializeField] private TextMeshProUGUI _StartSignalText;

    [SerializeField] private ChartManager AlphaRuthm;
    [SerializeField] private ChartManager BetaRuthm;

    public static double Relaxation { get; private set; }
    public static double Concetration { get; private set; }

    private IEnumerator _updateChartsCoroutine;
    private List<BrainBitSignalData> _signalData = new List<BrainBitSignalData>();
    private readonly object locker = new object();
    private EegEmotionalMath _math;

    private bool _started = false;
    private bool started {
        get { return _started; }
        set
        {
            if (value != _started) {
                _started = value;
                _StartSignalText.text = _started ? "Stop" : "Start";
            }

        }
    }
    
    public void ChannelsDropdownItemSelected(TMP_Dropdown dropdown)
    {
        //BackendManager.Instance.ChartManager.SelectedChannel = dropdown.value;
    }

    private IEnumerator UpdateCharts() {
        while (true) {
            lock (locker) 
            {
                int samplesCount = _signalData.Count;
                if (samplesCount > 0) {
                    var dataAlpha = new double[samplesCount];
                    var dataBeta = new double[samplesCount];

                    for (int i = 0; i < samplesCount; i++)
                    {
                        dataAlpha[i] = Relaxation;
                        dataBeta[i] = Concetration;
                    }

                    AlphaRuthm.AddData(dataAlpha);
                    BetaRuthm.AddData(dataBeta);

                    _signalData.Clear();
                }
            } 

            yield return new WaitForSeconds(0.06f);
        }
    }

    public void UpdateSignal()
    {
        if (started)
        {
            BrainBitController.Instance.StopSignal();
        }
        else 
        {
            BrainBitController.Instance.StartSignal((samples) => {

                if (samples != null && samples.Length > 0) 
                {
                    lock (locker)
                    {
                        _signalData.Clear();
                        _signalData.AddRange(samples);

                        int samplesCount = _signalData.Count;
                        if (samplesCount > 0)
                        {
                            var samples1 = new RawChannels[_signalData.Count];
                            for (int i = 0; i < _signalData.Count; i++)
                            {
                                samples1[i].LeftBipolar = _signalData[i].T3 - _signalData[i].O1;
                                samples1[i].RightBipolar = _signalData[i].T4 - _signalData[i].O2;
                            }



                            _math.PushData(samples1);
                            _math.ProcessDataArr();
                            bool calibrationFinished = _math.CalibrationFinished();
                            // and calibration progress
                            int calibrationProgress = _math.GetCallibrationPercents();
                            while (!calibrationFinished)
                            {
                                // Wait for the calibration to finish
                            }

                            MindData[] mentalData = _math.ReadMentalDataArr();
                            for (int i = 0; i < mentalData.Length; i++)
                            {
                                Concetration = mentalData[i].RelAttention;
                                Debug.Log(mentalData[i].RelAttention);
                                Debug.Log(mentalData[i].RelRelaxation);
                                Relaxation = mentalData[i].RelRelaxation;
                            }
                            SpectralDataPercents[] spData = _math.ReadSpectralDataPercentsArr();
                            _signalData.Clear();

                        }
                    }


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
        _updateChartsCoroutine = UpdateCharts();
        StartCoroutine(_updateChartsCoroutine);
    }

    public void Exit()
    {
        StopCoroutine(_updateChartsCoroutine);
        BrainBitController.Instance.StopSignal();
    }
}
