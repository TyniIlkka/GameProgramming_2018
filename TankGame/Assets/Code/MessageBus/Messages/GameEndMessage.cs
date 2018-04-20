using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Messaging {
    public class GameEndMessage : IMessage
    {
        public bool IsGameWin { get; private set; }

        public GameEndMessage(bool isWin)
        {
            IsGameWin = isWin;
        }

    }
}
