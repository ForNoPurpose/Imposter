using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    public HealthSystem playerHealth;
    public uint playerBufferMaxSize;
    public uint playerBufferCurrentSize = 0;
    [SerializeField] private uint playerLevel = 0;
    //[SerializeField] private uint playerExperience = 0;

    public List<IWeapon> playerBuffer = new();

    private void Awake()
    {
        playerHealth.health = 100 + playerLevel * 10;
        playerBufferMaxSize = 10 + playerLevel;
    }
    
}
