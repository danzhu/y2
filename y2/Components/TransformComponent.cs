using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS;
using Microsoft.Xna.Framework;

namespace y2.Components
{
    public class TransformComponent : IComponent
    {
        public Vector3 Position;
        public float Rotation;

        public TransformComponent() : this(0f, 0f, 0f) { }

        public TransformComponent(float x, float y, float z, float rot = 0f)
        {
            Position = new Vector3(x, y, z);
            Rotation = rot;
        }

        public override string ToString() => $"{Position}";
    }
}
