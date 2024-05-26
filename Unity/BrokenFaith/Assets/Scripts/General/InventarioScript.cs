using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using static UnityEditor.Progress;
#endif

public class InventarioScript : MonoBehaviour
{
    public static InventarioScript instance;

    private Transform panelInventario;
    private Transform panelInventarioExterno;
    private List<Transform> newSlots = new List<Transform>();
    private int contInventario;
    private Sprite espadaImg;
    private Sprite arcoImg;
    private Sprite pistaImg;
    private GameObject espadaFPS;
    private GameObject arcoFPS;
    private Dictionary<string, Item> itemsDiccionario = new Dictionary<string, Item>();
    private int slotSeleccionado;


    public Inventario inventario = new Inventario();


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        panelInventario = ConfiguracionJuego.instance.TransformPanelInventario;
        panelInventarioExterno = ConfiguracionJuego.instance.TransformPanelInventarioExterno;
        espadaImg = ConfiguracionJuego.instance.EspadaImg;
        arcoImg = ConfiguracionJuego.instance.ArcoImg;
        pistaImg = ConfiguracionJuego.instance.PistaImg;
        espadaFPS = ConfiguracionJuego.instance.EspadaFPS;
        arcoFPS = ConfiguracionJuego.instance.ArcoFPS;
        slotSeleccionado = 0;

        DiccionarioInventario();

        string ruta = Path.Combine(Application.dataPath, "Guardado.xml");
        if (!File.Exists(ruta))
        {
            GuardarEspadaExterno();
            GuardarArcoExterno();
            LlenarPanelInventario(1);
        }

        // Asignamos el script de poder soltar a todos los slots
        Transform slotTransform;
        for (int i = 0; i < 90; i++)
        {
            slotTransform = panelInventario.Find($"Slot ({i})");
            slotTransform.AddComponent<DropSlot>();
        }
        for (int i = 90; i < 93; i++)
        {
            slotTransform = panelInventarioExterno.Find($"Slot ({i})");
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
        int idArma = IdLibre();
        if (idArma >= 0)
        {
            Arma arma = new Arma(idArma.ToString(), "Espada", "Espada de Jaime I", 0.25f, 3f, TipoArma.Espada);
            inventario.AgregarItem(arma);
            Transform slotTransform = panelInventario.Find($"Slot ({contInventario})");
            newSlots.Add(slotTransform);
            LlenarPanelInventario(2);
            return true;
        }
        return false;
    }

    public bool GuardarEspada()
    {
        int idArma = IdLibre();
        if (idArma >= 0)
        {
            Arma arma = new Arma(idArma.ToString(), "Espada", "Espada de Jaime I", 0.25f, 3f, TipoArma.Espada);
            inventario.AgregarItem(arma);
            Transform slotTransform = panelInventario.Find($"Slot ({contInventario})");
            newSlots.Add(slotTransform);
            LlenarPanelInventario(2);
            return true;
        }
        return false;
    }

    public bool GuardarArco()
    {
        int idArma = IdLibre();
        if (idArma >= 0)
        {
            Arma arma = new Arma(idArma.ToString(), "Arco", "Arco de Noe", 0.25f, 3f, TipoArma.Arco);
            inventario.AgregarItem(arma);
            Transform slotTransform = panelInventario.Find($"Slot ({contInventario})");
            newSlots.Add(slotTransform);
            LlenarPanelInventario(2);
            return true;
        }
        return false;
    }

    public bool GuardarEspadaExterno()
    {
        Arma arma = new Arma("90", "Espada", "Espada de Jaime I", 0.25f, 3f, TipoArma.Espada);
        inventario.AgregarItem(arma);
        Transform slotTransform = panelInventarioExterno.Find($"Slot (90)");
        newSlots.Add(slotTransform);
        return true;
    }

