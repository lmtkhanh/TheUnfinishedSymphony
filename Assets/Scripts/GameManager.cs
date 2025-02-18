using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Keep track of what puzzle and combat are completed
States: Menu, Game, CutScene, Puzzle, Combat, End
*/

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Game,
        CutScene,
        Puzzle,
        Combat,
        End
    }
    public GameState currentState;


    // Start is called before the first frame update
    void Start()
    {
        SetGameState(GameState.Game);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;
    }

}
