using ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace y2.Components
{
    public class AnimationComponent : IComponent
    {
        public float StartTime;

        public Vector3 Acceleration;
        public Vector3 Velocity;
        public Vector3 Position;

        public bool IsEnabled = false;
    }
}
