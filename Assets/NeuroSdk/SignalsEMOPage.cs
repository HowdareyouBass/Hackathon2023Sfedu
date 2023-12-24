using UnityEngine;
using SignalMath;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using NeuroSDK;
using Unity.Mathematics;

public class SignalsEMOPage : MonoBehaviour, IPage
{
    public static double Relaxation { get; private set; }
    public static double Concetration { get; private set; }

    private List<BrainBitSignalData> _signalData = new List<BrainBitSignalData>();
    private readonly object locker = new object();
    private EegEmotionalMath _math;

    private bool _started = false;

    public void ChannelsDropdownItemSelected(TMP_Dropdown dropdown)
    {
        //BackendManager.Instance.ChartManager.SelectedChannel = dropdown.value;
    }

    public void UpdateSignal()
    {
        if (_started)
        {
            BrainBitController.Instance.StopSignal();
        }
        else
        {
            BrainBitController.Instance.StartSignal((samples) =>
            {

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
                                // Debug.Log(_signalData[i].T3 - _signalData[i].O1);
                                samples1[i].RightBipolar = _signalData[i].T4 - _signalData[i].O2;
                                // Debug.Log(_signalData[i].T3 - _signalData[i].O1);
                            }



                            _math.PushData(samples1);
                            _math.ProcessDataArr();
                            // Debug.Log(_math.IsBothSidesArtifacted());
                            bool calibrationFinished = _math.CalibrationFinished();
                            // and calibration progress
                            int calibrationProgress = _math.GetCallibrationPercents();
                            // Debug.Log("C: " + calibrationProgress.ToString());
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
        _started = !_started;
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        CalibrAndSignal();
        Enter();
    }

    private void OnDisable()
    {
        Exit();
    }

    public void CalibrAndSignal()
    {
        int samplingFrequency = 250;
        var mls = new MathLibSetting
        {
            sampling_rate = (uint)samplingFrequency,
            process_win_freq = 25,
            n_first_sec_skipped = 4,
            fft_window = (uint)samplingFrequency * 4,
            squared_spectrum = true,
            bipolar_mode = true,
            channels_number = 4,
            channel_for_analysis = 0
        };

        var ads = new ArtifactDetectSetting
        {
            art_bord = 110,
            allowed_percent_artpoints = 70,
            raw_betap_limit = 800_000,
            total_pow_border = (int)(8 * 1e7),
            global_artwin_sec = 4,
            spect_art_by_totalp = true,
            num_wins_for_quality_avg = 100,
            hanning_win_spectrum = false,
            hamming_win_spectrum = true
        };

        var sads = new ShortArtifactDetectSetting
        {
            ampl_art_detect_win_size = 200,
            ampl_art_zerod_area = 200,
            ampl_art_extremum_border = 25
        };

        var mss = new MentalAndSpectralSetting
        {
            n_sec_for_averaging = 2,
            n_sec_for_instant_estimation = 2
        };

        _math = new EegEmotionalMath(mls, ads, sads, mss);
        _math.StartCalibration();
        UpdateSignal();
    }

    public void Enter()
    {
        //_updateDataCoroutine = StartCoroutine(UpdateData());
    }

    public void Exit()
    {
        BrainBitController.Instance.StopSignal();
    }
}
