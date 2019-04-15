using System;
using System.Collections;

public class Game
{
    private BitArray CurrentBoardState;
    private String player_color;
    private Hashtable boards = new Hashtable();

    public Game()
    {
    }

    private void InitializeNewGame()
    {
        Console.WriteLine("What color would you like to be?");
        player_color = Console.ReadLine();
    }

    public void Start()
    {
        InitializeNewGame();


    }

}
