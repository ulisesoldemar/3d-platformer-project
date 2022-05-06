using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    //Bandera para cargar el nivel
    public bool canLoadLevel;
    //Nombre de la escena a cargar
    public string levelName;
    //Nivel anterior para checar si ha sido terminado
    public string levelToCheck;
    //Nombre del nivel para mostrar en el panel
    public string displayName;
    //Bandera para decirle si el nivel ha sido desbloqueado
    public bool _levelUnlocked;
    //Bandera para activar la pantalla negra cada que carga un nuevo nivel
    private bool _levelLoading;

    //Objetos para saber cual mapa esta activo
    public GameObject mapPointActive, mapPointInactive;

    private void Start()
    {
        //Checar si hay un nivel desbloqueado
        if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "")
        {
            //Activar el nivel si es desbloqueado
            mapPointActive.SetActive(true);
            //Desactivar el objeto con los materiales para cuando esta inactivo
            mapPointInactive.SetActive(false);
            //Desbloqueamos el nivel
            _levelUnlocked = true;
        }
        else
        {
            //Si no hay nivel desbloqueado
            //Desactivar el objeto con el diseño activado
            mapPointActive.SetActive(false);
            //activar el objeto con diseño inactivo
            mapPointInactive.SetActive(true);
            //No se ha pasado el nivel
            _levelUnlocked = false;
        }

        //Verificar si existe la etiqueta string "CurrentLevel"
        if(PlayerPrefs.GetString("CurrentLevel") == levelName)
        {
            //Transformar al personaje para moverlo a la posicion del ultimo nivel terminado
            PlayerController.Instance.transform.position = transform.position;
            LsResetPlayer.instance.respawnPosition = transform.position;
        }
    }

    private void Update()
    {
        //Si presiona la barra espaciadora, y verifica que ese nivel este disponible, y que este desbloqueado 
        if (Input.GetButtonDown("Jump") && canLoadLevel && _levelUnlocked && !_levelLoading)
        {
            //Activar corutina para encender la pantalla negra y cargar el nivel
            StartCoroutine("LevelLoadWaiter");
            _levelLoading = true;
        }

    }

    //Si quiere ingresar al nivel
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Se activa la bandera para poder ingresar
            canLoadLevel = true;

            //Activar el panel y el texto cuando se acerque a un nivel
            LSUIManager.instance.lNamePanel.SetActive(true);
            LSUIManager.instance.lNameText.text = displayName;
            //Verificar que el jugador haya recolectado monedas
            if (PlayerPrefs.HasKey(levelName + "_coins"))
            {
                //Obtener las monedas recolectadas del nivel
                LSUIManager.instance.coinsText.text = PlayerPrefs.GetInt(levelName + "_coins").ToString();
            }
            else
            {
                //Mostrar un texto en caso de que no se hayan agarrado monedas aun
                LSUIManager.instance.coinsText.text = "???";
            }
        }
    }

    //Ya que estemos adentro ya no podremos cargar los niveles
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = false;
            //desactivar el panel que muestra en nombre del nivel
            LSUIManager.instance.lNamePanel.SetActive(false);
        }
    }

    //Coroutine
    public IEnumerator LevelLoadWaiter()
    {
        //Detener al jugador
        PlayerController.Instance.stopMove = true;
        //Encender la pantalla en negro para cambiar de escena
        UIManager.Instance.FadeToBlack = true;

        //Esperar por 2 segundos
        yield return new WaitForSeconds(2f);

        //Cargamos la escena nombrada en el objeto por su levelname
        SceneManager.LoadScene(levelName);

        //Guardar el ultimo nivel en el que se encuentra
        PlayerPrefs.SetString("CurrentLevel", levelName);

    }
}
