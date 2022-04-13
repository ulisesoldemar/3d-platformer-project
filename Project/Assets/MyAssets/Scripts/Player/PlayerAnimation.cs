using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Animator _playerAnimator;

    void Start()
    {
        if (_playerAnimator == null)
        {
            Debug.LogWarning("No hay asignado un Animator");
        }
    }

    public void SetSpeed(float speed)
    {
        _playerAnimator.SetFloat("Speed", speed);
    }

    public void SetGrounded(bool grounded)
    {
        _playerAnimator.SetBool("Grounded", grounded);
    }

    public void SetJump(bool jump)
    {
        _playerAnimator.SetBool("Jump", jump);
    }

    public void SetGravityForce(float gravityForce)
    {
        _playerAnimator.SetFloat("GravityForce", gravityForce);
    }

}
