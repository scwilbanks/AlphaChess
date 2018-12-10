#!/usr/bin/env python3

"""
Class for tree of game states

"""


import math
import copy
import time


def calc_value(board):
    """
    Based on standard piece evaluations:

    Pawn: 1
    Knight: 3
    Bishop: 3
    Rook: 5
    Queen: 9

    
    TODO:
        -use regression to optimize values
    """
    
    value = 0

    if is_white_checkmate(board):
        return float('inf')
    if is_black_checkmate(board):
        return float('-inf')

    for row in board:
        for square in row:
            if square is not None:
                if square[0] == 'W':
                    if square[1] == 'P':
                        value += 1
                    elif square[1] == 'K' or square[1] == 'B':
                        value += 3
                    elif square[1] == 'R':
                        value += 5
                    elif square[1] == 'Q':
                        value += 9
                elif square[0] == 'B': 
                    if square[1] == 'P':
                        value -= 1
                    elif square[1] == 'K' or square[1] == 'B':
                        value -= 3
                    elif square[1] == 'R':
                        value -= 5
                    elif square[1] == 'Q':
                        value -= 9
        
    return value


def make_pieces(node):
    """
    Takes board and returns dict of pieces on board with locations

    Args:
        board (array): represents board state

    Returns:
        pieces (dict): represents all pieces on board
            keys: str corresponding to piece
            value: tuple corresponding to location of piece

    """
    t0 = time.perf_counter()

    board = node[2]
    pieces = {}

    for i in range(0, 8):
        for j in range(0, 8):
            if board[i][j]:
                pieces[board[i][j]] = (i, j)

    
    t1 = time.perf_counter()

    return pieces


def get_p_moves(piece, square, node):
    """
    TODO: add what happens if pawn gets to end
    """
    
    options = set()

    row = square[0]
    column = square[1]

    board = node[2]
    turn = node[4]

    if turn == 'w':
        if row < 7:
            if board[row+1][column] is None:
                options.add((piece, (row+1, column)))
                if row == 1 and board[row+2][column] is None:
                    options.add((piece, (row+2, column)))
            if column > 0:
                if board[row+1][column-1]:
                    if board[row+1][column-1][0] == 'B':
                        options.add((piece, (row+1, column-1)))
            if column < 7:
                if board[row+1][column+1]:
                    if board[row+1][column+1][0] == 'B':
                        options.add((piece, (row+1, column+1)))

    if turn == 'b':
        if row > 0:
            if board[row-1][column] is None:
                options.add((piece, (row-1, column))) 
                if row == 6 and board[row-2][column] is None:
                    options.add((piece, (row-2, column)))
            if column > 0:
                if board[row-1][column-1]:
                    if board[row-1][column-1][0] == 'W':
                        options.add((piece, (row-1, column-1)))
            if column < 7:
                if board[row-1][column+1]:
                    if board[row-1][column+1][0] == 'W':
                        options.add((piece, (row-1, column+1)))

    return options


def get_n_moves(piece, square, node):
    
    options = set()

    row = square[0]
    column = square[1]

    board = node[2]
    turn = node[4]

    if row < 6:
        if column > 0:
            move = board[row+2][column-1]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row+2, column-1)))
        if column < 7:
            move = board[row+2][column+1]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row+2, column+1)))
    if row < 7:
        if column > 1:
            move = board[row+1][column-2]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row+1, column-2)))
        if column < 6:
            move = board[row+1][column+2]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row+1, column+2)))

    if row > 1:
        if column > 0:
            move = board[row-2][column-1]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row-2, column-1)))
        if column < 7:
            move = board[row-2][column+1]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row-2, column+1)))
    if row > 0:
        if column > 1:
            move = board[row-1][column-2]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row-1, column-2)))
        if column < 6:
            move = board[row-1][column+2]
            if move is None or move[0].lower() != turn:
                options.add((piece, (row-1, column+2)))

    return options


