using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private AnimationCurve _curve;
    [Range(1f, 10f)]
    [SerializeField] private float _cameraSpeed = 7f;

    BoxCollider2D _boundsBox;

    [SerializeField] float _cameraWidth;
    [SerializeField] float _cameraHeight;

    private void Awake()
    {
        _offset = new Vector3(2.7f, 2.7f, -10);
        if (_followTarget == null)
            _followTarget = GameObject.FindGameObjectWithTag("Player").transform;

        _cameraWidth = 8;
        _cameraHeight = 5;

        transform.position = _followTarget.position;
        _boundsBox = GameObject.FindGameObjectWithTag("LevelBounds").GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        float sign = _followTarget.transform.rotation.y > 0 ? -1 : 1;
        _offset.x = Mathf.Abs(_offset.x) * sign;
        var targetPos = _followTarget.position + _offset;
        targetPos.x = Mathf.Clamp(targetPos.x, _boundsBox.bounds.min.x + (_cameraWidth), _boundsBox.bounds.max.x - (_cameraWidth));
        targetPos.y = Mathf.Clamp(targetPos.y, _boundsBox.bounds.min.y + (_cameraHeight), _boundsBox.bounds.max.y - (_cameraHeight));

        var smoothPos = Vector3.Lerp(transform.position, targetPos, _curve.Evaluate(_cameraSpeed * Time.deltaTime));
        transform.position = smoothPos;
    }

}
