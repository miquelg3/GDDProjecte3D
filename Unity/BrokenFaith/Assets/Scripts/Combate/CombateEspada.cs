using UnityEngine;

public class CombateEspada : MonoBehaviour
{

    #region Variables

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioSwing;
    private Animator animator;
    private bool puedeAtacar;
    private bool atacando = false;

    #endregion
    void Start()
    {
        puedeAtacar = true;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && puedeAtacar) Atacar();

        Defensa();
    }

    public void Atacar()
    {
        atacando = true;
        audioSource.clip = audioSwing;
        audioSource.Play();
        puedeAtacar = false;
        animator.SetTrigger("Atacar");
    }

    public bool GetAtacando()
    {
        return atacando;
    }

    public void Defensa()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            atacando = false;
            animator.SetBool("Defensa", true);
        } else
        {
            animator.SetBool("Defensa", false);
        }        
    }

    public void ResetAtaque()
    {
        atacando = false;
        puedeAtacar = true;
    }
}
