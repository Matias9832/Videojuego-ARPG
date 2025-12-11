using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class ControladorEnemigo : MonoBehaviour
{
    [Header("Parametros de Vida")]
    [Range(0f, 100f)]
    public float Vida = 100f;
    public bool estaMuerto = false;
    private bool estaGolpeado = false;
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

    [Header("Parametros de Ataque")]
    public float DanioPorAtaque = 10f;
    public float TiempoBloqueo = 3f;
    public bool bloqueo = false;
    private int Secuencia = 1;

    [Header("Parametros de Armas")]
    public ControladorArmaEnemiga armaEnemiga;


    [Header("Audio")]
    private RandomizeAudio randomizeAudio;
    public AudioClip sonidoGolpe;
    private AudioSource fuenteGolpe;


    void Start()
    {
        StartCoroutine(_EsperaPorMuerte());
        randomizeAudio = GetComponent<RandomizeAudio>();
        fuenteGolpe = GetComponent<AudioSource>();
        comportamiento = Comportamiento.agresivo;
    }

    void Update()
    {
        if (!estaMuerto)
        {
            if (TiempoBloqueo > 0f && bloqueo == true)
            {
                TiempoBloqueo -= Time.deltaTime;
            }
            else if (TiempoBloqueo <= 0f && bloqueo == true)
            {
                bloqueo = false;
                animator.SetBool("Bloquea", false);
                TiempoBloqueo = 3f;
            }

            if (bloqueo)
            {
                randomWalk.velocidad.actualMax = 0f;
                animator.SetBool("Ataca", false);
                return;
            }

            barraVida.SetFloat("Vida", Vida / 100);

            Vector3[] direcciones = new Vector3[7];


            direcciones[0] = OrigenVision.forward;
            direcciones[1] = (OrigenVision.forward + OrigenVision.right).normalized;
            direcciones[2] = (OrigenVision.forward - OrigenVision.right).normalized;
            direcciones[3] = (OrigenVision.forward + OrigenVision.right * 0.5f).normalized;
            direcciones[4] = (OrigenVision.forward - OrigenVision.right * 0.5f).normalized;
            direcciones[5] = (OrigenVision.forward * 0.5f + OrigenVision.right).normalized;
            direcciones[6] = (OrigenVision.forward * 0.5f - OrigenVision.right).normalized;


            for (int i = 0; i < 7; i++)
            {
                Debug.DrawRay(OrigenVision.position, direcciones[i] * RangoVision, Color.green);

                RaycastHit hit;
                if (Physics.Raycast(OrigenVision.position, direcciones[i], out hit, RangoVision, LayerVision))
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
                    animator.SetBool("Ataca", false);
                    randomWalk.velocidad.actualMax = randomWalk.velocidad.corriendo;
                    randomWalk.CambiaObjetivo(Player.position);
                }
                else
                {
                    if (!estaGolpeado) // SOLO ATACA SI NO ESTÁ GOLPEADO
                    {
                        randomWalk.velocidad.actualMax = 0f;
                        animator.SetBool("Ataca", true);
                    }
                    else
                    {
                        animator.SetBool("Ataca", false);
                    }
                }
            }
        }
        else
        {
            randomWalk.velocidad.actualMax = 0f;
        }
    }

    public void TomaDanio(float danio)
    {
        if (bloqueo == true)
        {
            TiempoBloqueo = 3f;
            SonidoGolpeBloqueo();
            return;
        }

        Vida -= danio;
        estaGolpeado = true;
        RandomizeIndiceGolpe();
        if (randomizeAudio != null)
        {
            randomizeAudio.PlayRandomClip();
        }
    }

    public void AnimarMuerte()
    {
        animator.applyRootMotion = true;
        animator.SetBool("Muerto", true);
        animator.SetTrigger("Morir");
        estaMuerto = true;
    }

    public float GetDistanciaObjetivo(Vector3 objetivo)
    {
        return (objetivo - this.transform.position).magnitude;
    }


    public void RandomizeIndiceGolpe()
    {
        if (bloqueo) return;
        animator.SetInteger("RecibeIndice", Secuencia);
        animator.SetTrigger("RecibeAtaque");

        if (Secuencia == 4)
        {
            Secuencia = 1;
            MantenerBloqueo();
        }
        else
        {
            Secuencia++;
        }
    }

    public void MantenerBloqueo()
    {
        bloqueo = true;
        TiempoBloqueo = 3f;
        animator.SetBool("Bloquea", true);
    }

    public void FinGolpe()
    {
        estaGolpeado = false;
    }

    public void PlayAudioMuerte(AudioClip clip)
    {
        this.GetComponent<AudioSource>().clip = clip;
        this.GetComponent<AudioSource>().Play();
    }

    public void SonidoGolpeBloqueo()
    {
        if (fuenteGolpe != null && sonidoGolpe != null)
            fuenteGolpe.PlayOneShot(sonidoGolpe);
    }

    IEnumerator _EsperaPorMuerte()
    {
        yield return new WaitWhile(() => Vida > 0f);
        Muerte.Invoke();
    }

    public enum Comportamiento { pasivo, neutral, agresivo }
}