def get_r_moves(piece, square, node):
    
    options = set()

    row = square[0]
    column = square[1]
    
    board = node[2]
    turn = node[4]

    if row < 7:
        up_squares = set()


        for up_row in range(row+1, 8):

            if board[up_row][column] is None:

                up_squares.add((piece, (up_row, column)))
            else:
                if board[up_row][column][0].lower() != turn:
                    up_squares.add((piece, (up_row, column)))
                break


        options = options.union(up_squares)

    #searching down
    if row > 0:
        down_squares = set()
        for down_row in range(row-1, -1, -1):

            if board[down_row][column] is None:
                down_squares.add((piece, (down_row, column)))
            else:
                if board[down_row][column][0].lower() != turn:
                    down_squares.add((piece, (down_row, column)))
                break
        

        options = options.union(down_squares)
    
    #searching right
    if column < 7:
        right_squares = set()
        for right_column in range(column+1, 8):

            if board[row][right_column] is None:
                right_squares.add((piece, (row, right_column)))
            else:
                if board[row][right_column][0].lower() != turn:
                    right_squares.add((piece, (row, right_column)))
                break

        options = options.union(right_squares)

    #searching left
    if column > 0:
        left_squares = set()
        for left_column in range(column-1, -1, -1):
            if board[row][left_column] is None:
                left_squares.add((piece, (row, left_column)))
            else:
                if board[row][left_column][0].lower() != turn:
                    left_squares.add((piece, (row, left_column)))
                break


        options = options.union(left_squares)

    return options


def get_b_moves(piece, square, node):
    
    """
    Structure get moves methods like this
    """

    options = set()

    row = square[0]
    column = square[1]

    board = node[2]
    turn = node[4]
    
    #searching upper-left (ul)
    if row < 7 and column > 0:
        ul_options = set()

        cur_row = row + 1
        cur_col = column - 1

        while cur_row <= 7 and cur_col >= 0:
            if board[cur_row][cur_col] is None:
                ul_options.add((piece, (cur_row, cur_col)))
                cur_row += 1
                cur_col -= 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    ul_options.add((piece, (cur_row, cur_col)))
                break

        options = options.union(ul_options)

    # Searching upper-right (ur)
    if row < 7 and column < 7:
        ur_options = set()

        cur_row = row + 1
        cur_col = column + 1

        while cur_row <= 7 and cur_col <= 7:
            if board[cur_row][cur_col] is None:
                ur_options.add((piece, (cur_row, cur_col)))
                cur_row += 1
                cur_col += 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    ur_options.add((piece, (cur_row, cur_col)))
                break

        options = options.union(ur_options)


    # Searching lower-left (ll)
    if row > 0 and column > 0:
        ll_options = set()

        cur_row = row - 1
        cur_col = column - 1

        while cur_row >= 0 and cur_col >= 0:
            if board[cur_row][cur_col] is None:
                ll_options.add((piece, (cur_row, cur_col)))
                cur_row -= 1
                cur_col -= 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    ll_options.add((piece, (cur_row, cur_col)))
                break

        options = options.union(ll_options)

    # Searching lower-right (lr)
    if row > 0 and column < 7:
        lr_options = set()

        cur_row = row - 1
        cur_col = column + 1

        while cur_row >= 0 and cur_col <= 7:
            if board[cur_row][cur_col] is None:
                lr_options.add((piece, (cur_row, cur_col)))
                cur_row -= 1
                cur_col += 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    lr_options.add((piece, (cur_row, cur_col)))
                break

        options = options.union(lr_options)


    return options

