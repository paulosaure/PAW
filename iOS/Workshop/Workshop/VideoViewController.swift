//
//  VideoViewController.swift
//  Workshop
//
//  Created by Anthony Soulier on 09/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit
import MediaPlayer

class VideoViewController: UICollectionViewController {
    
    private var socket : SIOSocket!
    private var socketIsConnected : Bool = false
    
    private var workshop : Workshop? {
        didSet {
            collectionView?.reloadData();
        }
    }
    
    var moviePlayer : MPMoviePlayerViewController?

    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        SIOSocket.socketWithHost("http://localhost:8080", response: { (socket: SIOSocket!) -> Void in
            
            self.socket = socket
            
            self.socket.onConnect = {() -> Void in
                self.socketIsConnected = true
                println("Connection")
            }
            
            self.socket.onError = {(var errorInfo) -> Void in
                println("Connection to server failed")
            }

            self.socket.onDisconnect = {() -> Void in
                self.socketIsConnected = false
            }
            
            self.socket.on("change_vue", callback: { (mes: [AnyObject]!) -> Void in
                if let message = mes[0] as? String{
                    println(message)
                }
            })
            
            self.socket.on("workshop", callback: { (mes :[AnyObject]!) -> Void in
                
                if let message = mes[0] as? [String:AnyObject]{
                    self.workshop = Workshop(fromJSON: message)
                }
            })
            
        })
        
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func refreshWorkshop(sender: AnyObject) {
        
        socket.emit("ask_for_workshop")
        
    }

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */
    
    // MARK: - UICollectionViewDatasource
    
    override func numberOfSectionsInCollectionView(collectionView: UICollectionView) -> Int {
        if let workshop = self.workshop{
            return 1;
        }
        return 0;
    }
    
    override func collectionView(collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        
        if let workshop = self.workshop{
            return workshop.slots.count;
        }
        return 0;
    }
    
    override func collectionView(collectionView: UICollectionView, cellForItemAtIndexPath indexPath: NSIndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCellWithReuseIdentifier("videoCell", forIndexPath: indexPath) as UICollectionViewCell
        

        if let workshop = self.workshop{

            let slot = workshop.slots[indexPath.row];
            cell.backgroundView = UIImageView(image: UIImage(named: slot.pictureName));
            
        }

        // Configure the cell
        return cell
    }
    
    override func collectionView(collectionView: UICollectionView, didSelectItemAtIndexPath indexPath: NSIndexPath) {
        
        let name = workshop?.slots[indexPath.row].pictureName;
        
        let path = NSBundle.mainBundle().pathForResource(name, ofType:"mp4")
        let url = NSURL.fileURLWithPath(path!)
        moviePlayer = MPMoviePlayerViewController(contentURL: url)
        
        if let player = moviePlayer {
            presentMoviePlayerViewControllerAnimated(player)
        }
    
    }

}
