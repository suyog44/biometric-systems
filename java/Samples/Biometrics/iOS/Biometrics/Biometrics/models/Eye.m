#import "Eye.h"

@implementation Eye

-(void)retrieveDataFromSubject{
    [super retrieveDataFromSubject];
    HNEAttributes attributes = NULL;
    HNIris iris = NULL;
    innerBoundaryPoints = [NSMutableArray array];
    outerBoundaryPoints = [NSMutableArray array];
    int num;
    
    @try {
        NSubjectGetIris(self.subject, 0, &iris);
        NIrisGetObject(iris, 0, &attributes);
    }
    @catch (NSException *exception) {
        return;
    }
    @finally {
    }
    
    
    @try {
        NRESULT_CHECK(NEAttributesGetInnerBoundaryPointCount(attributes, &num));
        for (int i = 0; i < num; i++){
            NPoint point;
            NRESULT_CHECK(NEAttributesGetInnerBoundaryPoint(attributes, i, &point));
            [innerBoundaryPoints addObject:[NSValue valueWithCGPoint:CGPointMake(point.X, point.Y)]];
        }
    }
    @catch (NSException *exception) {
    }
    @finally {
    }

    
    @try {
        NRESULT_CHECK(NEAttributesGetOuterBoundaryPointCount(attributes, &num));
        for (int i = 0; i < num; i++){
            NPoint point;
            NRESULT_CHECK(NEAttributesGetOuterBoundaryPoint(attributes, i, &point));
            [outerBoundaryPoints addObject:[NSValue valueWithCGPoint:CGPointMake(point.X, point.Y)]];
        }
    }
    @catch (NSException *exception) {    }
    @finally {
    }
    
}
-(UIImage*)getImageWithAttributesOnImage:(UIImage*)originalImage{
    UIColor *lineColor = [UIColor getDrawLineColor];

    UIGraphicsBeginImageContext(originalImage.size);

    // Pass 1: Draw the original image as the background
    [originalImage drawAtPoint:CGPointMake(0,0)];

    // Pass 2: Draw the line on top of original image
    CGContextRef context = UIGraphicsGetCurrentContext();
    CGContextSetStrokeColorWithColor(context, [lineColor CGColor]);
    CGContextSetLineWidth(context, 2.0);
    
    if (innerBoundaryPoints.count > 1){
        CGPoint firstPoint = [[innerBoundaryPoints firstObject] CGPointValue];
        CGPoint lastPoint = firstPoint;
        for (int i = 1; i < innerBoundaryPoints.count; i++){
            CGPoint currPoint = [[innerBoundaryPoints objectAtIndex:i] CGPointValue];
            CGContextMoveToPoint(context, lastPoint.x, lastPoint.y);
            CGContextAddLineToPoint(context, currPoint.x, currPoint.y);
            lastPoint = currPoint;
        }
        CGContextMoveToPoint(context, lastPoint.x, lastPoint.y);
        CGContextAddLineToPoint(context, firstPoint.x, firstPoint.y);
        CGContextStrokePath(context);
    }
    
    if (outerBoundaryPoints.count > 1){
        CGPoint firstPoint = [[outerBoundaryPoints firstObject] CGPointValue];
        CGPoint lastPoint = firstPoint;
        for (int i = 1; i < outerBoundaryPoints.count; i++){
            CGPoint currPoint = [[outerBoundaryPoints objectAtIndex:i] CGPointValue];
            CGContextMoveToPoint(context, lastPoint.x, lastPoint.y);
            CGContextAddLineToPoint(context, currPoint.x, currPoint.y);
            lastPoint = currPoint;
        }
        CGContextMoveToPoint(context, lastPoint.x, lastPoint.y);
        CGContextAddLineToPoint(context, firstPoint.x, firstPoint.y);
        CGContextStrokePath(context);
    }

    // Create new image
    UIImage *newImage = UIGraphicsGetImageFromCurrentImageContext();

    // Tidy up
    UIGraphicsEndImageContext();
    return newImage;
}

@end
