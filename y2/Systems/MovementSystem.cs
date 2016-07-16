using ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using y2.Components;
using y2.Data;

namespace y2.Systems
{
    public class MovementSystem : EntitySystem
    {
        public const int StepsPerMeter = 4;
        public static readonly Click UpdateTime = Click.PerSecond(2);

        GameEngine game;
        CollisionSystem collision;
        Filter moving;

        public MovementSystem(GameEngine engine)
        {
            game = engine;
        }

        public void Stop(Entity entity)
        {
            var ani = entity.Get<AnimationComponent>();
            var mov = entity.Get<MovementComponent>();
            var gri = entity.Get<GridComponent>();

            ani.IsEnabled = false;
            mov.LastVelocity = Int3.Zero;
            mov.NextPosition = gri.Position;
            mov.NextOffset = Int3.Zero;
        }

        protected override void OnLoad()
        {
            collision = World.GetSystem<CollisionSystem>();

            moving = World.CreateFilter(e => e.Has<MovementComponent>());
        }

        protected override void OnInterval()
        {
            var time = game.Time;
            foreach (var entity in moving)
            {
                var mov = entity.Get<MovementComponent>();

                if (time < mov.NextUpdate)
                    continue;

                var ani = entity.Get<AnimationComponent>();

                // only animate if the entity is / will be moving
                ani.IsEnabled = mov.LastVelocity != Int3.Zero || mov.TargetVelocity != Int3.Zero;
                if (!ani.IsEnabled)
                    continue;

                var off = mov.NextOffset + (mov.TargetVelocity + mov.LastVelocity) * (UpdateTime * StepsPerMeter) / 2;
                var pos = mov.NextPosition + off / StepsPerMeter;
                off %= StepsPerMeter;

                // update animation first, so that it's based on current position
                ani.Position = mov.NextPosition + (Vector3)mov.NextOffset / StepsPerMeter;
                ani.Velocity = mov.LastVelocity;
                ani.Acceleration = (Vector3)(mov.TargetVelocity - mov.LastVelocity) / UpdateTime;
                ani.StartTime = time;

                // update position ahead of time, so collisions can be detected before happening
                mov.NextPosition = pos;
                mov.NextOffset = off;

                mov.LastVelocity = mov.TargetVelocity;
                mov.NextUpdate = time + UpdateTime;
            }
        }
    }
}
