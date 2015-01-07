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
    
    let numberOfItem = 6
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    override func willRotateToInterfaceOrientation(toInterfaceOrientation: UIInterfaceOrientation, duration: NSTimeInterval) {
        friezeCollectionView.collectionViewLayout.invalidateLayout()
        actionCollectionView.collectionViewLayout.invalidateLayout()
    }
    
}

extension ViewController: UICollectionViewDataSource {

    func collectionView(collectionView: UICollectionView, cellForItemAtIndexPath indexPath: NSIndexPath) -> UICollectionViewCell {
        
        let identifier = (collectionView.isEqual(friezeCollectionView)) ? "friezeCell" : "actionCell"
        
        let cell : UICollectionViewCell = collectionView.dequeueReusableCellWithReuseIdentifier(identifier, forIndexPath: indexPath) as UICollectionViewCell
        
        if(collectionView.isEqual(friezeCollectionView)){
            cell.dropZoneHandler = self
        }
        else{
            cell.backgroundColor = UIColor.randomColor()
            
            let dragDropManager = OBDragDropManager.sharedManager()
            let panRecognizer = dragDropManager.createDragDropGestureRecognizerWithClass(UIPanGestureRecognizer.self, source: self)
            cell.addGestureRecognizer(panRecognizer)
        }
        
        return cell;
    }
    
    func collectionView(collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return numberOfItem
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
    
        let width = (collectionView.frame.size.width - CGFloat(numberOfItem-1) * 5) / CGFloat(numberOfItem)
        collectionView.frame.size.height = width
        
        return CGSizeMake(width, width)
        
    }
    
    func collectionView(collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAtIndex section: Int) -> CGFloat {
        return 5.0
    }
}
extension ViewController : OBDropZone{
    
    func ovumEntered(ovum: OBOvum!, inView view: UIView!, atLocation location: CGPoint) -> OBDropAction {
        if let cell = view as? UICollectionViewCell {
            cell.layer.borderWidth = 2.0
            cell.layer.borderColor = UIColor.redColor().CGColor
        }
        return OBDropActionMove
    }
    
    func ovumExited(ovum: OBOvum!, inView view: UIView!, atLocation location: CGPoint) {
        if let cell = view as? UICollectionViewCell{
            cell.layer.borderWidth = 0
            cell.layer.borderColor = UIColor.clearColor().CGColor
            view.backgroundColor = UIColor.clearColor()
        }
    }
    
    func ovumDropped(ovum: OBOvum!, inView view: UIView!, atLocation location: CGPoint) {
       
        if let cell = ovum.dataObject as? UICollectionViewCell {
            
            view.backgroundColor = cell.backgroundColor
            view.layer.borderWidth = 0
            view.layer.borderColor = UIColor.clearColor().CGColor

            //on vient de remplir la "dropzone", qui devient donc non dropable
            view.dropZoneHandler = nil
            cell.removeFromSuperview()
        }
    }
    
}

extension ViewController : OBOvumSource{
    
    func createOvumFromView(sourceView: UIView!) -> OBOvum! {
        let ovum = OBOvum()
        ovum.dataObject = sourceView
        return ovum
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

