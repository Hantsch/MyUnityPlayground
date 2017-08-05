using System;

namespace Ebro.Unity.Settings
{
    [Serializable]
    public class MovementSettings
    {
        public float MovementSpeed = 25f;
        public float JumpForce = 25f;
        public float InAirMovementSpeed = 50f;
        public int Jumps = 2;

        internal int UsedJumps = 0;
    }
}
