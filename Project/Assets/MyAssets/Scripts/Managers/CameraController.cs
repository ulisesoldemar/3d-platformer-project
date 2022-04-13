using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [Header("References")]
    // Necesitamos acceder a la cámara de cinemachine para evitar que la cámara se mueva al morir
    public CinemachineBrain cmBrain;
    [SerializeField]
    Transform _mainCamera, _player;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (cmBrain == null)
        {
            Debug.LogWarning("No se asignó el CinemachineBrain");
        }
    }

    void Update()
    {
        // if (Input.GetKey(KeyCode.H))
        // {
        //     // Wip
        // }
    }
}
