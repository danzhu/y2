using ECS;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace y2.Components
{
    public class ModelComponent : IComponent
    {
        public Model Model;

        public ModelComponent(Model model = null)
        {
            Model = model;
        }
    }
}
