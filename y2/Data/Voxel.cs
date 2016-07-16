using ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using y2.Components;

namespace y2.Data
{
    public struct Voxel
    {
        public VoxelType Type { get; }
        public Entity Entity { get; }

        public bool IsEmpty => Type == null;

        public Voxel(EntityWorld world, VoxelType type)
        {
            Type = type;
            Entity = world.CreateEntity(
                new TransformComponent(),
                new GridComponent(),
                model
                );
        }

        internal static ModelComponent model;
    }
}
