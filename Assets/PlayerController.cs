using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    [Header("Coleccionables")]
    public int contadorCollecionables;
    [SerializeField] private TextMeshProUGUI CollectText;

    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    private Vector3 defaultSpawn = Vector3.zero;

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
    }

    public void AddCoins(int amount = 1)
    {
        contadorCollecionables += amount;
        if (CollectText) CollectText.text = contadorCollecionables.ToString();
    }

    public void Respawn()
    {
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

    IEnumerator CCRespawn(Vector3 pos)
    {
        cc.enabled = false;
        transform.position = pos;
        yield return null;
        cc.enabled = true;
    }

    public void SetSpawn(Transform newSpawn) => spawnPoint = newSpawn;
}