def get_q_moves(piece, square, node):

    options = set()

    row = square[0]
    column = square[1]

    board = node[2]
    turn = node[4]

    #searching up
    if row < 7:
        up_squares = set()

        for up_row in range(row+1, 8):

            if board[up_row][column] is None:

                up_squares.add((piece, (up_row, column)))
            else:
                if board[up_row][column][0].lower() != turn:
                    up_squares.add((piece, (up_row, column)))
                break


        options = options.union(up_squares)

    #searching down
    if row > 0:
        down_squares = set()
        for down_row in range(row-1, -1, -1):

            if board[down_row][column] is None:
                down_squares.add((piece, (down_row, column)))
            else:
                if board[down_row][column][0].lower() != turn:
                    down_squares.add((piece, (down_row, column)))
                break
        
        options = options.union(down_squares)
    
    #searching right
    if column < 7:
        right_squares = set()
        for right_column in range(column+1, 8):

            if board[row][right_column] is None:
                right_squares.add((piece, (row, right_column)))
            else:
                if board[row][right_column][0].lower() != turn:
                    right_squares.add((piece, (row, right_column)))
                break

        options = options.union(right_squares)

    #searching left
    if column > 0:
        left_squares = set()
        for left_column in range(column-1, -1, -1):
            if board[row][left_column] is None:
                left_squares.add((piece, (row, left_column)))
            else:
                if board[row][left_column][0].lower() != turn:
                    left_squares.add((piece, (row, left_column)))
                break

        options = options.union(left_squares)

    #searching ul
    if row < 7 and column > 0:
        ul_options = set()

        cur_row = row + 1
        cur_col = column - 1

        while cur_row <= 7 and cur_col >= 0:
            if board[cur_row][cur_col] is None:
                ul_options.add((piece, (cur_row, cur_col)))
                cur_row += 1
                cur_col -= 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    ul_options.add((piece, (cur_row, cur_col)))
                break
        options = options.union(ul_options)

    # Searching upper-right (ur)
    if row < 7 and column < 7:
        ur_options = set()

        cur_row = row + 1
        cur_col = column + 1

        while cur_row <= 7 and cur_col <= 7:
            if board[cur_row][cur_col] is None:
                ur_options.add((piece, (cur_row, cur_col)))
                cur_row += 1
                cur_col += 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    ur_options.add((piece, (cur_row, cur_col)))
                break

        options = options.union(ur_options)

    # Searching lower-left (ll)
    if row > 0 and column > 0:
        ll_options = set()

        cur_row = row - 1
        cur_col = column - 1

        while cur_row >= 0 and cur_col >= 0:
            if board[cur_row][cur_col] is None:
                ll_options.add((piece, (cur_row, cur_col)))
                cur_row -= 1
                cur_col -= 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    ll_options.add((piece, (cur_row, cur_col)))
                break

        options = options.union(ll_options)

    # Searching lower-right (lr)
    if row > 0 and column < 7:
        lr_options = set()

        cur_row = row - 1
        cur_col = column + 1

        while cur_row >= 0 and cur_col <= 7:
            if board[cur_row][cur_col] is None:
                lr_options.add((piece, (cur_row, cur_col)))
                cur_row -= 1
                cur_col += 1
            else:
                if board[cur_row][cur_col][0].lower() != turn:
                    lr_options.add((piece, (cur_row, cur_col)))
                break

        options = options.union(lr_options)

    return options
    

