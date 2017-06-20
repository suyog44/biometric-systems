#import <Foundation/Foundation.h>
#import "CollectionDelegate.h"

@interface FacesCollectionViewDelegate : CollectionDelegate<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout>{
    NSArray *faces;
}
-(void)setupDelegateWithViewController:(UIViewController*)viewControllerArg;

@end
