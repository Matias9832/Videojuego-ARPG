using TMPro;
using UnityEngine;

public class JuegoController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private AudioSource musicSource;

    public bool gameOver = false;
    public float timer = 0f;

    private void Start()
    {
        if (musicSource)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    void Update()
    {
        if (gameOver) return;

        timer += Time.deltaTime;

        if (!timerText) return;
        int min = (int)(timer / 60f);
        int sec = (int)(timer % 60f);
        timerText.text = $"{min:00}:{sec:00}";
    }
}
