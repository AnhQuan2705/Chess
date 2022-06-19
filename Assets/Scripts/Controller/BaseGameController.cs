using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameController : MonoBehaviour
{
    public static BaseGameController current;
    private EgameState _gameState;
    public EgameState GameState
    {
        get { return _gameState; }
        set { _gameState = value; }
    }
    void Awake()
    {
        current = this;
        GameState = EgameState.PLAYING;
    }
  
}
