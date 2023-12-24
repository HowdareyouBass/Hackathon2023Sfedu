using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    [SerializeField] private float _timeToSelfDestructionInSeconds;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeToSelfDestructionInSeconds);
        Destroy(gameObject);
    }
}
