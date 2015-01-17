//
//  VideoViewController.swift
//  Workshop
//
//  Created by Anthony Soulier on 09/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class VideoViewController: UICollectionViewController {
    
    private var socket : SIOSocket!
    private var socketIsConnected : Bool = false
    
    private var workshop : Workshop? {
        didSet {
            collectionView?.reloadData();
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
    }
    
    override func viewWillAppear(animated: Bool) {
        SIOSocket.socketWithHost("http://192.168.1.12:8080", response: { (socket: SIOSocket!) -> Void in
            
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
            
            self.socket.on("workshop", callback: { (mes :[AnyObject]!) -> Void in
                
                if let message = mes[0] as? [String:AnyObject]{
                    self.workshop = Workshop(fromJSON: message)
                }
            })
            
        })
    }
    
    override func viewWillDisappear(animated: Bool) {
        self.socket.close()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func refreshWorkshop(sender: AnyObject) {
        
        self.socket.emit("ask_for_workshop",args: ["left"])
        
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
        
        let cell = collectionView.dequeueReusableCellWithReuseIdentifier("videoCell", forIndexPath: indexPath) as ActionVideoCollectionViewCell
        
        
        if let workshop = self.workshop{
            cell.slot = workshop.slots[indexPath.row];
        }
        
        // Configure the cell
        return cell
    }
    
    override func collectionView(collectionView: UICollectionView, didSelectItemAtIndexPath indexPath: NSIndexPath) {
        if let cell = collectionView.cellForItemAtIndexPath(indexPath) as? ActionVideoCollectionViewCell{
            cell.launchVideo()
        }
    }
    
    override func prefersStatusBarHidden() -> Bool {
        return false
    }
    
}
