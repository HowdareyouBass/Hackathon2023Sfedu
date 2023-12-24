using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadTrainingScene()
    {
        SceneManager.LoadScene("Training");
    }
    public void LoadAnalScene()
    {
        SceneManager.LoadScene("Anal");
    }
}
