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
    public class AnimationSystem : EntitySystem
    {
        GameEngine game;
        Filter animated;

        public AnimationSystem(GameEngine engine)
        {
            game = engine;
        }

        protected override void OnLoad()
        {
            animated = World.CreateFilter<AnimationComponent>();
        }

        protected override void OnUpdate()
        {
            var time = game.Time;
            var progress = game.UpdateProgress;

            foreach (var entity in animated)
            {
                var ani = entity.Get<AnimationComponent>();

                if (!ani.IsEnabled)
                    continue;

                var tra = entity.Get<TransformComponent>();

                var t = (time - ani.StartTime) + progress / Click.ClicksPerSecond;
                tra.Position = ani.Acceleration * (t * t / 2f) + ani.Velocity * t + ani.Position;

                // update rotation only when velocity is large (to avoid sudden turning)
                var vel = ani.Acceleration * t + ani.Velocity;
                if (vel.LengthSquared() > 1e-4f)
                    tra.Rotation = (float)Math.Atan2(-vel.Z, vel.X);
            }
        }

    }
}
