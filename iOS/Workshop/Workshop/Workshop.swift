//
//  Frieze.swift
//  Workshop
//
//  Created by Anthony Soulier on 09/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class Workshop {
    
    var slots : [Slot] = [Slot]()
    var fullVideoUrl : NSURL?
    
    
    
    init(fromSlots slots:[Slot]){
        self.slots = slots
    }
    
}
