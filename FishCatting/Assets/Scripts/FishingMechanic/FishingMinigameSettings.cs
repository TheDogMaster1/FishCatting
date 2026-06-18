using System;
using UnityEngine;

[Serializable]
public class FishingMinigameSettings
{
    [Header("Fish slider settings")]
    public float fishSpeed = 1;
    public int minTimeChange = 10;
    public int maxTimeChange = 30;
    [Range(1, 100)]
    public int fishWinSize;

    [Header("Timer slider settings")]
    public float upTimerSpeed = 0.5f;
    public float downTimerSpeed = 0.2f;
    public float beginValue = 0.1f;
}
