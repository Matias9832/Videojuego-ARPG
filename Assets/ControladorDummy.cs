using UnityEngine;

public class ControladorDummy : MonoBehaviour
{
    public float salud = 100f;

    public void RecibirGolpe(float daño)
    {
        RecalculaSalud(daño);
    }

    public void RecalculaSalud(float daño)
    {
        salud -= daño;

        if (salud < 0f)
            salud = 0f;

        Debug.Log("Dummy recibió " + daño + " de daño. Salud = " + salud);
    }
}
