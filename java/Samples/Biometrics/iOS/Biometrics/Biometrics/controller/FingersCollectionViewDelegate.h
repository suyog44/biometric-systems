#import <Foundation/Foundation.h>
#import "CollectionDelegate.h"

@interface FingersCollectionViewDelegate : CollectionDelegate<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout>{
    NSArray *fingers;
}

@end
