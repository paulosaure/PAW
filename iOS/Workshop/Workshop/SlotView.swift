//
//  SlotView.swift
//  Workshop
//
//  Created by Anthony Soulier on 16/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class SlotView: UIImageView {

    var slot : Slot!{
        didSet{
            self.image = UIImage(named: self.slot.pictureName)
        }
        
    }
    
}
