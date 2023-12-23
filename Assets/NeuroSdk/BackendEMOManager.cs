using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendEMOManager : MonoBehaviour
{
    public static BackendEMOManager Instance;

    [SerializeField] private GameObject _deviceSearchEMOScreen;
    [SerializeField] private GameObject _resistEMOScreen;
    [SerializeField] private GameObject _menuEMOScreen;
    [SerializeField] private GameObject _trainerEMOScreen;
    [SerializeField] private GameObject _analysisEMOScreen;
    
        
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
        _deviceSearchEMOScreen.gameObject.SetActive(true);
    }

    public void ToMenuPage()
    {
        _menuEMOScreen.gameObject.SetActive(true);
    }


    public void ToTrainPage()
    {
        _trainerEMOScreen.SetActive(true);
    }

    public void ToAnalPage() 
    {
        _analysisEMOScreen.SetActive(true);
    }

    public void ToResistPage() 
    {
        _resistEMOScreen.SetActive(true);
        _deviceSearchEMOScreen.SetActive(false);
    }


}
