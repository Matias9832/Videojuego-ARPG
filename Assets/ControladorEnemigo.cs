using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class ControladorEnemigo : MonoBehaviour
{
    [Header("Parametros de Vida")]
    [Range(0f, 100f)]
    public float Vida = 100f;
    public Animator animator;
    public Animator barraVida;
    public Comportamiento comportamiento;
    public UnityEvent Muerte;


    [Header("Parametros de Deteccion")]
    public float RangoVision = 10f;
    public LayerMask LayerVision;
    public Transform OrigenVision;
    public Transform Player;
    public Unity.AI.Navigation.Samples.RandomizeWalk randomWalk;

    void Start()
    {
        StartCoroutine(_EsperaPorMuerte());
        comportamiento = Comportamiento.agresivo;

    }

    void Update()
    {
        barraVida.SetFloat("vida", Vida / 100);

        Vector3[] direcciones = new Vector3[3];


        direcciones[0] = transform.forward;
        direcciones[1] = (transform.forward + transform.right).normalized;
        direcciones[2] = (transform.forward - transform.right).normalized;

        for (int i = 0; i < 3; i++)
        {
            Debug.DrawRay(transform.position, direcciones[i] * RangoVision, Color.green);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direcciones[i], out hit, RangoVision, LayerVision))
            {
                Debug.Log("PLAYER DETECTADO!: " + hit.collider.gameObject.name);
                Player = hit.collider.gameObject.transform;
            }
        }

        if (Player != null)
        {
            randomWalk.randomFuncionando = false;
            if (Vector3.Distance(this.transform.position, Player.position) > 2f)
            {
                animator.SetBool("ataque", false);
                randomWalk.velocidad.actualMax = randomWalk.velocidad.corriendo;
                randomWalk.CambiaObjetivo(Player.position);
            }
            else
            {
                randomWalk.velocidad.actualMax = 0f;
                animator.SetBool("ataque", true);
            }
        }
    }

    public void TomaDanio(float danio)
    {
        Vida -= danio;
    }

    public void AnimarMuerte()
    {
        animator.SetBool("muerte", true);
    }

    public float GetDistanciaObjetivo(Vector3 objetivo)
    {
        return (objetivo - this.transform.position).magnitude;
    }


    public void RandomizeIndiceGolpe()
    {
        animator.SetInteger("golpeIndice", Random.Range(1, 6));
        animator.SetTrigger("golpe");
    }

    public void PlayAudioMuerte(AudioClip clip)
    {
        this.GetComponent<AudioSource>().clip = clip;
        this.GetComponent<AudioSource>().Play();
    }


    IEnumerator _EsperaPorMuerte()
    {
        yield return new WaitWhile(() => Vida > 0f);
        Muerte.Invoke();
    }

    public enum Comportamiento { pasivo, neutral, agresivo }
}