def get_k_moves(piece, square, node, shallow=False):
    """
    TODO: add castling
    """

    options = set()

    row = square[0]
    column = square[1]

    board = node[2]
    turn = node[4]

    #searching up
    if row < 7:
        if board[row+1][column] is None:
            options.add((piece, (row+1, column)))
        elif board[row+1][column][0].lower() != turn:
            options.add((piece, (row+1, column)))


    #searching down
    if row > 0:
        if board[row-1][column] is None:
            options.add((piece, (row-1, column)))
        elif board[row-1][column][0].lower() != turn:
            options.add((piece, (row-1, column)))
    
    #searching right
    if column < 7:
        if board[row][column+1] is None:
            options.add((piece, (row, column+1)))
        elif board[row][column+1][0].lower() != turn:
            options.add((piece, (row, column+1)))

    #searching left
    if column > 0:
        if board[row][column-1] is None:
            options.add((piece, (row, column-1)))
        elif board[row][column-1][0].lower() != turn:
            options.add((piece, (row, column-1)))


    #searching ul
    if row < 7 and column > 0:
        if board[row+1][column-1] is None:
            options.add((piece, (row+1, column-1)))
        elif board[row+1][column-1][0].lower() != turn:
            options.add((piece, (row+1, column-1)))

    # Searching upper-right (ur)
    if row < 7 and column < 7:
        if board[row+1][column+1] is None:
            options.add((piece, (row+1, column+1)))
        elif board[row+1][column+1][0].lower() != turn:
            options.add((piece, (row+1, column+1)))


    # Searching lower-left (ll)
    if row > 0 and column > 0:
        if board[row-1][column-1] is None:
            options.add((piece, (row-1, column-1)))
        elif board[row-1][column-1][0].lower() != turn:
            options.add((piece, (row-1, column-1)))

    # Searching lower-right (lr)
    if row > 0 and column < 7:
        if board[row-1][column+1] is None:
            options.add((piece, (row-1, column+1)))
        elif board[row-1][column+1][0].lower() != turn:
            options.add((piece, (row-1, column+1)))

    if shallow:
        return options

    white_castleable = node[5]
    black_castleable = node[6]

    if turn == 'w':
        next_turn = 'b'
    elif turn == 'b':
        next_turn = 'w'
    
    if turn == 'w' and white_castleable:
        conditions = []
        conditions.append(board[0][4] == 'WK')
        conditions.append(board[0][0] == 'WR1')
        conditions.append(board[0][1] == None)
        conditions.append(board[0][2] == None)
        conditions.append(board[0][3] == None)
        if all(conditions):
            conditions = []
            enemy_node = copy.deepcopy(node)
            enemy_node[4] = next_turn
            enemy_pieces = make_pieces(enemy_node)
            enemy_options = make_options(enemy_node, enemy_pieces, shallow=True)

            for option in enemy_options:
                conditions.append(option[-2:] != 'e1')
                conditions.append(option[-2:] != 'd1')
                conditions.append(option[-2:] != 'c1')

            if all(conditions):
                options.add((piece, (0, 2)))
    
        conditions = []
        conditions.append(board[0][4] == 'WK')
        conditions.append(board[0][7] == 'WR2')
        conditions.append(board[0][5] == None)
        conditions.append(board[0][6] == None)
        if all(conditions):
            conditions = []
            enemy_node = copy.deepcopy(node)
            enemy_node[4] = next_turn
            enemy_pieces = make_pieces(enemy_node)
            enemy_options = make_options(enemy_node, enemy_pieces, shallow=True)

            for option in enemy_options:
                conditions.append(option[-2:] != 'e1')
                conditions.append(option[-2:] != 'f1')
                conditions.append(option[-2:] != 'g1')

            if all(conditions):
                options.add((piece, (0, 6)))



    if turn == 'b' and black_castleable:
        conditions = []
        conditions.append(board[7][4] == 'BK')
        conditions.append(board[7][0] == 'BR1')
        conditions.append(board[7][1] == None)
        conditions.append(board[7][2] == None)
        conditions.append(board[7][3] == None)
        if all(conditions):
            conditions = []
            enemy_node = copy.deepcopy(node)
            enemy_node[4] = next_turn
            enemy_pieces = make_pieces(enemy_node)
            enemy_options = make_options(enemy_node, enemy_pieces, shallow=True)

            for option in enemy_options:
                conditions.append(option[-2:] != 'e8')
                conditions.append(option[-2:] != 'd8')
                conditions.append(option[-2:] != 'c8')

            if all(conditions):
                options.add((piece, (7, 2)))
    
        conditions = []
        conditions.append(board[7][4] == 'BK')
        conditions.append(board[7][7] == 'BR2')
        conditions.append(board[7][5] == None)
        conditions.append(board[7][6] == None)
        if all(conditions):
            conditions = []
            enemy_node = copy.deepcopy(node)
            enemy_node[4] = next_turn
            enemy_pieces = make_pieces(enemy_node)
            enemy_options = make_options(enemy_node, enemy_pieces, shallow=True)

            for option in enemy_options:
                conditions.append(option[-2:] != 'e8')
                conditions.append(option[-2:] != 'f8')
                conditions.append(option[-2:] != 'g8')

            if all(conditions):
                options.add((piece, (7, 6)))

    return options


