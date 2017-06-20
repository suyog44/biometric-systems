#import <Foundation/Foundation.h>
#import "CollectionDelegate.h"

@interface EyesCollectionViewDelegate : CollectionDelegate<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout>{
    NSArray *eyes;
}

@end
