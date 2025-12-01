using UnityEngine;

public class ARPGArmaController : MonoBehaviour
{
    [SerializeField] private float ataque = 1f;
    [SerializeField] private bool puedeHacerDaño = false;

    // Estos los llamará el personaje (con Animation Events)
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
        // si no estamos en ventana de ataque, no hacer nada
        if (!puedeHacerDaño) return;

        if (other.CompareTag("Golpeable"))
        {
            var objetivo = other.GetComponent<ControladorDummy>();

            if (objetivo != null)
            {
                objetivo.RecibirGolpe(ataque);
                Debug.Log("Le pegamos al Dummy con " + gameObject.name);
            }
        }
    }
}