using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public class EntityWorld
    {
        Dictionary<Type, EntitySystem> systems = new Dictionary<Type, EntitySystem>();
        HashSet<Entity> entities = new HashSet<Entity>();
        HashSet<Filter> filters = new HashSet<Filter>();

        public void Load()
        {
            foreach (var system in systems.Values)
            {
                system.world = this;
                system.OnLoad();
            }
        }

        public void Unload()
        {
            foreach (var system in systems.Values)
                system.OnUnload();
        }

        public void Update()
        {
            foreach (var system in systems.Values)
                system.OnUpdate();
        }

        public void Interval()
        {
            foreach (var system in systems.Values)
                system.OnInterval();
        }
        
        public void Render()
        {
            foreach (var system in systems.Values)
                system.OnRender();
        }

        public void AddSystem(EntitySystem system)
        {
            systems.Add(system.GetType(), system);
        }

        public T GetSystem<T>() where T : EntitySystem
        {
            return systems.GetOrDefault(typeof(T)) as T;
        }

        public Entity CreateEntity(params IComponent[] comps)
        {
            var entity = new Entity();
            foreach (var comp in comps)
                entity.Add(comp);
            foreach (var filter in filters)
                filter.OnEntityChanged(entity);
            return entity;
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
            foreach (var filter in filters)
                filter.OnEntityRemoved(entity);
        }

        public Filter CreateFilter<T>() where T : IComponent
            => CreateFilter(e => e.Has<T>());

        public Filter CreateFilter<T1, T2>() where T1 : IComponent where T2 : IComponent
            => CreateFilter(e => e.Has<T1>() && e.Has<T2>());

        public Filter CreateFilter(Predicate<Entity> cond)
        {
            var filter = new Filter(cond);
            filters.Add(filter);
            return filter;
        }

        public void AddComponent(Entity entity, IComponent comp)
        {
            entity.Add(comp);
            foreach (var filter in filters)
                filter.OnEntityChanged(entity);
        }

        public void RemoveComponent<T>(Entity entity) where T : IComponent
        {
            entity.Remove<T>();
            foreach (var filter in filters)
                filter.OnEntityChanged(entity);
        }
    }
}
