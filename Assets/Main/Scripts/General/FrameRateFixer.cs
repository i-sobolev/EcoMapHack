using UnityEngine;

public static class FrameRateFixer
{
    [RuntimeInitializeOnLoadMethod]
    public static void SetTargetFrameRate()
    {
        Application.targetFrameRate = 90;
    }
}
