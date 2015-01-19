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
    var name: String
    
    init(fromJSON jsonObject:AnyObject){
        
        let json = JSON(jsonObject)
        
        self.name = json["name"].asString!
        
        if let frieze = json["frieze"].asArray {
            for item in frieze{
                
                let index = item["position"].asInt!
                let imageName = item["image"].asString!
                let slot = Slot(index: index, withPictureName: imageName)

                self.slots.append(slot)
            }
        }
    }
    
}
