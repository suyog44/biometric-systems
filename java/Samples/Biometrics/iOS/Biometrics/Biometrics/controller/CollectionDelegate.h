#import <Foundation/Foundation.h>
@class DatabaseController;

@interface CollectionDelegate : NSObject<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout>{
     DatabaseController *viewController;
}

-(void)setupDelegateWithViewController:(DatabaseController*)viewControllerArg;
-(HNSubject)getSubjectAtIndex:(int)index;
-(void)reloadData;

@end
