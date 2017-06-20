#import "HeaderLayout.h"

@implementation HeaderLayout

- (NSArray *) layoutAttributesForElementsInRect:(CGRect)rect {
    
    NSMutableArray *ans = [[super layoutAttributesForElementsInRect:rect] mutableCopy];
    
    NSMutableIndexSet *missingSections = [NSMutableIndexSet indexSet];
    for (NSUInteger index=0; index<[ans count]; index++) {
        UICollectionViewLayoutAttributes *layoutAttributes = ans[index];
        
        if (layoutAttributes.representedElementCategory == UICollectionElementCategoryCell) {
            [missingSections addIndex:layoutAttributes.indexPath.section];
        }
        if ([layoutAttributes.representedElementKind isEqualToString:UICollectionElementKindSectionHeader]) {
            [ans removeObjectAtIndex:index];
            index--;
        }
    }
    

    [missingSections enumerateIndexesUsingBlock:^(NSUInteger index, BOOL *stop) {
        NSIndexPath *indexPath = [NSIndexPath indexPathForItem:0 inSection:index];
        UICollectionViewLayoutAttributes *layoutAttributes = [self layoutAttributesForSupplementaryViewOfKind:UICollectionElementKindSectionHeader atIndexPath:indexPath];
        layoutAttributes==nil?:[ans addObject:layoutAttributes];
    }];
    
    return ans;
}

- (UICollectionViewLayoutAttributes *)layoutAttributesForSupplementaryViewOfKind:(NSString *)kind atIndexPath:(NSIndexPath *)indexPath {
    UICollectionViewLayoutAttributes *attr = [super layoutAttributesForSupplementaryViewOfKind:kind atIndexPath:indexPath];
    if ([kind isEqualToString:UICollectionElementKindSectionHeader]) {
        UICollectionView * const collectionView = self.collectionView;
        UIEdgeInsets const contentEdge = collectionView.contentInset;
        CGPoint const contentOffset = CGPointMake(collectionView.contentOffset.x, collectionView.contentOffset.y + contentEdge.top);
        CGPoint nextHeaderOrigin = CGPointMake(INFINITY, INFINITY);
        
        if (indexPath.section+1 < [collectionView numberOfSections]) {
            UICollectionViewLayoutAttributes *nextHeaderAttributes = [super layoutAttributesForSupplementaryViewOfKind:kind atIndexPath:[NSIndexPath indexPathForItem:0 inSection:indexPath.section+1]];
            nextHeaderOrigin = nextHeaderAttributes.frame.origin;
        }
        
        CGRect frameRect = attr.frame;
        if (self.scrollDirection == UICollectionViewScrollDirectionVertical) {
            frameRect.origin.y = MIN(MAX(contentOffset.y, frameRect.origin.y), nextHeaderOrigin.y - CGRectGetHeight(frameRect));
        }
        else {
            frameRect.origin.x = MIN(MAX(contentOffset.x, frameRect.origin.x), nextHeaderOrigin.x - CGRectGetWidth(frameRect));
        }
        attr.zIndex = 1024;
        attr.frame = frameRect;
    }
    return attr;
}

- (UICollectionViewLayoutAttributes *)initialLayoutAttributesForAppearingSupplementaryElementOfKind:(NSString *)kind atIndexPath:(NSIndexPath *)indexPath {
    UICollectionViewLayoutAttributes *attr = [self layoutAttributesForSupplementaryViewOfKind:kind atIndexPath:indexPath];
    return attr;
}
- (UICollectionViewLayoutAttributes *)finalLayoutAttributesForDisappearingSupplementaryElementOfKind:(NSString *)kind atIndexPath:(NSIndexPath *)indexPath {
    UICollectionViewLayoutAttributes *attr = [self layoutAttributesForSupplementaryViewOfKind:kind atIndexPath:indexPath];
    return attr;
}

- (BOOL) shouldInvalidateLayoutForBoundsChange:(CGRect)newBound {
    return YES;
}

@end
