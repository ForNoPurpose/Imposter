using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _followTarget = GameObject.FindGameObjectWithTag("Player");
        _offset.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _followTarget.transform.position + _offset;
    }
}
