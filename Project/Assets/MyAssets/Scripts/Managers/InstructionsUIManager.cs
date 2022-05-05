using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsUIManager : MonoBehaviour
{
    //Instancia de la clase
    public static InstructionsUIManager instance;

    //Objetos del Canvas

    //Objeto del panel a mostrar
    public GameObject backgroundPanel;
    //Objeto que contiene todos los elementos del primer instructivo
    public GameObject instructions1;
    public GameObject instructions2;
    public GameObject instructions3;
    public GameObject instructions4;
    public GameObject instructions5;
    //Boton para salir del panel
    public GameObject closeButton;

    void Awake()
    {
        instance = this;
    }

    //Funcion para Quitar el panel
    public void CloseIstructionPanel()
    {

        //Habilitar el movimiento del jugador
        PlayerController.Instance.stopMove = false;

        //Desactivar los objetos de las instrucciones y el panel
        backgroundPanel.SetActive(false);
        instructions1.SetActive(false);
        instructions2.SetActive(false);
        instructions3.SetActive(false);
        instructions4.SetActive(false);
        instructions5.SetActive(false);

        //Mostrar el cursor en la pantalla
        Cursor.visible = false;
        //Desbloquear la camara
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

    }
}
