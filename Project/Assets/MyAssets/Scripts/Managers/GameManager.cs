using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    Vector3 _respawnPosition;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _respawnPosition = PlayerController.Instance.transform.position;
    }

    void Update()
    {

    }

    public void Respawn()
    {
        // Se inicia la corutina llamando la función de la misma
        StartCoroutine(RespawnWaiter());
    }

    // CoRutina para dar tiempo de espera al momento de morir
    IEnumerator RespawnWaiter()
    {
        // Se "apaga" el personaje
        PlayerController.Instance.gameObject.SetActive(false);
        // Se desactiva el seguimiento de la cámara al personaje cuando muere
        // por eso es pública la variable
        CameraController.Instance.cmBrain.enabled = false;
        // Se habilita la transición hacia negro
        UIManager.Instance.FadeToBlack = true;

        // Espera por 1 segundo para ejecutar las líenas siguientes
        yield return new WaitForSeconds(1f);
        
        // Se habilita la transición desde el negro
        UIManager.Instance.FadeFromBlack = true;
        // Se restablece la posición del personaje
        PlayerController.Instance.transform.position = _respawnPosition;
        // Se reactiva la cámara
        CameraController.Instance.cmBrain.enabled = true;
        // Se "reactiva el personaje"
        PlayerController.Instance.gameObject.SetActive(true);
        // Se reinicia el contador de vidas
        HealthManager.Instance.ResetHealth();
    }

    // Setter del punto de respawn
    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        _respawnPosition = spawnPoint;
    }
}
