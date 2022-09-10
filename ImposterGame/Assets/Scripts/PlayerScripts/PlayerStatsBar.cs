using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsBar : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _bufferBar;

    private void Start()
    {
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _healthBar.maxValue = _controller.playerHealth.maxHealth;
        _bufferBar.maxValue = _controller.playerBufferMaxSize;
    }

    private void Update()
    {
        _healthBar.value = _controller.playerHealth.health;
        _bufferBar.value = _controller.playerBufferCurrentSize;
    }
}
