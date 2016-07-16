using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public sealed class Entity
    {
        internal Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

        public Entity() { }

        public T Get<T>() where T : class, IComponent => components.GetOrDefault(typeof(T)) as T;

        public bool Has<T>() where T : IComponent => components.ContainsKey(typeof(T));

        internal void Add(IComponent comp) => components.Add(comp.GetType(), comp);

        internal void Remove<T>() where T : IComponent => components.Remove(typeof(T));
    }
}
