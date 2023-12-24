using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackendEMOManager : MonoBehaviour
{
    public static BackendEMOManager Instance;

    [SerializeField] private GameObject _deviceSearchEMOScreen;
    [SerializeField] private GameObject _resistEMOScreen;
    [SerializeField] private GameObject _menuEMOScreen;
    [SerializeField] private GameObject _analPage;
    [SerializeField] private GameObject _trainingPage;

    [SerializeField] private GameObject _bitSignalReader;
    
        
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
    public void ToTrainingPage()
    {
        _menuEMOScreen.SetActive(false);
        _trainingPage.SetActive(true);
    }
    public void ToAnalPage()
    {
        _menuEMOScreen.SetActive(false);
        _analPage.SetActive(true);
    }

    public void ToMenuPage()
    {
        _menuEMOScreen.gameObject.SetActive(true);
        _bitSignalReader.SetActive(true);
    }

    public void ToResistPage() 
    {
        _resistEMOScreen.SetActive(true);
        _deviceSearchEMOScreen.SetActive(false);
    }


}
