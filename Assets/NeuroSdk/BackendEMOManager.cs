using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackendEMOManager : MonoBehaviour
{
    public static BackendEMOManager Instance;
    public bool EnableSignalReader = false;

    [SerializeField] private GameObject _deviceSearchEMOScreen;
    [SerializeField] private GameObject _resistEMOScreen;
    [SerializeField] private GameObject _menuEMOScreen;
    [SerializeField] private GameObject _analPage;
    [SerializeField] private GameObject _trainingPage;

    [SerializeField] private GameObject _bitSignalReader;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _gameObjects;
    
        
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    public void ToSearchPage()
    {
        _deviceSearchEMOScreen.SetActive(true);
    }

    public IEnumerator ToTrainPageCoroutine()
    {
        if (_bitSignalReader.activeInHierarchy)
        {
            _loadingScreen.SetActive(true);
            while (BrainBitSignalReader.CalibrationProgress != 100)
            {
                Debug.Log("Waiting for the calibration");
                yield return null;
            }
            _loadingScreen.SetActive(false);
        }

        _deviceSearchEMOScreen.SetActive(false);
        _menuEMOScreen.SetActive(false);
        _resistEMOScreen.SetActive(false);
        _gameObjects.SetActive(true);
        _trainingPage.SetActive(true);
        yield break;
    }
    public void ToTrainingPage()
    {
        StartCoroutine(ToTrainPageCoroutine());
        // if (_bitSignalReader.activeInHierarchy)
        // {
        //     _loadingScreen.SetActive(true);
        //     while (BrainBitSignalReader.CalibrationProgress != 100)
        //     {
        //         Debug.Log("Waiting for the calibration");
        //     }
        //     _loadingScreen.SetActive(false);
        // }

        // _deviceSearchEMOScreen.SetActive(false);
        // _menuEMOScreen.SetActive(false);
        // _resistEMOScreen.SetActive(false);
        // _gameObjects.SetActive(true);
        // _trainingPage.SetActive(true);
    }
    public void ToAnalPage()
    {
        _menuEMOScreen.SetActive(false);
        _resistEMOScreen.SetActive(false);
        _analPage.SetActive(true);
    }

    public void ToMenuPage()
    {
        _menuEMOScreen.gameObject.SetActive(true);
        if (EnableSignalReader)
        {
            _bitSignalReader.SetActive(true);
        }
    }

    public void ToResistPage() 
    {
        EnableSignalReader = true;
        _resistEMOScreen.SetActive(true);
        _deviceSearchEMOScreen.SetActive(false);
    }


}
