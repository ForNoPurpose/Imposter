using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "ItemData")]
public class ItemDataSO : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _displayName;
    [TextArea(4,4)]
    [SerializeField] private string _description;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private bool _canCopy;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _gameModel;
    [SerializeField] private uint _bufferRequired;

    public string ID => _id;
    public string DisplayName => _displayName;
    public string Description => _description;
    public ItemType Type => _itemType;
    public bool CanCopy => _canCopy;
    public Sprite Icon => _icon;
    public GameObject GameModel => _gameModel;
    public uint BufferRequired => _bufferRequired;
}
