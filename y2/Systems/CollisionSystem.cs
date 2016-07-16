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
    public class CollisionSystem : EntitySystem
    {
        MovementSystem movement;
        VoxelSystem voxel;
        Filter collidable;
        Filter moving;

        protected override void OnLoad()
        {
            movement = World.GetSystem<MovementSystem>();
            voxel = World.GetSystem<VoxelSystem>();

            collidable = World.CreateFilter<CollisionComponent>();
            moving = World.CreateFilter<MovementComponent, CollisionComponent>();
        }

        protected override void OnInterval()
        {
            foreach (var entity in moving)
            {
                var gri = entity.Get<GridComponent>();
                var mov = entity.Get<MovementComponent>();
                var col = entity.Get<CollisionComponent>();

                if (IsMovable(gri.Position, mov.NextPosition + mov.NextOffset.Sign, col.Size))
                {
                    gri.Position = mov.NextPosition;
                }
                else
                {
                    movement.Stop(entity);
                    // TODO: animation and position update after stopping
                }
            }
        }

        public bool IsMovable(Int3 from, Int3 to, Int3 size)
        {
            // TODO: detect size, entity as well
            return from.Range(to).All(i => voxel[i].IsEmpty);
        }
    }
}
