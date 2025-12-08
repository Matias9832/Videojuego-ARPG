using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ControladorNPC : MonoBehaviour
{
    [Range(0f,100f)]
    public float salud = 100f;
    public float saludMax = 100f;
    public Animator animator;
    public Animator barraVida;
    public Comportamiento comportamiento;

    public UnityEvent alMorir;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        StartCoroutine(_EsperaPorMuerte());
    }

    // Update is called once per frame
    protected void Update()
    {
        barraVida.SetFloat("vida", salud/saludMax);
    }

    public void TomaDanio(float danio)
    {
        salud -= danio;
    }

    public void AnimarMuerte()
    {
        animator.SetBool("muerte", true);
    }

    /////////////////////////////////////////////

    #region CORRUTINAS

    IEnumerator _EsperaPorMuerte()
    {
        yield return new WaitWhile(()=> salud > 0f);
        alMorir.Invoke();
    }

    #endregion

    /////////////////////////////////////////////
     
    #region DEFINICION DATOS

    public enum Comportamiento {pasivo, neutral, agresivo}

    #endregion
}
