using UnityEngine;

public class Vibration : MonoBehaviour
{
    public static void Vibrate(long milliseconds)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator"))
                    {
                        if (vibrator.Call<bool>("hasVibrator"))
                        {
                            vibrator.Call("vibrate", milliseconds);
                        }
                    }
                }
            }
        }
        else
        {
            Handheld.Vibrate();
        }
    }
}
