using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    #region Variables
    public static MovimientoJugador instance;

    private float yaw, pitch;

    private Transform cameraTransform;
    private CharacterController controlador;
    private Vector3 velocidadJugador;

    private TextMeshProUGUI textoNombreObjeto;
    private GameObject currentObject;
    private int score = 0;
    private float distanciaDetector = 5.0f;

    public GameState gameState = new GameState(GameState.StateGame.inGame);

    private GameObject pausa;

    private float lerpTime = 0f;

    private Vector3 altura;

    public Inventario inventario = new Inventario();

    private bool pistaEncontrada;

    private float inclinacionActual = 0f;
    private bool estaInclinando = false;
    public bool agachado;


    //Configuracions animacion.
    //Cambio Cristobal
    private Animator animator;
    [SerializeField] private AnimatorController espadaAnimatorController;
    [SerializeField] private AnimatorController arcoAnimatorController;
    //Fin del Cambio 24/04/2024fix

    #endregion

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        RecibirVariables();

        animator = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausa.SetActive(false);
        altura = transform.localScale;
        if (cameraTransform != null)
        {
            ConfiguracionJuego.instance.MidPoint = cameraTransform.localPosition.y;
        }

        controlador = GetComponent<CharacterController>();

        float initiaPitch = cameraTransform.localEulerAngles.x;

        transform.localEulerAngles = new Vector3(initiaPitch, transform.position.y, transform.position.z);
        
    }



    void RecibirVariables()
    {
        cameraTransform = ConfiguracionJuego.instance.CamaraTransform;
        textoNombreObjeto = ConfiguracionJuego.instance.NombreObjetoTexto;
        pausa = ConfiguracionJuego.instance.PanelPausa;
    }

    public void EliminarCapaDeCullingMask()
    {
        
    }

    public void MovimientoPersonaje()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");
        Vector3 movimiento = transform.right * movimientoX + transform.forward * movimientoZ;

        // Cambio Cristobal
        if (animator != null)
        {
            animator.SetFloat("MovimientoX", movimientoX);
            animator.SetFloat("MovimientoZ", movimientoZ);
        }
        //Fin Cambio 16-03-2024

        //gravedad
        if (controlador.isGrounded && velocidadJugador.y < 0) velocidadJugador.y = 0f;
        velocidadJugador.y += ConfiguracionJuego.instance.Gravedad * Time.deltaTime;
        controlador.Move(velocidadJugador * Time.deltaTime);

        //salto
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            velocidadJugador.y = 
                Mathf.Sqrt(ConfiguracionJuego.instance.AlturaSalto * -2f * ConfiguracionJuego.instance.Gravedad);

        // Esprintar
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movimiento *= ConfiguracionJuego.instance.MultiplicadorSprint;

            // Cambio Cristobal
            animator.SetFloat("MovimientoX", movimientoX * ConfiguracionJuego.instance.MultiplicadorSprint);
            animator.SetFloat("MovimientoZ", movimientoZ * ConfiguracionJuego.instance.MultiplicadorSprint);
            //Fin Cambio 16-03-2024
        }
        // Agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movimiento /= ConfiguracionJuego.instance.MultiplicadorSprint;
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

        yaw += ConfiguracionJuego.instance.VelocidadH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0f, yaw, 0f);

        if (cameraTransform != null)
        {
            pitch -= ConfiguracionJuego.instance.VelocidadV * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }

        controlador.Move(movimiento * ConfiguracionJuego.instance.Velocidad * Time.deltaTime);


        // Simulador de que se está moviendo
        if (Mathf.Abs(movimientoX) > 0.1f || Mathf.Abs(movimientoZ) > 0.1f)
        {
            ConfiguracionJuego.instance.Timer += Time.deltaTime * ConfiguracionJuego.instance.BobbingSpeed;
            float waveslice = Mathf.Sin(ConfiguracionJuego.instance.Timer);
            float totalAxes = Mathf.Abs(movimientoX) + Mathf.Abs(movimientoZ);
            totalAxes = Mathf.Clamp(totalAxes, 0f, 0.5f);
            float translateChange = totalAxes * waveslice * ConfiguracionJuego.instance.BobbingAmount;

            float totalY = ConfiguracionJuego.instance.MidPoint + translateChange;
            cameraTransform.localPosition = 
                new Vector3(cameraTransform.localPosition.x, totalY, cameraTransform.localPosition.z);
        }
        else
        {
            ConfiguracionJuego.instance.Timer = 0;
            cameraTransform.localPosition = 
                new Vector3(cameraTransform.localPosition.x, 
                Mathf.Lerp(cameraTransform.localPosition.y, ConfiguracionJuego.instance.MidPoint, 
                Time.deltaTime * ConfiguracionJuego.instance.BobbingSpeed), cameraTransform.localPosition.z);
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
            inclinacionActual += Time.deltaTime * ConfiguracionJuego.instance.VelocidadPeek; // Aumenta la inclinación gradualmente
            inclinacionActual = Mathf.Min(inclinacionActual, ConfiguracionJuego.instance.AnguloMaximo); // Limita la inclinación al máximo
        }
        // Inclinarse hacia el otro lado con E
        else if (Input.GetKey(KeyCode.E))
        {
            estaInclinando = true;
            inclinacionActual -= Time.deltaTime * ConfiguracionJuego.instance.VelocidadPeek; // Aumenta la inclinación gradualmente
            inclinacionActual = Mathf.Max(inclinacionActual, ConfiguracionJuego.instance.AnguloMaximo); // Limita la inclinación al máximo en la otra dirección
        }
        // Al soltar la tecla, vuelve gradualmente a la posición original
        else if (estaInclinando)
        {
            inclinacionActual = 
                Mathf.MoveTowards(inclinacionActual, 0f, Time.deltaTime * ConfiguracionJuego.instance.VelocidadPeek);
            if (inclinacionActual == 0f)
            {
                estaInclinando = false;
            }
        }

        // Aplica la rotación
        //Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, inclinacionActual);
        //transform.localRotation = targetRotation;

    }

    bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 1.2f;
        Vector3 dir = new Vector3(0, -1, 0);
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            return true;
        }
        return false;
    }

}
