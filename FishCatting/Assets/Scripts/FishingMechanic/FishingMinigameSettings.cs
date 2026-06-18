using System;
using UnityEngine;
using static Fish;

[Serializable]
public class FishingMinigameSettings
{
    public Rarity rarity;
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
