using ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using y2.Components;
using y2.Data;

namespace y2.Systems
{
    public class VoxelSystem : EntitySystem
    {
        Dictionary<Int3, Voxel> voxels = new Dictionary<Int3, Voxel>();

        public Voxel this[Int3 pos]
        {
            get { return voxels.GetOrDefault(pos); }
            set
            {
                var old = this[pos];
                if (old.Type != null)
                    World.RemoveEntity(old.Entity);
                voxels[pos] = value;
                if (value.Type != null)
                {
                    var entity = value.Entity;
                    entity.Get<GridComponent>().Position = pos;
                    entity.Get<TransformComponent>().Position = pos;
                }
            }
        }
    }
}