def make_options(node, pieces, shallow=False):
    """
    Makes options by iterating through pieces and checking valid moves on
    board.

    Args:
        dict: array of arrays representing game board

    Returns:
        set: set of valid moves
    """

    board = node[2]
    turn = node[4]

    options = set()

    for piece in pieces:

        if piece[0].lower() == turn:
            
            square = pieces[piece]

            if piece[1] == 'P':
                p_moves = get_p_moves(piece, square, node)
                options = options.union(p_moves)

            if piece[1] == 'R':
                r_moves = get_r_moves(piece, square, node)
                options = options.union(r_moves)

            if piece[1] == 'N':
                n_moves = get_n_moves(piece, square, node)
                options = options.union(n_moves)

            if piece[1] == 'B':
                b_moves = get_b_moves(piece, square, node)
                options = options.union(b_moves)

            if piece[1] == 'Q':
                q_moves = get_q_moves(piece, square, node)
                options = options.union(q_moves)

            if piece[1] == 'K':
                k_moves = get_k_moves(piece, square, node, shallow)
                options = options.union(k_moves)
   
    return options


def make_branches(node):
    """
    Makes branches by iterating through options and making new tree object
    for each branch.

    Args:
        self: tree object for game state

    Returns:
        dict:
            key is move string from move functions in main.py
            value is tree object corresponding to the state after that move
    """

    branches = {}
    board = node[2]

    key_board_pairs = []

    pieces = make_pieces(node)
    options = make_options(node, pieces)

    for option in options:
        board = list(map(list, node[2]))
        
        piece = option[0]
        row = option[1][0]
        column = option[1][1]

        old_square = pieces[piece]
        board[old_square[0]][old_square[1]] = None

        board[row][column] = piece
        
        key = piece+chr(column+97)+str(row+1)


        key_board_pairs.append([key, board])

    mapped = map(make_pair, key_board_pairs, [node]*(len(key_board_pairs)+1))
    
    for pair in mapped:
        pair[1][0] = node
        branches[pair[0]] =  pair[1]
    
    node[1] = branches
    
    return node


def make_pair(pair, node):

    turn = node[4]
    if turn == 'w':
        next_turn = 'b'
    elif turn == 'b':
        next_turn = 'w'

    branch = [
        None,
        {},
        pair[1],
        node[3],
        next_turn,
        node[5],
        node[6],
        1,
        calc_value(pair[1]),
        None
        ]


    return [pair[0], branch]


def is_white_checkmate(board):
    pass


def is_black_checkmate(board):
    pass


def get_best_move(node):
    """
    Won't need this when make_branches becomes a heap
    """
    
    branches = node[1]
    best_number = 0
    best_move = None

    for key in branches:
        branch = branches[key]
        branch_number = branch[7]

        if branch_number > best_number:
            best_move = key
            best_number = branch_number

    return best_move


def calc_uct(branch):
    """
    higher first term means more ways of winning
    higher second term means more explored
    """

    turn = branch[4]
    value = float(branch[8])

    if turn == 'w':
        value = -value

    number = float(branch[7])
    constant = 30 # use regression to find best value here
    parent_number = float(branch[0][7])
    parent_value = float(branch[0][8])

    first_term = value - parent_value
    second_term = constant*pow(math.log(parent_number)/number, .5)

    return -branch[7]
