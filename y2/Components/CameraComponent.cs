using ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace y2.Components
{
    public class CameraComponent : IComponent
    {
        public bool Perspective = true;
        public float FieldOfView = MathHelper.PiOver4;
        public float Width;
        public float Height;
        public float NearPlane = 0.1f;
        public float FarPlane = 1000f;
        public Vector3 Target = Vector3.Zero;

        public override string ToString() => $"Target: {Target}";
    }
}
