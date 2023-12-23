using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationsController : MonoBehaviour
{
    private Animator _playerAnimator;
    private Health _playerHealth;

    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerHealth = GetComponent<Health>();

        _playerHealth.Hit += PlayTwitchAnimation;
    }

    private void PlayTwitchAnimation()
    {
        _playerAnimator.Play("PlayerHit");
    }

}
