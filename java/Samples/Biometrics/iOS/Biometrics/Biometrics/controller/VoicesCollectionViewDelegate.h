#import <Foundation/Foundation.h>
#import "CollectionDelegate.h"

@interface VoicesCollectionViewDelegate : CollectionDelegate<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout>{
    NSArray *voices;
}

@end
