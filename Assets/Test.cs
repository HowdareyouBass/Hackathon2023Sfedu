using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _text1s;

    private void Update()
    {
        _text.text = Plug.Concetration.ToString();
        _text1s.text = Plug.Desired.ToString();
    }
}