    public bool GuardarArcoExterno()
    {
        Arma arma = new Arma("91", "Arco", "Arco de Noe", 0.25f, 3f, TipoArma.Arco);
        inventario.AgregarItem(arma);
        Transform slotTransform = panelInventarioExterno.Find($"Slot (91)");
        newSlots.Add(slotTransform);
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
            if (item.Id == "90" || item.Id == "91" || item.Id == "92")
                slotTransform = SlotSinHijoExterno();
            else
                slotTransform = SlotSinHijo();
            slot = slotTransform.gameObject;
            if (slot != null)
            {
                Debug.Log($"Apunto de instanciar como padre el Slot ({item.Id})");
                Transform newSlot;
                if (item.Id == "90" || item.Id == "91" || item.Id == "92")
                    newSlot = Instantiate(slotTransform, panelInventarioExterno);
                else
                    newSlot = Instantiate(slotTransform, panelInventario);
                newSlot.name = $"Slot ({92 + contInventario})";
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
            if (int.Parse(item.Id) < 90)
                slotParent = panelInventario.Find($"Slot ({item.Id})");
            else if (int.Parse(item.Id) >= 90 && int.Parse(item.Id) < 93)
                slotParent = panelInventarioExterno.Find($"Slot ({item.Id})");
            Debug.Log($"Intentando guardar en {item.Id}");
        }
        else if (modo == 2)
        {
            slotParent = SlotSinHijo();
        }
        slot.SetParent(slotParent);
        slot.position = slotParent.position;
    }
    // Otra corrutina porque en la ui hay que tener paciencia
    IEnumerator CargarInventario(int modo)
    {
        yield return null;
        HashSet<Item> items = inventario.GetItems();
        Transform slotTransform = null;
        GameObject slot;
        foreach (Item item in items)
        {
            if (int.Parse(item.Id) < 90)
                slotTransform = panelInventario.Find($"Slot ({item.Id})");
            else if (int.Parse(item.Id) >= 90 && int.Parse(item.Id) < 93)
                slotTransform = panelInventarioExterno.Find($"Slot ({item.Id})");
            else
                Debug.LogError($"Slot {item.Nombre} tiene un Id incorrecto ({item.Id})");
            Debug.Log($"Slot encontrado en {item.Id}");
            if (slotTransform != null)
            {
                slot = slotTransform.gameObject;
                Transform newSlot = Instantiate(slotTransform, slotTransform.parent);
                newSlot.name = $"Slot ({92 + item.Id})";
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
        for (int i = 0; i < 90; i++)
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
    Transform SlotSinHijoExterno()
    {
        Transform slotParent = null;
        for (int i = 90; i < 93; i++)
        {
            Transform slotParentComprobar = panelInventarioExterno.Find($"Slot ({i})");
            Debug.Log($"Slot ({i}) \nChild count: {slotParentComprobar.childCount}");
            if (slotParentComprobar.childCount == 0)
            {
                slotParent = slotParentComprobar;
                break;
            }
        }
        return slotParent;
    }
    int IdLibre()
    {
        for (int i = 0; i < 90; i++)
        {
            Transform slotParentComprobar = panelInventario.Find($"Slot ({i})");
            Debug.Log($"Slot ({i}) \nChild count: {slotParentComprobar.childCount}");
            if (slotParentComprobar.childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }

    public void DiccionarioInventario()
    {
        List<Item> inv = new List<Item>(inventario.GetItems());
        itemsDiccionario = inv.ToDictionary(item => item.Id);
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

    public void EquiparObjeto(int numSlot)
    {
        DiccionarioInventario();
        string numItemStr = "9" + numSlot;
        if (itemsDiccionario.TryGetValue(numItemStr, out Item itemBuscado))
        {
            Debug.Log("ItemEncontrado: " + itemBuscado.Nombre);
            if (itemBuscado.Nombre == "Espada")
            {
                espadaFPS.SetActive(true);
                arcoFPS.SetActive(false);
            }
            else if (itemBuscado.Nombre == "Arco")
            {
                arcoFPS.SetActive(true);
                espadaFPS.SetActive(false);
            }
            else
            {
                espadaFPS.SetActive(false);
                arcoFPS.SetActive(false);
            }
        }
        else
        {
            espadaFPS.SetActive(false);
            arcoFPS.SetActive(false);
        }
        SetSlotSeleccionado(numSlot);
    }

    public void SetSlotSeleccionado(int numSlot)
    {
        slotSeleccionado = numSlot;
    }
    public int GetSlotSeleccionado()
    {
        return slotSeleccionado;
    }

}
