using UnityEngine;

public class ControladorArmaEnemiga : MonoBehaviour
{
    [SerializeField] private float ataque = 1f;
    [SerializeField] private bool puedeHacerDaño = false;

    public void ActivarDaño()
    {
        puedeHacerDaño = true;
    }

    public void DesactivarDaño()
    {
        puedeHacerDaño = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!puedeHacerDaño) return;

        if(other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.BajarVida(ataque);
                Debug.Log("Le pegamos al Player con " + gameObject.name);
            }
        }
    }
}