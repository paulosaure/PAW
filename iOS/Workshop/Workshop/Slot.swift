//
//  Slot.swift
//  Workshop
//
//  Created by Anthony Soulier on 09/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class Slot {
    
    var pictureName : String = ""
    var index : Int = 0
    
    init(index:Int, withPictureName pictureName:String){
        self.index = index
        self.pictureName = pictureName
    }
    
}
