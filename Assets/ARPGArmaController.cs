using UnityEngine;

public class ARPGArmaController : MonoBehaviour
{
    [SerializeField] private float ataque = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Golpeable"))
        {
            var objetivo = other.GetComponent<ControladorDummy>();

            if (objetivo != null)
            {
                objetivo.RecibirGolpe(ataque);
                Debug.Log("Le pegamos al Dummy con la llave");
            }
        }
    }
}
