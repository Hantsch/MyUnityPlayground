using System;
using UnityEngine;

namespace Ebro.Unity.Settings
{
    [Serializable]
    public class PhysicsSettings
    {
        public float GravityScale = 10f;

        public float GetGravityVelocity()
        {
            return Physics.gravity.y * this.GravityScale * Time.deltaTime;
        }
    }
}
