using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventarioScript : MonoBehaviour
{
    public static InventarioScript instance;

    private Transform panelInventario;
    private List<Transform> newSlots = new List<Transform>();
    private int contInventario;
    private Sprite espadaImg;
    private Sprite arcoImg;
    private Sprite pistaImg;


    public Inventario inventario = new Inventario();

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) GuardarPista();
        if (Input.GetKeyDown(KeyCode.R)) inventario.MostrarInventario();
    }

    void Start()
    {
        panelInventario = ConfiguracionJuego.instance.panelInventario;
        espadaImg = ConfiguracionJuego.instance.espadaImg;
        arcoImg = ConfiguracionJuego.instance.arcoImg;
        pistaImg = ConfiguracionJuego.instance.pistaImg;

        // Asignamos el script de poder soltar a todos los slots
        Transform slotTransform;
        for (int i = 0; i < 90; i++)
        {
            slotTransform = panelInventario.Find($"Slot ({i})");
            slotTransform.AddComponent<DropSlot>();
        }

        //LlenarInventario();

    }

    // Aquí se recogería el inventario guardado
    public void LlenarInventario(List<Item> inventarioGuardado)
    {
        // Aquí cargaremos los objetos que tendrá el jugador en el inventario al principio del todo
        /*Municion flechas = new Municion("1", "Flechas", true, 3, TipoMunicion.Piedra, 5);
        Equipo arco = new Equipo("1", "Arco", TipoArma.Arco, flechas, 5);

        Equipo espada = new Equipo("2", "Espada", TipoArma.Espada, 5);

        inventario.AgregarItem(arco);
        inventario.AgregarItem(espada);

        inventario.MostrarInventario();*/

        
        foreach (Item item in inventarioGuardado)
        {
            Debug.Log("Agregado item " + item.Nombre);
            inventario.AgregarItem(item);
        }

        LlenarPanelInventario(1);
    }

    public bool GuardarPista()
    {
        /*Pista pista = new Pista("1", "Pista", "I think human consciousnes was a tragic mistep in evolution. We became too self-aware; nature created an aspect of nature separte from itself: we are creatures that should not exist by natural law");
        inventario.AgregarItem(pista);*/
        Arma arma = new Arma("3", "Espada", "Espada de Jaime I", 0.25f, 3f, TipoArma.Espada);
        inventario.AgregarItem(arma);
        Transform slotTransform = panelInventario.Find($"Slot ({contInventario})");
        newSlots.Add(slotTransform);
        LlenarPanelInventario(2);
        return true;
    }

    public void LlenarPanelInventario(int modo)
    {
        HashSet<Item> items = inventario.GetItems();
        Transform slotTransform;
        GameObject slot;
        if (modo == 1)
        {
            StartCoroutine(CargarInventario(modo));
        }
        else if (modo == 2)
        {
            Item item = items.Last();
            slotTransform = SlotSinHijo();
            slot = slotTransform.gameObject;
            if (slot != null)
            {
                Debug.Log($"Apunto de instanciar como padre el Slot ({contInventario})");
                Transform newSlot = Instantiate(slotTransform, panelInventario);
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
                Draggable draggableItem = newSlot.AddComponent<Draggable>();
                draggableItem.SetItem(item);
                StartCoroutine(SlotParent(newSlot, modo, item));
                contInventario++;
            }
            else
            {
                Debug.Log($"Slot no encontrado: Slot ({contInventario})");
            }
        }
    }
    // Es una corrutina porque con la ui hay que tener paciencia
    IEnumerator SlotParent(Transform slot, int modo, Item item)
    {
        yield return null;
        Transform slotParent = null;
        if (modo == 1)
        {
            slotParent = panelInventario.Find($"Slot ({item.Id})");
            Debug.Log($"Intentando guardar en {item.Id}");
        }
        else if (modo == 2)
        {
            slotParent = SlotSinHijo();
        }
        slot.SetParent(slotParent);
        slot.position = slotParent.position;
    }
    // Otra corrutina porque en la ui hay que tener pacience
    IEnumerator CargarInventario(int modo)
    {
        yield return null;
        HashSet<Item> items = inventario.GetItems();
        Transform slotTransform;
        GameObject slot;
        foreach (Item item in items)
        {
            slotTransform = panelInventario.Find($"Slot ({item.Id})");
            Debug.Log($"Slot encontrado en {item.Id}");
            if (slotTransform != null)
            {
                slot = slotTransform.gameObject;
                Transform newSlot = Instantiate(slotTransform, slotTransform.parent);
                newSlot.name = $"Slot ({90 + item.Id})";
                Debug.Log("Slot encontrado " + item.Id);
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
                Draggable draggableItem = newSlot.AddComponent<Draggable>();
                draggableItem.SetItem(item);
                newSlots.Add(newSlot);
                StartCoroutine(SlotParent(newSlot, modo, item));
                //contInventario++;
            }
            else
            {
                Debug.Log($"Slot no encontrado: Slot ({item.Id})");
            }
        }
    }
    Transform SlotSinHijo()
    {
        Transform slotParent = null;
        for (int i = 0; i < 89; i++)
        {
            Transform slotParentComprobar = panelInventario.Find($"Slot ({i})");
            Debug.Log($"Slot ({i}) \nChild count: {slotParentComprobar.childCount}");
            if (slotParentComprobar.childCount == 0)
            {
                slotParent = slotParentComprobar;
                break;
            }
        }
        return slotParent;
    }
    public List<Item> RecibirInventario()
    {
        List<Item> list = new List<Item>(inventario.GetItems());
        foreach (Item item in list)
        {
            Debug.Log("Intentando guardar: " + item.Id);
        }
        return list;
    }
}
