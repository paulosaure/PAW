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
        
        self.name = json["name"].stringValue;
        
        if let frieze = json["frieze"].array {
            for item in frieze{
                
                let index = item["position"].intValue
                let imageName = item["image"].stringValue
                let slot = Slot(index: index, withPictureName: imageName)

                self.slots.append(slot)
            }
        }
    }
    
}
