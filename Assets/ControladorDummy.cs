using UnityEngine;

public class ControladorDummy : MonoBehaviour
{
    public float salud = 100f;
    public Animator animator;

    private RandomizeAudio randomizeAudio;

    private void Awake()
    {
        // buscamos el componente de sonido en el mismo Dummy
        randomizeAudio = GetComponent<RandomizeAudio>();
        animator.SetFloat("Salud", salud);
    }

    public void RecibirGolpe(float daño)
    {
        RecalculaSalud(daño);
        animator.SetTrigger("Golpeado");
        animator.SetFloat("Salud", salud);

        // reproducir sonido al recibir golpe
        if (randomizeAudio != null)
        {
            randomizeAudio.PlayRandomClip();
        }
    }

    public void RecalculaSalud(float daño)
    {
        salud -= daño;

        if (salud < 0f)
            salud = 0f;

        Debug.Log("Dummy recibió " + daño + " de daño. Salud = " + salud);
    }
}
