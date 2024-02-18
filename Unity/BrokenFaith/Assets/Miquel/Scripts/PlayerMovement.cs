using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintMultiplier = 2.0f;
    private float yaw, pitch;
    public float SpeedH = 3, SpeedV = 3;

    private float bobbingSpeed = 10f;
    private float bobbingAmount = 0.1f;
    private float midpoint = 0.5f;
    private float timer = 0;

    public Transform cameraTransform;

    private Rigidbody rb;

    public TextMeshProUGUI objectNameText;
    private GameObject currentObject;
    private int score = 0;
    public float distanceDetector = 5.0f;

    public GameState gameState = new GameState(GameState.StateGame.inGame);

    public GameObject Pausa;
    public GameObject InventarioMenu;

    private float lerpTime = 0f;

    private Vector3 altura;

    public Inventario inventario = new Inventario();

    private bool pistaEncontrada;
    public bool agachado;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Pausa.SetActive(false);
        InventarioMenu.SetActive(false);
        rb = GetComponent<Rigidbody>();
        altura = transform.localScale;
        // Cuando queramos que haya deslizamiento, cambiamos esta variable
        // rb.drag = 0;
        if (cameraTransform != null)
        {
            midpoint = cameraTransform.localPosition.y;
        }
        // Llenar inventario
        //LlenarInventario();
    }

    void Update()
    {
        if (gameState.game == GameState.StateGame.inGame)
            MovimientoPersonaje();
        

        // Configuración de pausa
        if (Input.GetKeyDown(KeyCode.Escape) && gameState.game == GameState.StateGame.inGame)
        {
            gameState.PauseGame();
            Pausa.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameState.game == GameState.StateGame.pause)
        {
            gameState.ResumeGame();
            Pausa.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Mostrar inventario
        if (Input.GetKeyDown(KeyCode.Tab) && gameState.game == GameState.StateGame.inGame)
        {
            gameState.InventoryGame();
            InventarioMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


    }

    void MovimientoPersonaje()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xMovement, 0.0f, zMovement) * speed * Time.deltaTime;

        // Esprintar
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }
        // Agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movement /= sprintMultiplier;
            lerpTime += Time.deltaTime / 0.5f;
            //cameraTransform.position = Vector3.Lerp(altura, altura / 2, lerpTime);
            transform.localScale = Vector3.Lerp(altura, new Vector3(altura.x, altura.y / 2, altura.z), lerpTime);
            agachado = true;
        }
        else
        {
            lerpTime = 0f;
            transform.localScale = altura;
            agachado = false;
        }

        transform.Translate(movement);

        yaw += SpeedH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0f, yaw, 0f);

        if (cameraTransform != null)
        {
            pitch -= SpeedV * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }


        // Simulador de que se está moviendo
        if (Mathf.Abs(xMovement) > 0.1f || Mathf.Abs(zMovement) > 0.1f)
        {
            timer += Time.deltaTime * bobbingSpeed;
            float waveslice = Mathf.Sin(timer);
            float totalAxes = Mathf.Abs(xMovement) + Mathf.Abs(zMovement);
            totalAxes = Mathf.Clamp(totalAxes, 0f, 0.5f);
            float translateChange = totalAxes * waveslice * bobbingAmount;

            float totalY = midpoint + translateChange;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, totalY, cameraTransform.localPosition.z);
        }
        else
        {
            timer = 0;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, Mathf.Lerp(cameraTransform.localPosition.y, midpoint, Time.deltaTime * bobbingSpeed), cameraTransform.localPosition.z);
        }

        // Apuntar
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            float distance = Vector3.Distance(Camera.main.transform.position, hit.transform.position);
            // Detectar el objeto y mostrar su nombre si estás lo suficientemente cerca
            if (distance <= distanceDetector)
            {
                if (hit.transform.gameObject != currentObject)
                {
                    currentObject = hit.transform.gameObject;
                    if (currentObject.name == "Objeto")
                    {
                        objectNameText.text = $"Usar {currentObject.name}";
                        pistaEncontrada = false;
                    }
                    else if (currentObject.name == "Pista")
                    {
                        objectNameText.text = $"Usar {currentObject.name}";
                        pistaEncontrada = true;
                    }
                    else
                    {
                        objectNameText.text = "";
                        pistaEncontrada = false;
                    }
                }

                // Incrementar la variable al hacer clic
                if (Input.GetMouseButtonDown(0))
                {
                    score++;
                    Debug.Log("Score: " + score);
                    Debug.Log("PistaEncontrada " + pistaEncontrada);
                }
                if (pistaEncontrada && Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Encontrado");
                    bool pistaGuardada = GuardarPista();
                    if (pistaGuardada)
                    {
                        inventario.MostrarInventario();
                        Destroy(currentObject);
                    }
                }
            }
            else
                objectNameText.text = "";
        }
        else
        {
            // No hay objeto detectado, limpiar el texto
            objectNameText.text = "";
            currentObject = null;
        }
    }

    public void ResumeGame()
    {
        gameState.ResumeGame();
        Pausa.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LlenarInventario ()
    {
        Municion flechas = new Municion("1", "Flechas", 3, TipoMunicion.Piedra, 5);
        Equipo arco = new Equipo("1", "Arco", TipoArma.Arco, flechas, 5);

        Equipo espada = new Equipo("2", "Espada", TipoArma.Espada, 5);

        inventario.AgregarItem(arco);
        inventario.AgregarItem(espada);

        inventario.MostrarInventario();

    }

    bool GuardarPista()
    {
        Pista pista = new Pista("1", "Primera pista", "I think human consciousnes was a tragic mistep in evolution. We became too self-aware; nature created an aspect of nature separte from itself: we are creatures that should not exist by natural law");
        inventario.AgregarItem(pista);
        return true;
    }

}
