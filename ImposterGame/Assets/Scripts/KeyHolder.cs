using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    [SerializeField] private List<KeyType> _keyList;
    private void Awake()
    {
        OpenDoor.OnUnlock += RemoveKey;
        SceneObject.OnPickup += AddKey;
        _keyList = new List<KeyType>();
    }

    private void OnDestroy()
    {
        OpenDoor.OnUnlock -= RemoveKey;
        SceneObject.OnPickup -= AddKey;
    }

    public void AddKey(ItemDataSO keyType)
    {
        if (keyType.KeyType != KeyType.None)
        {
            Debug.Log("Added Key");
            _keyList.Add(keyType.KeyType); 
        }
    }
    public void RemoveKey(KeyType keyType)
    {
        _keyList.Remove(keyType);
    }
    public bool ContainsKey(KeyType keyType)
    {
        return _keyList.Contains(keyType);
    }
}
