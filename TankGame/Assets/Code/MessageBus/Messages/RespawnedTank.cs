using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Messaging {
    public class RespawnedTank : IMessage
    {
        public Unit DeadUnit { get; private set; }

        public RespawnedTank (Unit unit)
        {
            DeadUnit = unit;
        }
        
    }
}
