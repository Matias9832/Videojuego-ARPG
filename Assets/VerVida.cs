using UnityEngine;

public class VerVida : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        this.transform.LookAt(target);
    }
}
