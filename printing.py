#!/usr/bin/env python3

"""
Printing Module for printing trees, nodes, games in specific formats

"""

import copy
import tree

def printNode(node):
    printBoard(node[2])
    printProperties(node)

def printProperties(node):
    
    print('Value:', tree.calc_value(node[2]))
    print('UCT:', node[9])
    print('Future Value:', node[8])
    print('Number:', node[7])


def printBoard(board):
    """
    Prints to screen game and attributes of node.
    """

    board = copy.deepcopy(board)

    columns = '      a     b     c     d     e     f     g     h' 

    print(columns)
    for i in range(7, -1, -1):
        print('   ' + ' -----' * 8)
        print_row = ' ' + str(i+1) + ' | '
        for square in board[i]:
            if square is None:
                print_square = '   '
            else:
                print_square = square
                if len(print_square) < 3:
                    print_square = print_square+' '
            print_row += print_square+' | '
        print_row += ' ' + str(i+1)
        print(print_row)


    print('  ' + ' -----' * 8)
    print(columns)
