using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public IEnumerator WaitForSync(Action action)
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

        action.Invoke();
        yield break;
    }
    public void ToTrainingPage()
    {
        Action action = new Action(() => _gameObjects.SetActive(true));
        action += () => _trainingPage.SetActive(true);
        StartCoroutine(WaitForSync(action));
    }
    public void ToAnalPage()
    {
        Action action = new Action(() => _analPage.SetActive(true));
        StartCoroutine(WaitForSync(action));
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
