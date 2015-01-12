//
//  VideoViewController.swift
//  Workshop
//
//  Created by Anthony Soulier on 09/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class VideoViewController: UIViewController {
    
    var socket : SIOSocket!
    var socketIsConnected : Bool = false

    override func viewDidLoad() {
        super.viewDidLoad()

        SIOSocket.socketWithHost("http://134.59.214.247:8080", response: { (socket: SIOSocket!) -> Void in
            
            self.socket = socket
            
            self.socket.onConnect = {() -> Void in
                self.socketIsConnected = true
                NSLog("Connection")
            }
            
            socket.onError = {(var errorInfo) -> Void in
                NSLog("Connection to server failed")
            }

            socket.onDisconnect = {() -> Void in
                self.socketIsConnected = false
            }
            
            socket.on("change_vue", callback: { (mes: [AnyObject]!) -> Void in
                if let message = mes[0] as? String{
                    NSLog(message)
                }
            })
            
        })
        
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    @IBAction func emit(sender: UIButton) {
        
        socket.emit("isTable")
        
    }

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */

}
