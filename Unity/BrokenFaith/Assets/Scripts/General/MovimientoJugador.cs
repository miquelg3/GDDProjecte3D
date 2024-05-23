using TMPro;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;
using UnityEngine.Rendering;

public class MovimientoJugador : MonoBehaviour
{
    #region Variables
    public static MovimientoJugador instance;

    [SerializeField] private AudioClip sonidoCaminar;
    [SerializeField] private AudioClip sonidoCorrer;
    private AudioSource audioSource;

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

    [SerializeField] private Animator animator;

    [SerializeField] private Camera camaraJugador;

    // Cambios añadidos por Javier Calabuig el dia 17/5/2024 para el funcionamiento del sistema de salud creando eventos
    public delegate void EventoAtaque(int Danyo);
    public static event EventoAtaque RecibirDanyoJugador;
    // Cambios añadidos por Javier Calabuig el dia 22/5/2024 para el funcionamiento de la interaccion con puertas y mueble
    public delegate void EventoAbrirPuerta(Transform posicion);
    public static event EventoAbrirPuerta AbrirPuerta;
    public delegate void EventoApartarMueble();
    public static event EventoApartarMueble QuitarMueble;
    public bool Caido;
    public delegate void EventoDesbloquearReja(bool PuedePasar);
    public static event EventoDesbloquearReja PasarReja;
    private bool JefeCargando;
   
    
    #endregion

    void Awake()
    {
        JefeCargando = false;
        Caido = false;
        instance = this;
    }

    void Start()
    {
        RecibirVariables();

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
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sonidoCaminar;
        audioSource.enabled = false;
    }

    public void MovimientoPersonaje()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");
        Vector3 movimiento = transform.right * movimientoX + transform.forward * movimientoZ;

        // Cambio Cristobal
        if(movimientoX != 0 || movimientoZ != 0) audioSource.enabled = true;        
        else audioSource.enabled = false;
        //Fin Cambio 22-05-2024


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


        // Esprintar
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movimiento *= ConfiguracionJuego.instance.MultiplicadorSprint;

            // Cambio Cristobal
            if (animator != null)
            {
                animator.SetFloat("MovimientoX", movimientoX * ConfiguracionJuego.instance.MultiplicadorSprint);
                animator.SetFloat("MovimientoZ", movimientoZ * ConfiguracionJuego.instance.MultiplicadorSprint);

            }
            //Fin Cambio 16-03-2024
        }
        // Agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movimiento /= ConfiguracionJuego.instance.MultiplicadorSprint;
            lerpTime += Time.deltaTime / 0.5f;
            controlador.height = 1f;
            transform.localScale = Vector3.Lerp(altura, new Vector3(altura.x, altura.y / 2, altura.z), lerpTime);
            agachado = true;
            //animator.SetBool("Agachado", agachado);
        }
        else
        {
            controlador.height = 2f;
            lerpTime = 0f;
            transform.localScale = altura;
            agachado = false;
            //animator.SetBool("Agachado", agachado);
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
                //Añadido el 22/05/2024 por Javier Calabuig Mateu para el funcionamiento de la interaccion con las puertas
                if (hit.transform.gameObject.CompareTag("Puerta") && Input.GetKeyDown(KeyCode.E) && distance <= 2f)
                {
                    AbrirPuerta?.Invoke(hit.transform);
                }
                if (hit.transform.gameObject.CompareTag("Mueble") && Input.GetKeyDown(KeyCode.E) && distance <= 2f)
                {
                    QuitarMueble?.Invoke();
                }
                if (hit.transform.gameObject.CompareTag("Reja") && Input.GetKeyDown(KeyCode.E) && distance <= 2f)
                {
                    PasarReja?.Invoke(Caido);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ManoEnemigo"))
        {
            Debug.Log("Entro daño");
            float DanyoRecibido = Random.Range(10f, 100f);
            int DanyoEntero = Mathf.RoundToInt(DanyoRecibido);
            Debug.Log(DanyoEntero);
            RecibirDanyoJugador?.Invoke(DanyoEntero);
        }
        if (other.gameObject.CompareTag("Jefe1"))
        {
            Debug.Log("Entro daño jefe");
            float DanyoRecibido = Random.Range(10f, 100f) * 1.5f;
            int DanyoEntero = Mathf.RoundToInt(DanyoRecibido);
            Debug.Log(DanyoEntero);
            RecibirDanyoJugador?.Invoke(DanyoEntero);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ManoEnemigo"))
        {
            Debug.Log("Entro daño");
            float DanyoRecibido = Random.Range(10f, 100f);
            int DanyoEntero = Mathf.RoundToInt(DanyoRecibido);
            Debug.Log(DanyoEntero);
            RecibirDanyoJugador?.Invoke(DanyoEntero);
        }
        if (collision.gameObject.CompareTag("Jefe1") && JefeCargando)
        {
            Debug.Log("Entro daño jefe");
            float DanyoRecibido = Random.Range(10f, 100f) * 1.5f;
            int DanyoEntero = Mathf.RoundToInt(DanyoRecibido);
            Debug.Log(DanyoEntero);
            RecibirDanyoJugador?.Invoke(DanyoEntero);
        }
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
    //Cambios Javier Calabuig Mateu Dia 22/05/2024 Abrir reja despues de puzzle1
    //Cambios añadidos por Javier Calabuig el dia 23/5/2024 para el funcionamiento del Jefe
    private void OnEnable()
    {
        Puzzle1.AbrirReja += HaCaido;
        JefeComportamiento.Danyo += JefeCarga;
    }
    private void OnDisable()
    {
        Puzzle1.AbrirReja -= HaCaido;
        JefeComportamiento.Danyo -= JefeCarga;
    }
    public void HaCaido()
    {
        Caido = true;
    }
    public void JefeCarga(bool haCargado)
    {
        JefeCargando = haCargado;
    }
}
