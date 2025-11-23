using UnityEngine;

public class DamageStateBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var ataque = animator.GetComponent<ARPGPersonajeAtaque>();
        if (ataque != null)
            ataque.ActivarDaño();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var ataque = animator.GetComponent<ARPGPersonajeAtaque>();
        if (ataque != null)
            ataque.DesactivarDaño();
    }
}
