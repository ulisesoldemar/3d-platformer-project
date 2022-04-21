using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("References")]
    [SerializeField]
    Image _blackScreen;

    [Header("Fade controls")]
    [SerializeField]
    float _fadeSpeed;
    public bool FadeToBlack { get; set; }
    public bool FadeFromBlack { get; set; }
    //Texto que almacena la cantida de hits restantes
    public Text healtText;
    //Sprite de salud
    public Image healtImage;
    //Texto que almacena la cantidad de monedas actuales
    public Text coinText;
    //Pantallas de pausa y opciones
    public GameObject pauseScreen, optionsScreen;
    //Controladores de volumen para música y SFX
    public Slider musicVolSlider, sfxVolSlider;

    //Variables para cargar el canvas de mainMenu y levelselect
    public string mainMenu, levelSelect;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (_blackScreen == null)
        {
            Debug.LogWarning("No hay asignada una imagen para desvanecer");
        }
    }

    void Update()
    {
        // Se activa la transición hacia el negro
        if (FadeToBlack)
        {
            // Se actualiza únicamente el valor del alpha, por eso los demás
            // parámetros siguen usando los mismos valores de _blackScreen
            _blackScreen.color = new Color(
                _blackScreen.color.r,
                _blackScreen.color.g,
                _blackScreen.color.b,
                // Mathf.MoveTowards permite pasar lentamente de un valor a otro
                // Desde el alpha (0) hasta el 1 (full) con la velocidad _fadeSpeed
                Mathf.MoveTowards(_blackScreen.color.a, 1f, _fadeSpeed * Time.deltaTime)
            );
            // Cuando se llega a la opacidad completa del negro, se detiene
            // la animación
            if (_blackScreen.color.a == 1f)
            {
                FadeToBlack = false;
            }
        }

        // El mismo caso anterior, pero desde el negro hacia la vista transparente
        if (FadeFromBlack)
        {
            _blackScreen.color = new Color(
                _blackScreen.color.r,
                _blackScreen.color.g,
                _blackScreen.color.b,
                // Desde el alpha (0) hasta el 1 (empty) con la velocidad _fadeSpeed
                Mathf.MoveTowards(_blackScreen.color.a, 0f, _fadeSpeed * Time.deltaTime)
            );

            if (_blackScreen.color.a == 0f)
            {
                FadeFromBlack = false;
            }
        }
    }

    //Manega el pausado y despausado del juego
    public void Resume()
    {
        GameManager.Instance.PauseUnpause();
    }
    //Activa pantalla de opciones
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }
    //Desactiva pantalla de opciones
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }
    //Pantalla de selección de nivel
    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        //Evita que cuando cambia la escena se pause
        Time.timeScale = 1f;
    }
    //Pantalla del menú principal
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        //Evita que cuando cambia la escena se pause
        Time.timeScale = 1f;
    }
    //Establece el volumen de la música
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }
    //Establece el volumen de los SFX
    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
