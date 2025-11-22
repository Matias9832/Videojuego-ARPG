using UnityEngine;

public class ControladorDummy : MonoBehaviour
{
    public float salud = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RecalculaSalud(float danio)
    {
        if (salud >= 0f)
            salud -= danio;
        else
            salud = 0f;
    }
}
