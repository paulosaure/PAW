//
//  SocketManager.swift
//  Workshop
//
//  Created by Anthony Soulier on 15/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import Foundation

private let _singletonInstance = SocketSingleton()

class SocketSingleton {
    
    class var sharedInstance: SocketSingleton {
        return _singletonInstance
    }
    
    
    var socket: SIOSocket = SIOSocket()
    var isConnected : Bool = false
    
    
    func connectToHost(host :String){
        
        if(!self.isConnected){
            SIOSocket.socketWithHost(host, response: { (socket: SIOSocket!) -> Void in
                
                self.socket = socket
                
                self.socket.onConnect = {() -> Void in
                    self.isConnected = true
                }
                
                self.socket.onError = {(var errorInfo) -> Void in
                    println("Connection to server failed")
                }
                
                self.socket.onDisconnect = {() -> Void in
                    self.isConnected = false
                }
                
              
                
            })
        }
        
        
    }
}
