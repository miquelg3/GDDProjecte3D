using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JefeComportamiento:MonoBehaviour
{
    private NavMeshAgent Agent;
    private Transform Jugador;
    private float Stun;
    private float CooldownCargar;
    private float TiempoCooldown;
    private bool stuneado;
    public GameObject cuchilloPrefab;
    public Transform puntoLanzamiento;
    public float alcance = 100f;
    private float Cooldown;
    private float TiempoCuchillo;
    private int CuchilloGastado;
    private float intervalCuchillo;
    private float lanzarCuchilloTiempo;
    private GameObject cuchillo;
    private bool LanzandoCuchillos;
    private bool estaCargando;
    public delegate void EventoDanyoJefe(bool Cargando);
    public static event EventoDanyoJefe Danyo;
    // Start is called before the first frame update
    void Start()
    {
        estaCargando = false;
        LanzandoCuchillos = false;
        intervalCuchillo = 2f;
        CuchilloGastado = 0;
        Cooldown = 0f;
        TiempoCuchillo = 10f;
        stuneado = false;
        TiempoCooldown = 0f;
        CooldownCargar = 10f;
        Stun = 3f;
        Jugador = GameObject.FindGameObjectWithTag("Player").transform;
        Agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Agent.acceleration);
        lanzarCuchilloTiempo += Time.deltaTime;
        if(CuchilloGastado == 3)
        {
            Cooldown += Time.deltaTime;
        }
        if(Cooldown >= TiempoCuchillo)
        {
            CuchilloGastado = 0;
            Cooldown = 0f;
        }
        if (Vector3.Distance(transform.position, Jugador.position) > 0.5f)
        {
            Agent.SetDestination(Jugador.transform.position);
        }
        ControlarCargado();
        if (Vector3.Distance(transform.position, Jugador.position) > 2f && CuchilloGastado < 3 && lanzarCuchilloTiempo >= intervalCuchillo)
        {
            LanzarCuchillo();
            lanzarCuchilloTiempo = 0;
        }
    }

    private void Cargar()
    {
        if (!stuneado && !LanzandoCuchillos)
        {
            estaCargando = true;
            Danyo?.Invoke(estaCargando);
            Agent.speed = 12.0f;
            Agent.acceleration = 6f;
            Agent.SetDestination(transform.forward);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pilar"))
        {
            StartCoroutine(StunCoroutine());
            estaCargando = false;
            Danyo?.Invoke(estaCargando);
        }
        else
        {
            Agent.speed = 3f;
            Agent.acceleration = 6f;
            Agent.SetDestination(Jugador.transform.position);
            estaCargando = false;
            Danyo?.Invoke(estaCargando);
        }
    }
    private void ControlarCargado()
    {
        TiempoCooldown += Time.deltaTime;
        if (TiempoCooldown >= CooldownCargar)
        {
            Cargar();
            TiempoCooldown = 0f;
        }
    }
    private IEnumerator StunCoroutine()
    {
        stuneado = true;
        Agent.velocity = Vector3.zero;
        Agent.speed = 0f;

        yield return new WaitForSeconds(Stun);
        Agent.speed = 3f;
        stuneado = false;
        Agent.SetDestination(Jugador.transform.position);
    }
    private void LanzarCuchillo()
    {
        Vector3 direccion = Jugador.position - transform.position;
        RaycastHit hit;
        Ray rayo = new Ray(transform.position,(Jugador.position - transform.position).normalized);
        if (Physics.Raycast(rayo, out hit, alcance))
        {
            if (hit.collider.CompareTag("Player") && !stuneado && !estaCargando)
            {
                LanzandoCuchillos = true;

                StartCoroutine(tirarCuchillo());
                CuchilloGastado++;
                Debug.Log("Entro");
               
                Quaternion rotacionCuchillo = Quaternion.LookRotation(direccion);

                cuchillo = Instantiate(cuchilloPrefab, transform.position+transform.forward, rotacionCuchillo);
                Physics.IgnoreCollision(GetComponent<Collider>(), cuchilloPrefab.GetComponent<Collider>());

                Rigidbody rb = cuchillo.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(direccion.normalized * 100f, ForceMode.Impulse);
                }
                Destroy(cuchillo, 10f);
            }
        }

    }
    private IEnumerator tirarCuchillo()
    {
        Agent.velocity = Vector3.zero;
        Agent.speed = 0f;

        yield return new WaitForSeconds(3f);
        LanzandoCuchillos = false;
        Agent.speed = 3f;
    }
}
