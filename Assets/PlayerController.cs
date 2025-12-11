using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Estadisticas")]
    public float Vida = 100f;
    public bool estaMuerto = false;
    public Animator barraVida;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Animator gameOverAnimator;


    [Header("Coleccionables")]
    public int contadorCollecionables;
    [SerializeField] private TextMeshProUGUI CollectText;

    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    private Vector3 defaultSpawn = Vector3.zero;

    [Header("Golpes Recividos")]
    private RandomizeAudio randomizeAudio;
    public AudioClip sonidoMuerte;
    private AudioSource fuenteMuerte;

    CharacterController cc;
    Rigidbody rb;
    NavMeshAgent agent;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Respawn();
        if (CollectText) CollectText.text = contadorCollecionables.ToString();
        gameOverCanvas.gameObject.SetActive(false);
        randomizeAudio = GetComponent<RandomizeAudio>();
        fuenteMuerte = GetComponent<AudioSource>();
    }

    public void Update()
    {
        barraVida.SetFloat("Vida", Vida / 100);

        if (Vida <= 0f && !estaMuerto)
        {
            estaMuerto = true;
            if (gameOverCanvas)
            {
                gameOverCanvas.gameObject.SetActive(true);
                if (fuenteMuerte != null && sonidoMuerte != null)
                    fuenteMuerte.PlayOneShot(sonidoMuerte);
                if (gameOverAnimator)
                {
                    gameOverAnimator.SetTrigger("GameOver");
                }
            }
        }
    }

    public void AddCoins(int amount = 1)
    {
        contadorCollecionables += amount;
        if (CollectText) CollectText.text = contadorCollecionables.ToString();
    }

    public void Respawn()
    {
        if (estaMuerto)
        {
            Vida = 100f;
            estaMuerto = false;
            gameOverCanvas.gameObject.SetActive(false);
        }

        Vector3 pos = spawnPoint ? spawnPoint.position : defaultSpawn;

        if (agent)
        {
            agent.Warp(pos);
            agent.ResetPath();
            return;
        }

        if (cc)
        {
            StartCoroutine(CCRespawn(pos));
            return;
        }

        if (rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = pos;
            rb.Sleep();
            return;
        }

        transform.position = pos;
    }

    public void BajarVida(float cantidad)
    {
        Vida -= cantidad;
        if (randomizeAudio != null)
        {
            randomizeAudio.PlayRandomClip();
        }
    }

    IEnumerator CCRespawn(Vector3 pos)
    {
        cc.enabled = false;
        transform.position = pos;
        yield return null;
        cc.enabled = true;
    }

    public void SetSpawn(Transform newSpawn) => spawnPoint = newSpawn;
}
