#!/usr/bin/env python3

"""
Collection of common boards

"""

import copy
import random
import tree

PIECES = [
    'WP1',
    'WP2',
    'WP3',
    'WP4',
    'WP5',
    'WP6',
    'WP7',
    'WP8',
    'WR1',
    'WN1',
    'WB1',
    'WQ',
    'WK',
    'WB2',
    'WN2',
    'WR2',
    'BP1',
    'BP2',
    'BP3',
    'BP4',
    'BP5',
    'BP6',
    'BP7',
    'BP8',
    'BR1',
    'BN1',
    'BB1',
    'BQ',
    'BK',
    'BB2',
    'BN2',
    'BR2'
    ]

NEW = [
    ['WR1', 'WN1', 'WB1', 'WQ', 'WK', 'WB2', 'WN2', 'WR2'],
    ['WP1', 'WP2', 'WP3', 'WP4', 'WP5', 'WP6', 'WP7', 'WP8'],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    ['BP1', 'BP2', 'BP3', 'BP4', 'BP5', 'BP6', 'BP7', 'BP8'],
    ['BR1', 'BN1', 'BB1', 'BQ', 'BK', 'BB2', 'BN2', 'BR2']
    ]

EMPTY = [
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None],
    [None, None, None, None, None, None, None, None]
    ]

def get_random_board():

    board = copy.deepcopy(EMPTY)

    for piece in PIECES:
        
        placed = False

        while not placed:
            
            i = random.randrange(0, 8)
            j = random.randrange(0, 8)

            if board[i][j] is None:

                board[i][j] = piece
                placed = True

    return board

def get_random_color():

    colors = ['white', 'black']
    
    return colors[random.randrange(0, 2)]

