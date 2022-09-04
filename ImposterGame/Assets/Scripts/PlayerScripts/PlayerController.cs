using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthSystem playerHealth;
    public float playerBuffer;
    [SerializeField] private uint playerLevel = 0;
    [SerializeField] private uint playerExperience = 0;

    private void Awake()
    {
        playerHealth.health = 100 + playerLevel * 10;
        playerBuffer = 10 + playerLevel;
    }

}
