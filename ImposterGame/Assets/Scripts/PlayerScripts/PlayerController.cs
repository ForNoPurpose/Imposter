using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;
using System.Linq;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController instance;
    
    public HealthSystem playerHealth;
    public uint playerBufferMaxSize;
    public uint playerBufferCurrentSize = 0;
    [SerializeField] private uint playerLevel = 0;
    //[SerializeField] private uint playerExperience = 0;

    [SerializeField] private List<ItemDataSO> _playerBuffer = new();

    private float _damageTimer = 1f;
    private bool _canTakeDamage = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        playerHealth.health = 100 + playerLevel * 10;
        playerBufferMaxSize = 10 + playerLevel;

        SceneObject.OnPickup += AddToBuffer;
        PlayerAttack.OnThrow += RemoveFromBuffer;
    }

    private void OnDisable()
    {
        SceneObject.OnPickup -= AddToBuffer;
        PlayerAttack.OnThrow -= RemoveFromBuffer;
    }

    private void Update()
    {
        DamageTimer();
    }

    private void AddToBuffer(ItemDataSO itemData)
    {
        if (playerBufferCurrentSize + itemData.BufferRequired <= playerBufferMaxSize)
        {
            _playerBuffer.Add(itemData);
            playerBufferCurrentSize += itemData.BufferRequired;
        }
    }

    private void RemoveFromBuffer(ItemDataSO itemData)
    {
        if (!_playerBuffer.Contains(itemData)) return;
        _playerBuffer.Remove(itemData);
        playerBufferCurrentSize -= itemData.BufferRequired;
    }

    public ItemDataSO CurrentProjectile()
    {
        return _playerBuffer.FirstOrDefault(x => x.Type == ItemType.Projectile);
    }

    public void Damage(float amount)
    {
        if (!_canTakeDamage) return;
        _canTakeDamage = false;
        playerHealth.health -= amount / playerHealth.resistance;
        if (playerHealth.health <= 0)
        {
            GameController.GameInstance.SetState(GameController.GameState.GameOver);
        }
    }

    private void DamageTimer()
    {
        if (!_canTakeDamage)
        {
            _damageTimer -= Time.deltaTime;
        }
        if(_damageTimer < 0)
        {
            _canTakeDamage = true;
            _damageTimer = 1f;
        }
    }
}
