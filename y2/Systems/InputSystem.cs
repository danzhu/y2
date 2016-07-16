using ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using y2.Components;
using y2.Data;

namespace y2.Systems
{
    public class InputSystem : EntitySystem
    {
        GameEngine game;

        public Entity Player { get; set; }

        public InputSystem(GameEngine engine)
        {
            game = engine;
        }

        protected override void OnLoad()
        {
        }

        protected override void OnInterval()
        {
            var key = Keyboard.GetState();
            var mov = Player.Get<MovementComponent>();

            // TODO: more sophisticated input mechanism

            var vel = key.IsKeyDown(Keys.LeftShift) ? 4 : 2;

            if (key.IsKeyDown(Keys.W))
                mov.TargetVelocity.X = vel;
            else if (key.IsKeyDown(Keys.S))
                mov.TargetVelocity.X = -vel;
            else
                mov.TargetVelocity.X = 0;

            if (key.IsKeyDown(Keys.D))
                mov.TargetVelocity.Z = vel;
            else if (key.IsKeyDown(Keys.A))
                mov.TargetVelocity.Z = -vel;
            else
                mov.TargetVelocity.Z = 0;

            //if (key.IsKeyDown(Keys.Space))
            //    mov.TargetVelocity.Y = vel;
            //else
            //    mov.TargetVelocity.Y = 0;
        }
    }
}
