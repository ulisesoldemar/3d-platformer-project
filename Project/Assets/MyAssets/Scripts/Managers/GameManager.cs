using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    Vector3 _respawnPosition;
    //Intancia del efecto de muerte
    public GameObject DeathEffect;
    //Lleva la cuenta actual de monedas
    public int currentCoins;
    //Almacena el sonido que se desea reproducir
    public int musicPlaying;
    //Música de final de nivel
    public int levelEndMusic;
    //Nivel que se desea cargar a continuación
    public string levelToLoad;
    //Variable para saber cuando respawneamos en el jefe final
    public bool isRespawning;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _respawnPosition = PlayerController.Instance.transform.position;
        //Se asegura de que la pantalla de pausa esté desactivada
        UIManager.Instance.pauseScreen.SetActive(false);
        //Reestablece el contador de monedas
        addCoins(0);
    }

    void Update()
    {
        //Espera la tecla Esc para pausar o despausar el juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void Respawn()
    {
        // Se inicia la corutina llamando la función de la misma
        StartCoroutine(RespawnWaiter());
        HealthManager.Instance.PlayerKilled();
    }

    // CoRutina para dar tiempo de espera al momento de morir
    IEnumerator RespawnWaiter()
    {
        //Efecto de sonido para cuando se hace daño el player
        AudioManager.instance.PlaySFX(5);
        // Se "apaga" el personaje
        PlayerController.Instance.gameObject.SetActive(false);
        // Se desactiva el seguimiento de la cámara al personaje cuando muere
        // por eso es pública la variable
        CameraController.Instance.cmBrain.enabled = false;
        // Se habilita la transición hacia negro
        UIManager.Instance.FadeToBlack = true;

        isRespawning = true;

        Instantiate(DeathEffect, PlayerController.Instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.Instance.transform.rotation);

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

        isRespawning = false;
    }

    // Setter del punto de respawn
    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        _respawnPosition = spawnPoint;
    }
    //Suma cantidad de monedas y actualiza UI
    public void addCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.Instance.coinText.text = "" + currentCoins;
    }

    //Pausar o despausar juego
    public void PauseUnpause()
    {
        //Condiciones cuando la pantalla de pausa se encuentra activada en la jerarquía
        if (UIManager.Instance.pauseScreen.activeInHierarchy)
        {
            UIManager.Instance.pauseScreen.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            //Reproduce el audio especificado en el editor
            AudioManager.instance.PlayMusic(musicPlaying);
        }
        else//Condiciones cuando la pantalla de pausa se encuentra activada en la jerarquía
        {
            UIManager.Instance.pauseScreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.CloseOptions();
            Time.timeScale = 0f;
            //Pausa el audio previamente especificado
            AudioManager.instance.PauseMusic(musicPlaying);
        }
    } 

    //Corutina de fin de nivel
    public IEnumerator LevelEndWaiter()
    {
        //Activar la musica de final de nivel
        AudioManager.instance.PlayMusic(levelEndMusic);
        //Activa trigger para desactivar el movimiento del jugador
        PlayerController.Instance.stopMove = true;
        //Tiempo de espera antes de continuar con la ejecución
        yield return new WaitForSeconds(4f);

        //Desbloquear nivel con el nombre de la escena activa
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        //Verificar si el jugador ya agarro monedas
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins"))
        {
            //Verificar si las monedas actuales conseguidas son mayores que las guardadas en playerPrefs
            if (currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins"))
            {
                //Sobreescribir y guardar el numero total de monedas
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
            }
        }
        else
        {
            //Si no hay ningun valor en PlayerPrefs de las monedas, de todas maneras mostramos las actuales
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
        }

        //Solicita al SceneManager cargar la siguiente escena
        SceneManager.LoadScene(levelToLoad);
    }
}
