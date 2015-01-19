//
//  QRCodeReaderViewController.swift
//  Workshop
//
//  Created by Anthony Soulier on 17/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit
import AVFoundation

class QRCodeReaderViewController: UIViewController {
    
    
    @IBOutlet weak var toolbar: UIToolbar!
    
    var qrCodeFrameView:UIView?
    var captureSession:AVCaptureSession?
    var videoPreviewLayer:AVCaptureVideoPreviewLayer?
    
    //socket.io
    private var socket : SIOSocket!
    private var socketIsConnected : Bool = false
    
    override func viewDidLoad() {
        
        
        // Get an instance of the AVCaptureDevice class to initialize a device object and provide the video
        // as the media type parameter.
        let captureDevice = AVCaptureDevice.defaultDeviceWithMediaType(AVMediaTypeVideo)
        
        // Get an instance of the AVCaptureDeviceInput class using the previous device object.
        var error:NSError?
        let input: AnyObject! = AVCaptureDeviceInput.deviceInputWithDevice(captureDevice, error: &error)
        
        if (error != nil) {
            // If any error occurs, simply log the description of it and don't continue any more.
            println("\(error?.localizedDescription)")
            return
        }
        
        // Initialize the captureSession object.
        captureSession = AVCaptureSession()
        // Set the input device on the capture session.
        captureSession?.addInput(input as AVCaptureInput)
        
        // Initialize a AVCaptureMetadataOutput object and set it as the output device to the capture session.
        let captureMetadataOutput = AVCaptureMetadataOutput()
        captureSession?.addOutput(captureMetadataOutput)
        
        // Set delegate and use the default dispatch queue to execute the call back
        captureMetadataOutput.setMetadataObjectsDelegate(self, queue: dispatch_get_main_queue())
        captureMetadataOutput.metadataObjectTypes = [AVMetadataObjectTypeQRCode]
        
        // Initialize the video preview layer and add it as a sublayer to the viewPreview view's layer.
        videoPreviewLayer = AVCaptureVideoPreviewLayer(session: captureSession)
        videoPreviewLayer?.videoGravity = AVLayerVideoGravityResizeAspectFill
        
        videoPreviewLayer?.frame = view.layer.bounds
        view.layer.addSublayer(videoPreviewLayer)
        
        // Start video capture.
        captureSession?.startRunning()
        
        // Move the message label to the top view
        //view.bringSubviewToFront(messageLabel)
        
        // Initialize QR Code Frame to highlight the QR code
        // Initialize QR Code Frame to highlight the QR code
        qrCodeFrameView = UIView()
        qrCodeFrameView?.layer.borderColor = UIColor.greenColor().CGColor
        qrCodeFrameView?.layer.borderWidth = 2
        view.addSubview(qrCodeFrameView!)
        view.bringSubviewToFront(qrCodeFrameView!)
        
        view.bringSubviewToFront(toolbar)
        
    }
    
    @IBAction func cancelTap(sender: AnyObject) {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
    
    override func willRotateToInterfaceOrientation(toInterfaceOrientation: UIInterfaceOrientation, duration: NSTimeInterval) {
        switch(toInterfaceOrientation){
        case .Portrait:
            videoPreviewLayer?.connection.videoOrientation = AVCaptureVideoOrientation.Portrait
        case .LandscapeLeft:
            videoPreviewLayer?.connection.videoOrientation = AVCaptureVideoOrientation.LandscapeLeft
        case .LandscapeRight:
            videoPreviewLayer?.connection.videoOrientation = AVCaptureVideoOrientation.LandscapeRight
        case .PortraitUpsideDown:
            videoPreviewLayer?.connection.videoOrientation = AVCaptureVideoOrientation.PortraitUpsideDown
        case .Unknown:
            videoPreviewLayer?.connection.videoOrientation = AVCaptureVideoOrientation.Portrait
        }
        
        view.setNeedsDisplay()
    }
    
    override func viewWillLayoutSubviews() {
        videoPreviewLayer?.frame = view.bounds
    }
    
    
    
}
extension QRCodeReaderViewController : AVCaptureMetadataOutputObjectsDelegate{
    
    func captureOutput(captureOutput: AVCaptureOutput!, didOutputMetadataObjects metadataObjects: [AnyObject]!, fromConnection connection: AVCaptureConnection!) {
        // Check if the metadataObjects array is not nil and it contains at least one object.
        if metadataObjects == nil || metadataObjects.count == 0 {
            qrCodeFrameView?.frame = CGRectZero
            
            return
        }
        
        // Get the metadata object.
        let metadataObj = metadataObjects[0] as AVMetadataMachineReadableCodeObject
        
        // Here we use filter method to check if the type of metadataObj is supported
        // Instead of hardcoding the AVMetadataObjectTypeQRCode, we check if the type
        // can be found in the array of supported bar codes.
        if metadataObj.type == AVMetadataObjectTypeQRCode {
            // If the found metadata is equal to the QR code metadata then update the status label's text and set the bounds
            let barCodeObject = videoPreviewLayer?.transformedMetadataObjectForMetadataObject(metadataObj as AVMetadataMachineReadableCodeObject) as AVMetadataMachineReadableCodeObject
            qrCodeFrameView?.frame = barCodeObject.bounds
            
            if let message = metadataObj.stringValue {
                
                let json = JSON(string:message)
                let host = json["host"].asString
                let side = json["side"].asString
                
                if host != nil && side != nil {
                    connectToServer(host!, side: side!)
                    captureSession?.stopRunning()
                }
            }
        }
    }
    
    func connectToServer(host : String!, side :String!){
        SIOSocket.socketWithHost(host, response: { (socket: SIOSocket!) -> Void in
            
            self.socket = socket
            
            self.socket.onConnect = {() -> Void in
                self.socketIsConnected = true
                self.socket.emit("ask_for_workshop", args: [side])
            }
            
            self.socket.onError = {(var errorInfo) -> Void in
                println("Connection to server failed")
            }
            
            self.socket.onDisconnect = {() -> Void in
                self.socketIsConnected = false
            }
            
            self.socket.on("workshop", callback: { (mes :[AnyObject]!) -> Void in
                if let message = mes[0] as? [String:AnyObject]{
                
                    println(Workshop(fromJSON: message))
                    self.dismissViewControllerAnimated(true, completion: nil)
                }
            })
            
        })
    }
    
}
