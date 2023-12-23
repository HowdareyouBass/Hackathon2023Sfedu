using NeuroSDK;
using SignalMath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class StartBrainBit : MonoBehaviour
{
    private List<BrainBitSignalData> _signalData = new List<BrainBitSignalData>();
    public void Calibration()
    {
        int samplingFrequency = 250;
        var mls = new MathLibSetting
        {
            sampling_rate = (uint)samplingFrequency,
            process_win_freq = 25,
            n_first_sec_skipped = 6,
            fft_window = (uint)samplingFrequency * 2,
            bipolar_mode = true,
            channels_number = 4,
            channel_for_analysis = 0
        };

        var ads = new ArtifactDetectSetting
        {
            art_bord = 110,
            allowed_percent_artpoints = 70,
            raw_betap_limit = 800_000,
            total_pow_border = (int)(3 * 1e7),
            global_artwin_sec = 4,
            spect_art_by_totalp = false,
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

        var math = new EegEmotionalMath(mls, ads, sads, mss);

        Scanner scanner = new Scanner(SensorFamily.SensorLEBrainBit);


        math.StartCalibration();

        var samples = new RawChannels[_signalData.Count];
        for (int i = 0; i < _signalData.Count; i++)
        {
            samples[i].LeftBipolar = _signalData[i].O1 - _signalData[i].T3;
            samples[i].RightBipolar = _signalData[i].O2 - _signalData[i].T4;
        }
        math.PushData(samples);


        MindData[] mentalData = math.ReadMentalDataArr();

        SpectralDataPercents[] spData = math.ReadSpectralDataPercentsArr();

        
    }

}
