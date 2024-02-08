using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoJugador : MonoBehaviour
{
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

    private Sprite espadaImg;
    private Sprite arcoImg;
    private Sprite pistaImg;

    private int contInventario;
    private Transform panelInventory;

    private float velocidadPeek = 40f;
    private float anguloMaximo = 20f;
    private float inclinacionActual = 0f;
    private bool estaInclinando = false;
    private List<Transform> newSlots = new List<Transform>();

    void Start()
    {
        RecibirVariables();
        // PanelInventory GameObject
        panelInventory = GameObject.Find("Canvas").transform.Find("Inventario").transform.Find("PanelInventory");
        // Asignamos el script de poder soltar a todos los slots
        Transform slotTransform;
        for (int i = 0; i < 90; i++)
        {
            slotTransform = panelInventory.Find($"Slot ({i})");
            slotTransform.AddComponent<DropSlot>();
        }


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
        // Llenar inventario
        inventarioMenu.transform.GetComponent<CanvasGroup>().alpha = 0;
        LlenarInventario();
    }

    void Update()
    {
        if (gameState.game == GameState.StateGame.inGame)
            MovimientoPersonaje();

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
        textoNombreObjeto = ConfiguracionJuego.instance.nombreObjetoTexto;
        pausa = ConfiguracionJuego.instance.pausa;
        inventarioMenu = ConfiguracionJuego.instance.inventarioMenu;

        espadaImg = ConfiguracionJuego.instance.espadaImg;
        arcoImg = ConfiguracionJuego.instance.arcoImg;
        pistaImg = ConfiguracionJuego.instance.pistaImg;
    }

    void MovimientoPersonaje()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xMovement, 0.0f, zMovement) * velocidad * Time.deltaTime;

        // Esprintar
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= multiplicadorSprint;
        }
        // Agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movement /= multiplicadorSprint;
            lerpTime += Time.deltaTime / 0.5f;
            //cameraTransform.position = Vector3.Lerp(altura, altura / 2, lerpTime);
            transform.localScale = Vector3.Lerp(altura, new Vector3(altura.x, altura.y / 2, altura.z), lerpTime);
        }
        else
        {
            lerpTime = 0f;
            transform.localScale = altura;
        }

        transform.Translate(movement);

        yaw += VelocidadH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0f, yaw, 0f);

        if (cameraTransform != null)
        {
            pitch -= VelocidadV * Input.GetAxis("Mouse Y");
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
                    bool pistaGuardada = GuardarPista();
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

    public void ResumeGame()
    {
        gameState.ResumeGame();
        pausa.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inventarioMenu.SetActive(true);
    }

    void LlenarInventario ()
    {
        // Aquí cargaremos los objetos que tendrá el jugador en el inventario al principio del todo
        Municion flechas = new Municion("1", "Flechas", true, 3, TipoMunicion.Piedra, 5);
        Equipo arco = new Equipo("1", "Arco", TipoArma.Arco, flechas, 5);

        Equipo espada = new Equipo("2", "Espada", TipoArma.Espada, 5);

        inventario.AgregarItem(arco);
        inventario.AgregarItem(espada);

        inventario.MostrarInventario();
        LlenarPanelInventory(1);
    }

    bool GuardarPista()
    {
        Pista pista = new Pista("1", "Pista", "I think human consciousnes was a tragic mistep in evolution. We became too self-aware; nature created an aspect of nature separte from itself: we are creatures that should not exist by natural law");
        inventario.AgregarItem(pista);
        Transform slotTransform = panelInventory.Find($"Slot ({contInventario})");
        newSlots.Add(slotTransform);
        LlenarPanelInventory(2);
        return true;
    }

    // 1 = Primera vez que se ejecuta el código
    // 2 = Para guardar un nuevo objeto en el inventario
    void LlenarPanelInventory(int modo)
    {
        HashSet<Item> items = inventario.GetItems();
        Transform slotTranform;
        GameObject slot;
        if (modo == 1)
        {
            foreach (Item item in items)
            {
                slotTranform = panelInventory.Find($"Slot ({contInventario})");
                slot = slotTranform.gameObject;
                if (slot != null)
                {
                    Transform newSlot = Instantiate(slotTranform, slotTranform.parent);
                    newSlot.name = $"Slot ({90 + contInventario})";
                    Debug.Log("Slot encontrado " + contInventario);
                    newSlot.GetComponent<Image>().type = Image.Type.Simple;
                    if (item.Nombre == "Espada")
                    {
                        newSlot.GetComponent<Image>().sprite = espadaImg;
                    }
                    else if (item.Nombre == "Arco")
                    {
                        newSlot.GetComponent<Image>().sprite = arcoImg;
                    }
                    else if (item.Nombre == "Pista")
                    {
                        newSlot.GetComponent<Image>().sprite = pistaImg;
                    }
                    newSlot.AddComponent<Draggable>();
                    newSlots.Add(newSlot);
                    StartCoroutine(SlotParent(newSlot, modo));
                    contInventario++;
                }
                else
                {
                    Debug.Log($"Slot no encontrado: Slot ({contInventario})");
                }
            }
        }else if (modo == 2)
        {
            Item item = items.Last();
            slotTranform = SlotSinHijo();
            slot = slotTranform.gameObject;
            if (slot != null)
            {
                Debug.Log($"Apunto de instanciar como padre el Slot ({contInventario})");
                Transform newSlot = Instantiate(slotTranform, panelInventory);
                newSlot.name = $"Slot ({90 + contInventario})";
                Debug.Log("Slot encontrado " + contInventario);
                newSlot.GetComponent<Image>().type = Image.Type.Simple;
                if (item.Nombre == "Espada")
                {
                    newSlot.GetComponent<Image>().sprite = espadaImg;
                }
                else if (item.Nombre == "Arco")
                {
                    newSlot.GetComponent<Image>().sprite = arcoImg;
                }
                else if (item.Nombre == "Pista")
                {
                    newSlot.GetComponent<Image>().sprite = pistaImg;
                }
                newSlot.AddComponent<Draggable>();
                newSlots.Add(newSlot);
                StartCoroutine(SlotParent(newSlot, modo));
                contInventario++;
            }
            else
            {
                Debug.Log($"Slot no encontrado: Slot ({contInventario})");
            }
        }
    }

    // Es una corrutina porque con la ui hay que tener paciencia
    IEnumerator SlotParent(Transform slot, int modo)
    {
        yield return null;
        Transform slotParent = null;
        if (modo == 1)
        {
            slotParent = panelInventory.Find($"Slot ({newSlots.IndexOf(slot)})");
        }else if (modo == 2)
        {
            slotParent = SlotSinHijo();
        }
        slot.SetParent(slotParent);
        slot.position = slotParent.position;
    }

    // Buscamos algún slot que no tenga hijo
    Transform SlotSinHijo()
    {
        Transform slotParent = null;
        for (int i = 0; i < 89; i++)
        {
            Transform slotParentComprobar = panelInventory.Find($"Slot ({i})");
            Debug.Log($"Slot ({i}) \nChild count: {slotParentComprobar.childCount}");
            if (slotParentComprobar.childCount == 0)
            {
                slotParent = slotParentComprobar;
                break;
            }
        }
        return slotParent;
    }

}
