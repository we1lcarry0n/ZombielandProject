using UnityEngine;
using UnityEngine.UI;

public class AverageFPS : MonoBehaviour
{
    public float interval = 5.0f; // Интервал времени для замера среднего FPS
    private float timeElapsed = 0.0f;
    private int frames = 0;
    private float totalFPS = 0.0f;
    private float averageFPS = 0.0f;

    public Text fpsText; // Текстовый элемент UI для отображения FPS

    void Update()
    {
        frames++;
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= interval)
        {
            totalFPS = frames / timeElapsed;
            averageFPS = totalFPS / frames;

            if (fpsText != null)
            {
                fpsText.text = "Average FPS: " + averageFPS.ToString("F2");
            }

            // Сброс счетчиков
            frames = 0;
            timeElapsed = 0.0f;
        }
    }
}