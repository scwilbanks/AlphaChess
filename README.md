# AlphaChess

AlphaChess is a Command Line Chess game with an AI player that recommends the best move at the current position based on Monte Carlo Tree Search and the Minimax algorithm.

## Motivation

My motivation is to create a Monte Carlo Tree Search (MCTS) program similar to DeepMind's AlphaGo, but for Chess and without a neural network. My particular interest is in how to develop algorithms that can make decisions without being excplicitly programed. AlphaGoZero was milestone in AI research for showing that not only is it effective for an AI system to make 'intelligent' decisions in complex situations with no a priori knowledge, but also that it can be much more effective than when programmed to take advantage of expert human advice.

## How it works

During each player's turn, AlphaChess takes the current board state (which includes the position of the pieces, whose turn it is, whether or not a player can castle, etc.) and looks at every possible move the player can make from that position. Then it expands a tree of future positions with MCTS and assigns values to those positions. Finally, it prints the board and pieces themselves to the screen, recommends the best move (as well other data about each move), and prompts the user for his move.

## How Board Positions are Evaluated

Each board state has 4 values that are associated with that board state:

1) Visit Number, number of times that position has been considered by the MCTS algorithm.

2) Material Value, which is the sum of the player's pieces' values minus the sum of the opponent's pieces' values. It uses Pawn = 1, Knight = 3, Bishop = 3, Rook = 5, Queen = 9. In AlphaZero, this value is given by a convolutional neural network, and the weights within that neural network changed based on if the game was won or lost.

3) Future Value, which is the value of the board position if the player takes his best move among the possible current moves, then the opponent makes his best move (which is the worst move for the player), then the player takes his best move after that, and so on. This is the Minimax algorithm. Making decisions based on this future value rather than current value quickly eliminates moves with bad consequences to the player such as moving to a position where your opponent can take a piece and the player gains nothing in return.

4) Upper Confidence Tree (UCT) score, which is a measure of how good a move is versus how much that move and its future moves have been explored. A move with a high future value after one look will not have a high UCT score because it is relatively unexplored. Likewise, a move with a high future value and has been explored many times will have a high UCT and you can be confident that it is a promissing move.

## Monte Carlo Tree Search

Each turn the MCTS algorithm goes through a number of MCTS iterations until it recommends the best possible move with the information it gathered. In it's current version, I've found 10,000 iterations to be a good balance between depth of nodes and not taking too much time.

Each MCTS iteration has the following steps:

1) Searches for the best unexplored board by selecting the moves that result in boards with the highest UCT values for the current player.
2) When it reaches that unexplored board, it expands the possible future boards from that position, calculates the Material Value of that position.
3) Finally, it moves from the now-explored board position back up the tree, along the way updating the Visit Number, Future Value, and UCT values of all the preceeding board positions until it gets back to the current position.

The 'best' move is the move with the highest Visit Number, because it will have a high future value (although not necessarily the highest among possible positions) and will have the most developed tree of future moves. Choosing the move with the highest Visit Number may not be the optimal move for that turn in isolation, but it results in better outcomes over time because it means a more developed tree. This means the future moves will be developed further than they would be at nodes with a lower Visit Number.

## Planned Optimizations

### Saving space = saving time
On my machine, one 10,000 MCTS iterations from the starting position takes up 400MBs. This is a staggering amount of space and totally unsustainable at the scale required for this program to be useful. 

I know from timing my code that the biggest time sink is copying the information into memory. Currently each node is implmented as a list of various types. See below for the data and their types:


Parent | Reference

Branches | Dictionary

Board | 2D List

Player Color | String

Turn | String

Castleable White | List of 3 Booleans

Castleable Black | List of 3 Booleans

Visit Number | Int

Future Value | Int

UCT | Float


TODOs:

1) Replace the branches dictionary with a list. Each position has a finite number of moves, maybe 20-25 if there are a lot but generally less. The time savings from constant time access to branches is probably not worth the overhead of initializing hundreds of thousands of dictionaries every 2 seconds.

2) Replace most of the other information in the node's list with a bitstring. Here's how I see it working out:

Represent if a piece is taken: 1 bit x 32 pieces = 32 bits
Represent place on board if not taken: 6 bits x 32 pieces = 192 bits
Represent admin data (player color, turn, castleable_white, and castleable_black): 8 bits

The other values can likely be converted to a bitstring too, but would require a lot more work, and the space savings less substantial.

## Multithreading
Python's Global Interpreter Lock (GIL) prevents multiple threads from executing CPU instructions at the same time. This prevents CPU-intensive tasks from a lot of the possible time-savings from multithreading. I played around with the concurrency libraries that Python offers, but I honestly couldn't get multithreading or multiprocessing to give it much speed up.

## Alpha-Beta Pruning
Alpha-Beta Pruning is a method of identifying branches of the tree that are worse than other branches, and disregarding them. It improves on the minimax algorithm and results in a more efficient search since it won't waste time with branches you know are suboptimal.

## References

Tim Wheeler has an amazing overview of MCTS and How AlphaZero works:
http://tim.hibal.org/blog/alpha-zero-how-and-why-it-works/

DeepMind's Nature article also has a great explanation:
https://www.nature.com/articles/nature24270

More info from the DeepMind website:
https://deepmind.com/blog/alphago-zero-learning-scratch/
