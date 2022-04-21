using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Variable para cargar el primer nivel
    public string firstLevel;

    //Variable para cargar el nivel seleccionado
    public string levelSelect;

    //Variable para hacer referencia al boton
    public GameObject continueButton;

    //Lista con todos los niveles
    public string[] levelNames;

    private void Start()
    {
        //Si el jugador ha avanzado algun nivel
        if (PlayerPrefs.HasKey("Continue"))
        {
            //Se activa el boton de continue
            continueButton.SetActive(true);
        }
        else
        {
            //si no ha realizado ningun nivel, entonces se resetea el progreso del jugador 
            resetProgress();
        }
    }

    //Cuando el usuario presione en el boton "Nuevo juego"
    public void newGame()
    {
        //Se carga el primer nivel
        SceneManager.LoadScene(firstLevel);
        //Se guarda la informacion de las preferencias del usuario entre las sessiones
        //Guardamos un entero con el nombre de continue
        PlayerPrefs.SetInt("Continue", 0);
        //Le decimos cual es nuestro nivel actual para caer justo en el primer nivel
        PlayerPrefs.SetString("CurrentLevel", firstLevel);
        //Bloquear todos los niveles
        resetProgress();
    }

    //Cuando el usuario presione en el boton "Continuar"
    public void continueGame()
    {
        //Se carga el nivel seleccionado
        SceneManager.LoadScene(levelSelect);
    }


    //Cuando el usuario presione en el boton "Salir"
    public void quitGame()
    {
        //Termina el juego
        Application.Quit();
    }

    //Funcion para que se elimine todo el progreso del jugador
    public void resetProgress()
    {
        //Bloquear todos los niveles
        for(int i = 0; i < levelNames.Length; i++)
        {
            //asignar 0 al nivel para bloquearlo
            PlayerPrefs.SetInt(levelNames[i] + "_unlocked", 0);
        }
    }
}
