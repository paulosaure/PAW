//
//  ViewController.swift
//  Workshop
//
//  Created by Anthony Soulier on 04/01/2015.
//  Copyright (c) 2015 Anthony Soulier. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    @IBOutlet weak var friezeCollectionView: UICollectionView!
    @IBOutlet weak var actionCollectionView: UICollectionView!
    
    @IBOutlet weak var actionCollectionViewCellHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var friezeCollectionViewCellHeightConstraint: NSLayoutConstraint!
    
    
    var workshop : Workshop?{
        didSet{
            actionCollectionView.reloadData()
            friezeCollectionView.reloadData()
        }
    }
    
    private var socket : SIOSocket!
    private var socketIsConnected : Bool = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        actionCollectionView.backgroundColor = UIColor.clearColor()
        friezeCollectionView.backgroundColor = UIColor.clearColor()
        // Do any additional setup after loading the view, typically from a nib.
    }
    
    override func viewWillAppear(animated: Bool) {
        invalidateCollectionViewLayout()
        
        SIOSocket.socketWithHost("http://192.168.1.6:8080", response: { (socket: SIOSocket!) -> Void in
            
            self.socket = socket
            
            self.socket.onConnect = {() -> Void in
                self.socketIsConnected = true
                self.socket.emit("ask_for_workshop")
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
                    self.workshop?.slots.shuffle()
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
    
    override func willRotateToInterfaceOrientation(toInterfaceOrientation: UIInterfaceOrientation, duration: NSTimeInterval) {
        invalidateCollectionViewLayout()
    }
    
    func invalidateCollectionViewLayout() {
        friezeCollectionView.collectionViewLayout.invalidateLayout()
        actionCollectionView.collectionViewLayout.invalidateLayout()
    }
    
    override func prefersStatusBarHidden() -> Bool {
        return false
    }
    
    /*
    @IBAction func scanQRCode(sender: AnyObject) {
        
        let reader = QRCodeReaderViewController()
        
        reader.modalPresentationStyle = UIModalPresentationStyle.FormSheet
       // reader.delegate = self
        reader.setCompletionWithBlock { (result: String!) -> Void in
            self.dismissViewControllerAnimated(true, completion: { () -> Void in
                println(result)
            })
        }
        
        self.presentViewController(reader, animated: true, completion: nil)
        
    
        QRCodeReaderViewController *reader = [QRCodeReaderViewController new];
        reader.modalPresentationStyle      = UIModalPresentationFormSheet;
        
        // Using delegate methods
        reader.delegate                    = self;
        
        // Or by using blocks
        [reader setCompletionWithBlock:^(NSString *resultAsString) {
            [self dismissViewControllerAnimated:YES completion:^{
            NSLog(@"%@", result);
            }];
            }];
        
        [self presentViewController:reader animated:YES completion:NULL];

    }

*/
    
}

extension ViewController: UICollectionViewDataSource {
    
    func collectionView(collectionView: UICollectionView, cellForItemAtIndexPath indexPath: NSIndexPath) -> UICollectionViewCell {
        
        let identifier = (collectionView.isEqual(friezeCollectionView)) ? "friezeCell" : "actionCell"
        
        let cell : SlotCollectionViewCell = collectionView.dequeueReusableCellWithReuseIdentifier(identifier, forIndexPath: indexPath) as SlotCollectionViewCell
        
        
        cell.layer.borderWidth = 3.0
        cell.layer.borderColor = UIColor.lightGrayColor().CGColor
        cell.layer.cornerRadius = 5.0
        
        if(collectionView.isEqual(friezeCollectionView)){
            cell.dropZoneHandler = self
            cell.slot = Slot(index: indexPath.row + 1, withPictureName: "")
        }
        else{
            
            if let slot = workshop?.slots[indexPath.row] {
                cell.slot = slot
            }
            else{
                cell.backgroundColor = UIColor.whiteColor()
            }
            
            let dragDropManager = OBDragDropManager.sharedManager()
            let panRecognizer = dragDropManager.createDragDropGestureRecognizerWithClass(UIPanGestureRecognizer.self, source: self)
            cell.addGestureRecognizer(panRecognizer)
            
        }
        return cell;
    }
    
    func collectionView(collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        if let workshop = self.workshop {
            return workshop.slots.count
        }
        return 0
    }
    
    func numberOfSectionsInCollectionView(collectionView: UICollectionView) -> Int {
        return 1
    }
}

extension ViewController: UICollectionViewDelegate {
    // scroll view delegate methods
    
}

extension ViewController : UICollectionViewDelegateFlowLayout{
    
    func collectionView(collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAtIndexPath indexPath: NSIndexPath) -> CGSize {
        
        let numberOfSlot = workshop?.slots.count
        
        let width = (collectionView.frame.size.width - CGFloat(numberOfSlot!-1) * 5) / CGFloat(numberOfSlot!)
        
        actionCollectionViewCellHeightConstraint.constant = width //+ navigationController!.navigationBar.frame.height + 10
        
        friezeCollectionViewCellHeightConstraint.constant = width //+ navigationController!.navigationBar.frame.height + 25
        
        return CGSizeMake(width, width)
    }
    
    func collectionView(collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAtIndex section: Int) -> CGFloat {
        return 5.0
    }
}
extension ViewController : OBDropZone{
    
    func ovumEntered(ovum: OBOvum!, inView view: UIView!, atLocation location: CGPoint) -> OBDropAction {
        
        if let cell = view as? SlotCollectionViewCell {
            
            if let fromView = ovum.dataObject as? SlotCollectionViewCell  {

                cell.layer.borderWidth = 5.0
                cell.layer.borderColor = UIColor.redColor().CGColor
                
                if cell.slot.index == fromView.slot.index{
                    cell.layer.borderColor = UIColor.greenColor().CGColor
                    return OBDropActionMove
                }
            }
        }
        return OBDropActionNone
    }
    
    func ovumExited(ovum: OBOvum!, inView view: UIView!, atLocation location: CGPoint) {
        if let cell = view as? UICollectionViewCell{
            cell.layer.borderWidth = 3.0
            cell.layer.borderColor = UIColor.lightGrayColor().CGColor
            cell.layer.cornerRadius = 5.0
            
        }
    }
    
    func ovumDropped(ovum: OBOvum!, inView view: UIView!, atLocation location: CGPoint) {
        
        
        if let cell = ovum.dataObject as? SlotCollectionViewCell {
            
            if var dropView = view as? SlotCollectionViewCell{
                dropView.slot = cell.slot
                dropView.layer.borderWidth = 0
                dropView.layer.borderColor = UIColor.clearColor().CGColor
                
                //on vient de remplir la "dropzone", qui devient donc non dropable
                dropView.dropZoneHandler = nil
                cell.removeFromSuperview()
            }
        }
    }
}

extension ViewController : OBOvumSource{
    
    func createOvumFromView(sourceView: UIView!) -> OBOvum! {
        let ovum = OBOvum()
        ovum.dataObject = sourceView
        return ovum
    }
    
    func createDragRepresentationOfSourceView(sourceView: UIView!, inWindow window: UIWindow!) -> UIView! {
        
        if let cell = sourceView as? SlotCollectionViewCell{
            
            var frame = sourceView.convertRect(sourceView.bounds, toView:sourceView.window)
            frame = window.convertRect(frame, fromWindow:sourceView.window);
            
            var view = SlotView(frame: frame.rectByInsetting(dx: 5, dy: 5))
            view.slot = cell.slot
            view.layer.cornerRadius = 5
            view.alpha = 0.7
            
            return view
        }
        return nil
    }
}

extension UIColor{
    
    class func randomColor() -> UIColor{
        var randomRed:CGFloat = CGFloat(drand48())
        var randomGreen:CGFloat = CGFloat(drand48())
        var randomBlue:CGFloat = CGFloat(drand48())
        
        return UIColor(red: randomRed, green: randomGreen, blue: randomBlue, alpha: 1.0)
        
    }
}

