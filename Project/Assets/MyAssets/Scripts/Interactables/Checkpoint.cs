using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    GameObject _cpOn;
    [SerializeField]
    GameObject _cpOff;

    void Start()
    {
        if (_cpOn == null)
        {
            Debug.LogWarning("No se asignó el objeto vacío _cpOn");
        }

        if (_cpOff == null)
        {
            Debug.LogWarning("No se asignó el objeto vacío _cpOff");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // La posición de respawn ahora es la del checkpoint
            GameManager.Instance.SetSpawnPoint(transform.position);

            // Se buscan todos los checkpoints de la escena y se almacenan en el
            // arreglo
            Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

            // Se activa el trigger y la visualización de los checkpoints que no 
            // están activos EN EL JUEGO y visceversa con los activos
            for (int i = 0; i < checkpoints.Length; ++i)
            {
                checkpoints[i]._cpOff.SetActive(true);
                checkpoints[i]._cpOn.SetActive(false);
            }
            // El checkpoint se habilita visualmente
            _cpOff.SetActive(false);
            _cpOn.SetActive(true);
            //SFX para el checkpoint
            AudioManager.instance.PlaySFX(7);
        }
    }
}
