using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomUtilities
{
    public static class Utils
    {
        public static Vector3 GetMouseToWorldPosition()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            return worldPos;
        }

        public static TrajectoryData GetTrajectory(Transform startPoint)
        {
            Vector2 displacement = Utils.GetMouseToWorldPosition() - startPoint.position;
            float h = Mathf.Clamp(Mathf.Abs(0.1f + displacement.y), 1, Mathf.Abs(0.1f + displacement.y));
            float gravity = Physics2D.gravity.y;
            float time = (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacement.y - h) / gravity));
            //float time = 0.5f;
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
            Vector3 velocityX = new Vector3(displacement.x, 0, 0) / time;

            return new TrajectoryData(velocityX + velocityY * -Mathf.Sign(gravity), time);
        }

        public struct TrajectoryData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public TrajectoryData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }
        }

        public static void DrawTrajectory(Transform startPoint)
        {
            TrajectoryData trajectory = GetTrajectory(startPoint);
            Vector3 previousDrawPoint = startPoint.position;
            int resolution = 30;
            for (int i = 1; i <= resolution; i++)
            {
                float simulationTime = i / (float)resolution * trajectory.timeToTarget;
                Vector3 drawDisplacement = trajectory.initialVelocity * simulationTime + Vector3.up * Physics2D.gravity.y * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = startPoint.position + drawDisplacement;
                Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
                previousDrawPoint = drawPoint;
            }
        }
    } 
}
