using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RandomizeWalk : MonoBehaviour
    {
        [Header("Random Walk Parameters")]
        public float m_Range = 25.0f;
        NavMeshAgent m_Agent;
        public Velocidad velocidad;
        public Animator animator;
        public List<Transform> posicionesObjetivo;
        public bool randomFuncionando = true;

        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
            velocidad.actual = 0f;
            velocidad.actualMax = velocidad.caminata;
        }

        void Update()
        {

            m_Agent.speed = velocidad.actual;
            animator.SetFloat("Velocidad", velocidad.actual);

            if (m_Agent.pathPending || !m_Agent.isOnNavMesh || m_Agent.remainingDistance > 0.1f)
            {
                if (m_Agent.remainingDistance > 0.1f)
                {
                    if (velocidad.actual < velocidad.actualMax)
                        velocidad.actual += Time.deltaTime;
                    else if (velocidad.actual > velocidad.actualMax)
                        velocidad.actual -= Time.deltaTime * 5f;
                }

                return;
            }

            if (randomFuncionando)
            {
                Vector2 random2D = Random.insideUnitCircle * m_Range;
                Vector3 randomPos = new Vector3(random2D.x + transform.position.x, transform.position.y, random2D.y + transform.position.z);

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPos, out hit, 5f, NavMesh.AllAreas))
                {
                    m_Agent.SetDestination(hit.position);
                }
            }
        }

        public void CambiaObjetivo(Vector3 nuevoObjetivo)
        {
            m_Agent.destination = nuevoObjetivo;
        }

        [System.Serializable]
        public struct Velocidad
        {
            public float actual;
            public float actualMax;
            public float caminata;
            public float corriendo;
        }

    }
}