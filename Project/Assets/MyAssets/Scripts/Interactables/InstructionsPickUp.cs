using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsPickUp : MonoBehaviour
{
    public int instructionName;

    //Cuando se acerque al objeto mostrar las instrucciones
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Detener al jugador
            PlayerController.Instance.stopMove = true;

            //Habilitar el Panel
            InstructionsUIManager.instance.backgroundPanel.SetActive(true);

            //Mostrar el cursor en la pantalla
            Cursor.visible = true;
            //Bloquear la camara
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;

            //Monstrar la instruccion adecuada
            switch (instructionName)
            {
                case 1:
                    //Mostrar la instruccion 1
                    InstructionsUIManager.instance.instructions1.SetActive(true);
                    break;

                case 2:
                    //Mostrar la instruccion 2
                    InstructionsUIManager.instance.instructions2.SetActive(true);
                    break;

                case 3:
                    InstructionsUIManager.instance.instructions3.SetActive(true);
                    break;

                case 4:
                    InstructionsUIManager.instance.instructions4.SetActive(true);
                    break;

                case 5:
                    InstructionsUIManager.instance.instructions5.SetActive(true);
                    break;

            }

        }
    }
}
