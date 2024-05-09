using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.ShaderData;
#endif

public class ControlJuego : MonoBehaviour
{
    public GameState gameState = new GameState(GameState.StateGame.inGame);
    public Progreso progreso = new Progreso();
    private Salud personaje;

    private Transform cameraTransform;
    private GameObject pausa;
    private GameObject inventarioMenu;


    // Start is called before the first frame update
    void Start()
    {
        personaje = new Cuerpo();
        RecibirVariables();
        inventarioMenu.transform.GetComponent<CanvasGroup>().alpha = 0;
        string ruta = Path.Combine(Application.dataPath, "Guardado.xml");
        /*
        if (File.Exists(ruta))
        {
            LlamarCargarPartida();
        }*/
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
        cameraTransform = ConfiguracionJuego.instance.CamaraTransform;
        pausa = ConfiguracionJuego.instance.PanelPausa;
        inventarioMenu = ConfiguracionJuego.instance.InventarioMenu;
    }

    public void ResumeGame()
    {
        gameState.ResumeGame();
        pausa.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inventarioMenu.SetActive(true);
    }

    public void LlamarGuardarPartida()
    {
        Debug.Log("Guardando...");
        progreso.GuardarPartida(personaje.ListaSalud, InventarioScript.instance.RecibirInventario(), transform.position);
    }

    public void LlamarCargarPartida()
    {
        Debug.Log("Cargando...");

        Partida partidaCargada = progreso.CargarPartida();
        Vector3 position = new Vector3(partidaCargada.Position.X, partidaCargada.Position.Y, partidaCargada.Position.Z);
        List<Item> inventario = partidaCargada.Inventario;

        InventarioScript.instance.LlenarInventario(inventario);
        StartCoroutine(Posicionar(position));
    }

    IEnumerator Posicionar(Vector3 position)
    {
        yield return null;
        transform.position = position;
    }

}
