using System;
using UnityEngine;

namespace Ebro.Unity.Settings
{
    [Serializable]
    public class CameraSettings
    {
        public Transform Camera;

        public float MinAngle = 0f;
        public float MaxAngle = 85f;

        public float MinDistance = 1f;
        public float MaxDistance = 15f;
        public float Distance = 10.0f;
        public float ZoomSpeed = 0.5f;
    }
}
