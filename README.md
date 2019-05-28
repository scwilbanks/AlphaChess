# AlphaChess

AlphaChess is a Command Line Chess game with an AI player that recommends the best move at the current position based on Monte Carlo Tree Search and the Minimax algorithm.

## Motivation

My motivation is to create a Monte Carlo Tree Search (MCTS) program similar to DeepMind's AlphaGo, but for Chess and without a neural network. My particular interest is in how to develop algorithms that can make decisions without being excplicitly programed. AlphaGoZero was milestone in AI research for showing that not only is it effective for an AI system to make 'intelligent' decisions in complex situations with no a priori knowledge, but also that it can be much more effective than when programmed to take advantage of expert human advice.

## How it works

During each player's turn, AlphaChess takes the current board state (which includes the position of the pieces, whose turn it is, whether or not a player can castle, etc.) and looks at every possible move the player can make from that position. Then it expands a tree of future positions with MCTS and discovers any possible checkmates. Finally, it prints the board and pieces themselves to the screen, recommends the best move (as well other data about each move), and prompts the user for his move.

## How Board Positions are Evaluated

Each board state has 4 values that are associated with that board state:

1) Whether or not White is in checkmate.

2) Whether or not Black is in checkmate.

3) Visit Number, number of times that position has been considered by the MCTS algorithm.

4) Upper Confidence Tree (UCT) score, which is a measure of how good a move is (whether or not it results in checkmating your opponent) versus how much that move has been explored relative to other move options. A move that finds a future checkmate with only one look will not have a high UCT score because it is relatively unexplored. Likewise, a move branch with numerous favorable checkmates and has been explored many times will have a high UCT and you can be confident that it is a promissing move.

## Monte Carlo Tree Search

Each turn the MCTS algorithm goes through a number of MCTS iterations until it recommends the best possible move with the information it gathered.

Each MCTS iteration has the following steps:

1) Searches for the best unexplored board by selecting the moves that result in boards with the highest UCT values for the current player.
2) When it reaches that unexplored board, it expands the possible future boards from that position and calculates if it results in a checkmate.
3) Finally, it moves from the now-explored board position back up the tree, along the way updating the Visit Number, Checkmate counts, and UCT values of all the preceeding board positions until it gets back to the current position.

The 'best' move is the move with the highest Visit Number, because it will have a high future value (although not necessarily the highest among possible positions) and will have the most developed tree of future moves. Choosing the move with the highest Visit Number may not be the optimal move for that turn in isolation, but it results in better outcomes over time because it means a more developed tree. This means the future moves will be developed further than they would be at nodes with a lower Visit Number.
