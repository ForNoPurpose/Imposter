using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "AIState/Wander")]
public class WanderState : AIStateSO
{
    public Vector3 _nextPosition;
    public Vector3 _currentPosition;
    public float _idleTime = 5f;
    public bool _destinationReached = true;

    private void OnEnable()
    {
        _nextPosition = Vector3.zero;
        _destinationReached = true;
    }

    public override void State(Controller controller)
    {
        controller.TryGetComponent(out Animator enemyAnimator);
        if (!_destinationReached)
        {
            enemyAnimator.SetBool("isMoving", true);
            controller.transform.localScale = new Vector3(Mathf.Sign(controller.transform.position.x - _nextPosition.x), 1, 1);
            controller.transform.position = Vector3.MoveTowards(controller.transform.position, _nextPosition, 1f * Time.deltaTime);
            if (Vector3.Distance(controller.transform.position, _nextPosition) < 0.01f)
            {
                controller.transform.position = _nextPosition;
                _destinationReached = true;
                enemyAnimator.SetBool("isMoving", false);
            }
            return;
        }
        if (_idleTime > 0)
        {
            _idleTime -= Time.deltaTime;
            return;
        }
        else
        {
            _destinationReached = false;
            _nextPosition = GetRandomWaypoint(controller.transform.position);
            _idleTime = 3f;
        }
    }

    private Vector3 GetRandomWaypoint(Vector3 currentPostion)
    {
        float xVal = Mathf.Round(Random.Range(1, 2));
        float sign = Mathf.Sign(Random.value > 0.5f ? 1 : -1);
        return currentPostion + new Vector3(xVal * sign, 0, 0);
    }
}
