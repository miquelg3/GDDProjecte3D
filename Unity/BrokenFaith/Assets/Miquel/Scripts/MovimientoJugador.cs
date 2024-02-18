using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoJugador : MonoBehaviour
{
    public static MovimientoJugador instance;

    private float velocidad = 5.0f;
    private float multiplicadorSprint = 2.0f;
    private float yaw, pitch;
    private float VelocidadH = 3, VelocidadV = 3;

    private float bobbingSpeed = 10f;
    private float bobbingAmount = 0.1f;
    private float midpoint = 0.5f;
    private float timer = 0;

    private Transform cameraTransform;

    private Rigidbody rb;

    private TextMeshProUGUI textoNombreObjeto;
    private GameObject currentObject;
    private int score = 0;
    private float distanciaDetector = 5.0f;

    public GameState gameState = new GameState(GameState.StateGame.inGame);

    private GameObject pausa;
    private GameObject inventarioMenu;

    private float lerpTime = 0f;

    private Vector3 altura;

    public Inventario inventario = new Inventario();

    private bool pistaEncontrada;

    private float velocidadPeek = 40f;
    private float anguloMaximo = 20f;
    private float inclinacionActual = 0f;
    private bool estaInclinando = false;

    void Start()
    {
        RecibirVariables();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausa.SetActive(false);
        rb = GetComponent<Rigidbody>();
        altura = transform.localScale;
        // Cuando queramos que haya deslizamiento, cambiamos esta variable
        // rb.drag = 0;
        if (cameraTransform != null)
        {
            midpoint = cameraTransform.localPosition.y;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void RecibirVariables()
    {
        cameraTransform = ConfiguracionJuego.instance.cameraTransform;
        textoNombreObjeto = ConfiguracionJuego.instance.nombreObjetoTexto;
        pausa = ConfiguracionJuego.instance.pausa;
        inventarioMenu = ConfiguracionJuego.instance.inventarioMenu;
    }

    public void MovimientoPersonaje()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV) * velocidad * Time.deltaTime;

        // Esprintar
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movimiento *= multiplicadorSprint;
        }
        // Agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movimiento /= multiplicadorSprint;
            lerpTime += Time.deltaTime / 0.5f;
            //cameraTransform.position = Vector3.Lerp(altura, altura / 2, lerpTime);
            transform.localScale = Vector3.Lerp(altura, new Vector3(altura.x, altura.y / 2, altura.z), lerpTime);
        }
        else
        {
            lerpTime = 0f;
            transform.localScale = altura;
        }

        transform.Translate(movimiento);

        yaw += VelocidadH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0f, yaw, 0f);

        if (cameraTransform != null)
        {
            pitch -= VelocidadV * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }


        // Simulador de que se está moviendo
        if (Mathf.Abs(movimientoH) > 0.1f || Mathf.Abs(movimientoV) > 0.1f)
        {
            timer += Time.deltaTime * bobbingSpeed;
            float waveslice = Mathf.Sin(timer);
            float totalAxes = Mathf.Abs(movimientoH) + Mathf.Abs(movimientoV);
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
            if (distance <= distanciaDetector)
            {
                if (hit.transform.gameObject != currentObject)
                {
                    currentObject = hit.transform.gameObject;
                    if (currentObject.name == "Objeto")
                    {
                        textoNombreObjeto.text = $"Usar {currentObject.name}";
                        pistaEncontrada = false;
                    }
                    else if (currentObject.name == "Pista")
                    {
                        textoNombreObjeto.text = $"Usar {currentObject.name}";
                        pistaEncontrada = true;
                    }
                    else
                    {
                        textoNombreObjeto.text = "";
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
                if (pistaEncontrada && Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("Encontrado");
                    bool pistaGuardada = InventarioScript.instance.GuardarPista();
                    if (pistaGuardada)
                    {
                        inventario.MostrarInventario();
                        Destroy(currentObject);
                    }
                }
            }
            else
                textoNombreObjeto.text = "";
        }
        else
        {
            // No hay objeto detectado, limpiar el texto
            textoNombreObjeto.text = "";
            currentObject = null;
        }

        // Inclinarse
        if (Input.GetKey(KeyCode.Q))
        {
            estaInclinando = true;
            inclinacionActual += Time.deltaTime * velocidadPeek; // Aumenta la inclinación gradualmente
            inclinacionActual = Mathf.Min(inclinacionActual, anguloMaximo); // Limita la inclinación al máximo
        }
        // Inclinarse hacia el otro lado con E
        else if (Input.GetKey(KeyCode.E))
        {
            estaInclinando = true;
            inclinacionActual -= Time.deltaTime * velocidadPeek; // Aumenta la inclinación gradualmente
            inclinacionActual = Mathf.Max(inclinacionActual, -anguloMaximo); // Limita la inclinación al máximo en la otra dirección
        }
        // Al soltar la tecla, vuelve gradualmente a la posición original
        else if (estaInclinando)
        {
            inclinacionActual = Mathf.MoveTowards(inclinacionActual, 0f, Time.deltaTime * velocidadPeek);
            if (inclinacionActual == 0f)
            {
                estaInclinando = false;
            }
        }

        // Aplica la rotación
        Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, inclinacionActual);
        transform.localRotation = targetRotation;

    }

}
