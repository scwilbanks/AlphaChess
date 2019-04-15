#!/usr/bin/env python3


"""
Monte Carlo Tree Search Module to be executed on Tree objects.

"""


import random
import printing
import time
import math
import tree
import concurrent.futures


MCTS_FACTOR = 10000


def mcts(root, times=MCTS_FACTOR):
    """
    Main mcts function. Does MCTS times number of iterations

    """

    count = 0
    total_depth = 0
    t0 = time.perf_counter()

    if not root[1]:
        leaf, depth = find_best_leaf(root)
        leaf = tree.make_branches(leaf)
        root = backup(leaf)

    branches = root[1]
    futures = {}
    threads = 1

    with concurrent.futures.ThreadPoolExecutor(max_workers=threads) as executor:


        items = list(map(list, root[1].items()))

        count = 0
        i = 0
        depth = 0
        total_depth = 0
        depth_count = 0

        while count < MCTS_FACTOR:
            print('count', count, 'lastest depth', depth)

            results = executor.map(do_mcts, items)
            
            for result in results:
                move = result[0]
                branch = result[1]
                depth = result[2]
                total_depth += depth
                depth_count += 1
                count += 1
                root[1][move] = branch

    root[7] = 0
    for key in branches:
        branch = branches[key]
        root[7] += branch[7]
    
    for key in branches:
        branches[key][-1] = tree.calc_uct(branch)

    t1 = time.perf_counter()
    print('avg depth:', total_depth/float(depth_count))
    print('avg mcts:', (t1-t0)/count)

    return root


def is_checkmate(leaf):
    return False


def do_mcts(item):
    """
    Single mcts iteration
    """


    move = item[0]
    branch = item[1]

    t0 = time.perf_counter()
    leaf, depth = find_best_leaf(branch)
    t1 = time.perf_counter()

    if is_checkmate(leaf):
        leaf[8] = float('inf')
    else:
        t0 = time.perf_counter()
        leaf = tree.make_branches(leaf)
        t1 = time.perf_counter()

    t0 = time.perf_counter()
    branch = backup(leaf)
    t1 = time.perf_counter()

    return (move, branch, depth)


def find_best_leaf(root):
    cur = root
    depth = 0

    while cur[1] != {}:
        highest_uct = float('-inf')
        best_branch = None
        best_key = None
        branches = cur[1]

        for key in branches:
            branch = branches[key]
            branch_uct = branch[-1]

            if branch_uct > highest_uct:
                highest_uct = branch_uct
                best_branch = branch
                best_key = key

        depth += 1
        cur = best_branch

    return cur, depth

def backup(leaf):
    """
    updates tree and every parent above it
    """

    cur = leaf

    while True:

        branches = cur[1]
        turn = cur[4]

        if turn == 'w':

            cur_number = 0
            best_value = float('-inf')

            for key in branches:
                branch = branches[key]

                branch_number = branch[7]
                cur_number += branch_number

                future_value = branch[8]

                if future_value > best_value:
                    best_value = future_value

            cur[7] = cur_number
            cur[8] = best_value

        elif turn == 'b':

            cur_number = 0
            best_value = float('inf')

            for key in branches:
                branch = branches[key]
            
                branch_number = branch[7]
                cur_number += branch_number

                future_value = branch[8]

                if future_value < best_value:
                    best_value = future_value
            
            cur[7] = cur_number
            cur[8] = best_value

        for key in branches:
            
            branch = branches[key]
            
            branch[-1] = tree.calc_uct(branch)

        if cur[0] and cur[0][0]:
            cur = cur[0]
        else:
            return cur
