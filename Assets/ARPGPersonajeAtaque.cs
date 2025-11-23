using UnityEngine;

public class ARPGPersonajeAtaque : MonoBehaviour
{
    [SerializeField] private ARPGArmaController arma;   // arrastras aquí la llave

    public void ActivarDaño()
    {
        if (arma != null)
            arma.ActivarDaño();
    }

    public void DesactivarDaño()
    {
        if (arma != null)
            arma.DesactivarDaño();
    }
}
