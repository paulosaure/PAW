//
//  SlotCollectionViewCell.swift
//  Workshop
//
//  Created by Anthony Soulier on 16/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class SlotCollectionViewCell: UICollectionViewCell {

    @IBOutlet weak var imageView: UIImageView!
    
    var slot : Slot!{
        didSet{
            self.imageView.image = UIImage(named: self.slot.pictureName)
        }
    }
   
}
