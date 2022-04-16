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
    //Velocidad del jugador
    public void SetSpeed(float speed)
    {
        _playerAnimator.SetFloat("Speed", speed);
    }
    //Verifica que el jugador se encuentre en el suelo
    public void SetGrounded(bool grounded)
    {
        _playerAnimator.SetBool("Grounded", grounded);
    }
    //Activa el salto
    public void SetJump(bool jump)
    {
        _playerAnimator.SetBool("Jump", jump);
    }
    //Controla la fuerza de la gravedad
    public void SetGravityForce(float gravityForce)
    {
        _playerAnimator.SetFloat("GravityForce", gravityForce);
    }

}
