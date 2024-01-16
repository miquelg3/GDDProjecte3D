using UnityEngine;


public class GameState
{
    public enum StateGame
    {
        menu,
        inGame,
        pause,
        gameOver
    }

    public StateGame game { get; set; }

    public GameState(StateGame game)
    {
        this.game = game;
    }

    public void PauseGame()
    {
        game = StateGame.pause;
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        game = StateGame.inGame;
        Time.timeScale = 1;
    }



}
