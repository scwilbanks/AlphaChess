#!/usr/bin/env python3


"""
Main game program

"""


import copy
import tree
import mcts
import printing
import boards


def choose_player_color(): #!
    """
    Takes input from player on if he is playing white or black and returns
    result.

    Args:
        None

    Returns:
        str: 'white' or 'black'

    """

    color = input("Player color: white or black?\n>")

    if color == 'w' or color == 'white':
        color = 'w'
    elif color == 'b' or color == 'black':
        color = 'b'

    return color


def execute_opponent_turn(cur):
    """
    Executes opponent's turn and returns new game state

    Takes current game tree
    Executes mcts
    Displays best move
    Prompts for input for move
    Finds branch that corresponds to move
    Checks for win
    Returns new current branch.

    Args:
        current (Tree): Tree object of current game state

    Returns:
        Tree: branch that becomes new game state after move

    """

    branches = cur[1]
 
    if not branches:
        mcts.mcts(cur, 50)

    print('Current Game:')
    printing.printNode(cur)
    print()
     
    print('Best move:')
    print(tree.get_best_move(cur))
    
    print('Move, Future Value, UCT, Number')
    
    branches = cur[1]
    
    for key in sorted(branches.keys()):
        branch = branches[key]
        print(key, branch[8], branch[9], branch[7])

    valid = False

    while not valid:

        move = input("Opponent's move >")
        if move in branches:
            valid = True

    cur = branches[move]
    cur[0] = None

    return cur


def execute_player_turn(cur):
    """
    Executes player's turn and returns new game state

    Takes current game tree
    Executes mcts
    Displays best move
    Prompts for input for move
    Finds branch that corresponds to move
    Checks for win
    Returns new current branch.

    Args:
        current (Tree): Tree object of current game state

    Returns:
        Tree: branch that becomes new game state after move

    """
    
    cur =  mcts.mcts(cur)
    
    print('Current Game:')
    printing.printNode(cur)
    print()
     
    print('Best move:')
    print(tree.get_best_move(cur))

    print('Move, Future Value, UCT, Number')
    
    branches = cur[1]
    
    for key in sorted(branches.keys()):
        branch = branches[key]
        print(key, branch[8], branch[9], branch[7])    

    valid = False

    while not valid:

        move = input("Player's move >")
        if move in branches:
            valid = True

    cur = branches[move]
    cur[0] = None

    return cur
 

def play():
    """
    Executes the game.

    Initializes new game.
    Takes input for if playing as white or black.
    Loops through turns until game is over.

    Args:
        None

    Returns:
        None

    """
    
    board = copy.deepcopy(boards.NEW)
    player_color = choose_player_color()

    cur = [None, {}, board, player_color, 'w', True, True, 1, 0, None]

    if player_color == 'w':
        cur = execute_player_turn(cur)

    while True:

        cur = execute_opponent_turn(cur)
        cur = execute_player_turn(cur)


if __name__ == '__main__':
    play()
