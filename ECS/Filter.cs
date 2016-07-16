using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS
{
    public class Filter : IEnumerable<Entity>
    {
        HashSet<Entity> items = new HashSet<Entity>();

        public Predicate<Entity> Condition { get; set; }

        public Filter(Predicate<Entity> cond)
        {
            Condition = cond;
        }

        internal void OnEntityChanged(Entity entity)
        {
            var has = items.Contains(entity);
            var match = Condition(entity);
            if (match && !has)
                items.Add(entity);
            else if (!match && has)
                items.Remove(entity);
        }

        internal void OnEntityRemoved(Entity entity)
        {
            if (items.Contains(entity))
                items.Remove(entity);
        }

        #region Interface
        public IEnumerator<Entity> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
        #endregion
    }
}
