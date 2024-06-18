using UnityEngine;
using UnityEngine.UI;

public class MyConsole : MonoBehaviour
{
    public static MyConsole Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] private Text textConsole;

    private string currentContent = "";

    public void DebugLog(object message)
    {
        float timeSinceStart = Time.time;
        int minutes = (int)(timeSinceStart / 60);
        int seconds = (int)(timeSinceStart % 60);
        string timeString = $"{minutes:D2}:{seconds:D2}";

        currentContent += $"[{timeString}] {message}\n";
        textConsole.text = currentContent;
    }
}
