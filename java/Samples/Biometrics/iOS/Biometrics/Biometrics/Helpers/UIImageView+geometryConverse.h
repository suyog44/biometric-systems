#import <UIKit/UIKit.h>

@interface UIImageView (geometryConverse)

- (CGPoint)convertPointFromImage:(CGPoint)imagePoint;
- (CGRect)convertRectFromView:(CGRect)viewRect;
- (CGPoint)convertPointFromView:(CGPoint)viewPoint;
- (CGRect)convertRectFromImage:(CGRect)imageRect;

@end
