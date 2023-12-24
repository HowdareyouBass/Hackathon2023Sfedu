using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResistanceObserver : MonoBehaviour
{
    // How much do you need to wait for all resistances to stay
    [SerializeField] private float _waitInSeconds;

    [SerializeField] private GameObject[] _checks;

    private bool AllResistancesGreen()
    {
        foreach(GameObject check in _checks)
        {
            if (check.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private Coroutine _waitingCoroutine;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
        _waitingCoroutine = StartCoroutine(WaitingRoutine());
    }

    private IEnumerator WaitingRoutine()
    {
        float time = 0.0f;
        while (time < _waitInSeconds)
        {
            if (!AllResistancesGreen())
            {
                time = 0.0f;
            }
            time += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        _button.interactable = true;
        yield break;
    }
}
