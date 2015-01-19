//
//  ShuffleArray.swift
//  Workshop
//
//  Created by Anthony Soulier on 16/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import Foundation

extension Array
{
    /** Randomizes the order of an array's elements. */
    mutating func shuffle()
    {
        for _ in 0..<10
        {
            sort { (_,_) in arc4random() < arc4random() }
        }
    }
}