using ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using y2.Data;

namespace y2.Components
{
    public class GridComponent : IComponent
    {
        public Int3 Position;
        public Int3 Offset;
        public Rotation Rotation;

        public GridComponent(int x, int y, int z, int rot = 0)
        {
            Position = new Int3(x, y, z);
            Rotation = new Rotation(rot);
        }

        public GridComponent() : this(0, 0, 0) { }
    }
}
