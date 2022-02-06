using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{

    public enum State
    {
        Default                 = 1 << 0,
        DescriptionWindow       = 1 << 1,
        Chatting                = 1 << 2,
        QuestGiving             = 1 << 3,
    }

    public delegate void GameStateEvent(State g);
    public static event GameStateEvent onGameStateChanged = new GameStateEvent((x) => { });

    private static State state = State.Default;
    
    

    public static void SetState(State s)
    {
        state = s;
        onGameStateChanged.Invoke(state);
    }

    public static bool IsState(State s)
    {
        return s == state;
    }

    
}
