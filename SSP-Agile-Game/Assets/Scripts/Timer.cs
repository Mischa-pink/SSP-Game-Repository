using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedtime;

    void Update()
    {
        // Timer that adds up by one every second
        elapsedtime += Time.deltaTime;
        timerText.text = elapsedtime.ToString();
    }
}
