using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class ControlJuego : MonoBehaviour
{
    public GameState gameState = new GameState(GameState.StateGame.inGame);

    private Transform cameraTransform;
    private GameObject pausa;
    private GameObject inventarioMenu;


    // Start is called before the first frame update
    void Start()
    {
        RecibirVariables();
        inventarioMenu.transform.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.game == GameState.StateGame.inGame)
            MovimientoJugador.instance.MovimientoPersonaje();

        // Configuración de pausa
        if (Input.GetKeyDown(KeyCode.Escape) && gameState.game == GameState.StateGame.inGame)
        {
            gameState.PauseGame();
            pausa.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inventarioMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameState.game == GameState.StateGame.pause)
        {
            gameState.ResumeGame();
            pausa.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventarioMenu.SetActive(true);
        }

        // Mostrar inventario
        if (Input.GetKeyDown(KeyCode.Tab) && gameState.game == GameState.StateGame.inGame)
        {
            gameState.InventoryGame();
            inventarioMenu.transform.GetComponent<CanvasGroup>().alpha = 100;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && gameState.game == GameState.StateGame.inInventory)
        {
            gameState.ResumeGame();
            inventarioMenu.transform.GetComponent<CanvasGroup>().alpha = 0;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void RecibirVariables()
    {
        cameraTransform = ConfiguracionJuego.instance.cameraTransform;
        pausa = ConfiguracionJuego.instance.pausa;
        inventarioMenu = ConfiguracionJuego.instance.inventarioMenu;
    }
}
