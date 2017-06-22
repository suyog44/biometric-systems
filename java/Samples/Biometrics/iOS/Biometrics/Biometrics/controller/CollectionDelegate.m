#import "CollectionDelegate.h"
#import "DatabaseController.h"

@implementation CollectionDelegate

- (void)setupDelegateWithViewController:(DatabaseController *)viewControllerArg{
    viewController = viewControllerArg;
}

- (HNSubject)getSubjectAtIndex:(int)index{
    return NULL;
}
- (void)reloadData{
    
}

//CollectionView

- (NSInteger)numberOfSectionsInCollectionView:(UICollectionView *)collectionView{
    return 0;
}
- (NSInteger)collectionView:(UICollectionView *)collectionView numberOfItemsInSection:(NSInteger)section{
    return 0;
}
-(UICollectionViewCell *)collectionView:(UICollectionView *)collectionViewArg cellForItemAtIndexPath:(NSIndexPath *)indexPath{
    return nil;
}

@end
