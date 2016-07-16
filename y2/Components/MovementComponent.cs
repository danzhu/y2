using ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using y2.Data;

namespace y2.Components
{
    public class MovementComponent : IComponent
    {
        public Int3 NextPosition;

        /// <summary>
        /// Represents the offset from <see cref="GridComponent.Position"/> in steps.
        /// </summary>
        public Int3 NextOffset;

        /// <summary>
        /// The velocity this <see cref="Entity"/> will reach when the current
        /// update ends.
        /// </summary>
        public Int3 LastVelocity;

        /// <summary>
        /// The velocity this <see cref="Entity"/> will try to reach during next update.
        /// Set this value to accelerate / decelerate.
        /// </summary>
        public Int3 TargetVelocity;

        /// <summary>
        /// Time stamp of the planned next update of movement directive.
        /// </summary>
        public Click NextUpdate;
    }
}
