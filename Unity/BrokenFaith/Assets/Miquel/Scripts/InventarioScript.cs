using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        panelInventario = GameObject.Find("Canvas").transform.Find("Inventario").transform.Find("PanelInventario");
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
        Pista pista = new Pista("1", "Pista", "I think human consciousnes was a tragic mistep in evolution. We became too self-aware; nature created an aspect of nature separte from itself: we are creatures that should not exist by natural law");
        inventario.AgregarItem(pista);
        Transform slotTransform = panelInventario.Find($"Slot ({contInventario})");
        newSlots.Add(slotTransform);
        LlenarPanelInventario(2);
        return true;
    }

    public void LlenarPanelInventario(int modo)
    {
        HashSet<Item> items = inventario.GetItems();
        Transform slotTranform;
        GameObject slot;
        if (modo == 1)
        {
            foreach (Item item in items)
            {
                slotTranform = panelInventario.Find($"Slot ({contInventario})");
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
        }
        else if (modo == 2)
        {
            Item item = items.Last();
            slotTranform = SlotSinHijo();
            slot = slotTranform.gameObject;
            if (slot != null)
            {
                Debug.Log($"Apunto de instanciar como padre el Slot ({contInventario})");
                Transform newSlot = Instantiate(slotTranform, panelInventario);
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
            slotParent = panelInventario.Find($"Slot ({newSlots.IndexOf(slot)})");
        }
        else if (modo == 2)
        {
            slotParent = SlotSinHijo();
        }
        slot.SetParent(slotParent);
        slot.position = slotParent.position;
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
    public List<Item> EnviarInventario()
    {
        List<Item> list = new List<Item>(inventario.GetItems());
        return list;
    }
}
