//
//  VideoViewController.swift
//  Workshop
//
//  Created by Anthony Soulier on 09/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class VideoViewController: UIViewController {
    

    override func viewDidLoad() {
        super.viewDidLoad()

        SIOSocket.socketWithHost("http://134.59.215.166:8080", response: { (socket: SIOSocket!) -> Void in
            
            
            socket.onConnect = {() -> Void in
                
                var alert = UIAlertController(title: "Alert", message: "Message", preferredStyle: UIAlertControllerStyle.Alert)
                alert.addAction(UIAlertAction(title: "OK", style: UIAlertActionStyle.Default, handler: nil))
                self.presentViewController(alert, animated: true, completion: nil);

                NSLog("Connection")

            }
            
            socket.onError = {(var errorInfo) -> Void in
                NSLog("Connection to server failed")
            }

            
        })
        
        // Do any additional setup after loading the view.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
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
