using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public abstract class EntitySystem
    {
        internal EntityWorld world;
        public EntityWorld World => world;

        protected internal virtual void OnLoad() { }
        protected internal virtual void OnUnload() { }
        protected internal virtual void OnUpdate() { }
        protected internal virtual void OnInterval() { }
        protected internal virtual void OnRender() { }
    }
}
