using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS;
using y2.Components;
using Microsoft.Xna.Framework;

namespace y2.Systems
{
    public class RenderSystem : EntitySystem
    {
        GameEngine game;
        Filter modeled;
        Matrix view, world, proj;

        public Entity Camera { get; set; }
        public Matrix ViewMatrix => view;
        public Matrix ProjectionMatrix => proj;

        public RenderSystem(GameEngine engine)
        {
            game = engine;
        }

        protected override void OnLoad()
        {
            modeled = World.CreateFilter(e => e.Has<ModelComponent>());
        }

        protected override void OnRender()
        {
            //engine.GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };

            // generate matrixes
            var pos = Camera.Get<TransformComponent>();
            var cam = Camera.Get<CameraComponent>();
            view = Matrix.CreateLookAt(pos.Position, cam.Target, Vector3.Up);
            proj = cam.Perspective ?
                Matrix.CreatePerspectiveFieldOfView(cam.FieldOfView, game.GraphicsDevice.Viewport.AspectRatio, cam.NearPlane, cam.FarPlane) :
                Matrix.CreateOrthographic(cam.Width, cam.Height, cam.NearPlane, cam.FarPlane);

            foreach (var entity in modeled)
            {
                Model model = entity.Get<ModelComponent>().Model;

                var transforms = new Matrix[model.Bones.Count];
                model.CopyBoneTransformsTo(transforms);

                var t = entity.Get<TransformComponent>();
                world = Matrix.CreateRotationY(t.Rotation) * Matrix.CreateTranslation(t.Position);
                foreach (var mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.View = view;
                        effect.World = transforms[mesh.ParentBone.Index] * world;
                        effect.Projection = proj;
                    }
                    mesh.Draw();
                }
            }
        }
    }
}
