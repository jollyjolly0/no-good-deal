using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public enum State
    {
        Default                 = 1 << 0,
        DescriptionWindow       = 1 << 1,
        Chatting                = 1 << 2,
        QuestGiving             = 1 << 3,
    }

    public static State state = State.Default;
}
