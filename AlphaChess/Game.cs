using System;
using System.Collections;

public class Game
{
    private BitArray board;
    private String player_color;
    private Hashtable boards = new Hashtable();

    public Game()
    {

    }

    private void InitializeNewGame()
    {
        board = new BitArray(new Byte[1] { 0 });
        Console.WriteLine("What color would you like to be?");
        player_color = Console.ReadLine();
    }

    public void Start()
    {
        InitializeNewGame();


    }

}
