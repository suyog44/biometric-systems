#import <Foundation/Foundation.h>

@interface UICollectionViewCell (typeEnum)

typedef enum {
    UICollectionViewCellTypeFirst,
    UICollectionViewCellTypeMiddle,
    UICollectionViewCellTypeLast,
    UICollectionViewCellTypeFirstAndLast
} UICollectionViewCellType;

@end
