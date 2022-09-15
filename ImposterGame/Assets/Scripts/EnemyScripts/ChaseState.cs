using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIState/Chase")]
public class ChaseState : AIStateSO
{
    [SerializeField] private Transform _target;
    [SerializeField] private string _targetTag;

    private void OnEnable()
    {
        _target = GameObject.FindGameObjectWithTag(_targetTag).transform;
    }

    public override void State(Controller controller)
    {
        if(_target != null)
        {
            controller.TryGetComponent(out Animator _enemyAnimator);
            controller.transform.localScale = new Vector3(
                Mathf.Sign(controller.transform.position.x - _target.position.x) * controller.transform.localScale.x,
                controller.transform.localScale.y,
                controller.transform.localScale.z
                );
            controller.transform.position = Vector3.MoveTowards(controller.transform.position, _target.position, 1f * Time.deltaTime);
            if (_enemyAnimator != null)
            {
                _enemyAnimator.SetBool("isMoving", true);

            }        
        }
    }
}
