using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        public static Dictionary<string, SceneState> GetSceneList()
        {
            Dictionary<string, SceneState> sceneList = new();
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var temp = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                sceneList.Add(
                    temp,
                    new SceneState(temp)
                    );
            }
            return sceneList;
        }

        public sealed class SceneState
        {
            public string name;
            public Vector3 startPos;
            public Vector3 exitPos;
            public int visitCount;

            public SceneState(string name)
            {
                this.name = name; 
                this.startPos = Vector3.zero; 
                this.exitPos = Vector3.zero;
                visitCount = 0;
            }

            public void AddVisit()
            {
                visitCount++;
            }

            public void SetStartPos(Vector3 pos)
            {
                startPos = pos;
            }

            public void SetExitPos(Vector3 pos)
            {
                exitPos = pos;
            }
        }
    } 
}
