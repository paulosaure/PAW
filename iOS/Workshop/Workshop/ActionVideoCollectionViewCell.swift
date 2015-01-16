//
//  ActionVideoCollectionViewCell.swift
//  Workshop
//
//  Created by Anthony Soulier on 16/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit
import MediaPlayer


class ActionVideoCollectionViewCell: UICollectionViewCell {
    
    @IBOutlet weak var imageView: UIImageView!

    var moviePlayer : MPMoviePlayerViewController?

    var slot: Slot! {
        didSet{
            imageView.image = UIImage(named: slot.pictureName);
        }
    }
    
    
    @IBAction func playVideo(sender: UIButton) {
        launchVideo()
    }
    
    
    func launchVideo(){
        
        let name = slot.pictureName
        
        if let path = NSBundle.mainBundle().pathForResource(name, ofType:"mp4"){
            let url = NSURL.fileURLWithPath(path)
            moviePlayer = MPMoviePlayerViewController(contentURL: url)
            
            if let controller = self.window?.rootViewController {
            
            if let player = moviePlayer {
                
                controller.presentMoviePlayerViewControllerAnimated(player)
            }
        }
        }
    }
    
}
