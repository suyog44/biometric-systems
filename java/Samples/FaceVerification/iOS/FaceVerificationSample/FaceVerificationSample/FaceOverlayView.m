// This is a private class of FaceView. Please, do not use it directly, use FaceView instead.

#import "FaceOverlayView.h"

@implementation FaceOverlayView

- (instancetype)initWithFrame:(CGRect)frame
{
    self = [super initWithFrame:frame];
    if (self) {
        self.backgroundColor = [UIColor clearColor];
        [self clear];
    }
    return self;
}

- (void)drawFaceBoundingRect {
    if (faceBoundingRect.size.width > 0 && faceBoundingRect.size.height > 0) {
        CGFloat imageScale = fminf(self.bounds.size.width/imageSize.width, self.bounds.size.height/imageSize.height);
        CGFloat x = (imageSize.width - faceBoundingRect.size.width - faceBoundingRect.origin.x) * imageScale;
        CGFloat y = faceBoundingRect.origin.y * imageScale;
        CGFloat width = faceBoundingRect.size.width * imageScale;
        CGFloat height = faceBoundingRect.size.height * imageScale;
        CGFloat xOffset = (self.bounds.size.width - (imageSize.width * imageScale)) * 0.5;
        CGFloat yOffset = (self.bounds.size.height - (imageSize.height * imageScale)) * 0.5;
        [[UIColor greenColor] setStroke];
        UIRectFrame(CGRectMake(xOffset + x, yOffset + y, width, height));
    }
}

- (void)drawText:(NSString*)text {
    NSMutableParagraphStyle *paragraphStyle = [[NSParagraphStyle defaultParagraphStyle] mutableCopy];
    paragraphStyle.alignment = NSTextAlignmentCenter;
    NSDictionary *attributes = @{
                                 NSFontAttributeName: [UIFont systemFontOfSize:16.0],
                                 NSParagraphStyleAttributeName: paragraphStyle,
                                 NSForegroundColorAttributeName: [UIColor blackColor],
                                 NSBackgroundColorAttributeName: [UIColor yellowColor]
                                };
    CGFloat bottomOffset = 30.0;
    [text drawInRect:CGRectMake(self.bounds.origin.x, self.bounds.size.height-bottomOffset, self.bounds.size.width, bottomOffset) withAttributes:attributes];
}

- (CGFloat)calcYawOffset:(NFloat)yaw {
    CGFloat scale = self.bounds.size.width/110.0;
    return yaw*scale;
}

- (void)drawBlink {
    [[UIColor yellowColor] setFill];
    UIBezierPath *path = [self getBlinkPath];
    [path applyTransform:CGAffineTransformMakeScale(0.25, 0.25)];
    CGFloat tx = self.bounds.size.width*0.5 - path.bounds.size.width;
    CGFloat ty = self.bounds.size.height - 70.0 - path.bounds.size.height*0.5;
    CGFloat xOffset = [self calcYawOffset:currentYaw];
    [path applyTransform:CGAffineTransformMakeTranslation(tx + xOffset, ty)];
    [path fill];
}

- (void)drawTarget {
    [[UIColor yellowColor] setStroke];
    UIBezierPath *path = [self getTargetPath];
    [path setLineWidth:1.5];
    [path applyTransform:CGAffineTransformMakeScale(0.4, 0.4)];
    CGFloat tx = (self.bounds.size.width - path.bounds.size.width) * 0.5;
    CGFloat ty = self.bounds.size.height - 70.0;
    CGFloat xOffset = [self calcYawOffset:livenessTargetYaw];
    [path applyTransform:CGAffineTransformMakeTranslation(tx + xOffset, ty)];
    [path stroke];
}

- (void)drawArrow {
    [[UIColor yellowColor] setFill];
    UIBezierPath *path = [self getArrowPath];
    CGFloat tx = 0.0;
    if (livenessTargetYaw < currentYaw) {
        [path applyTransform:CGAffineTransformMakeScale(-0.1, 0.1)];
        tx = self.bounds.size.width*0.5 + path.bounds.size.width*0.75;
    } else {
        [path applyTransform:CGAffineTransformMakeScale(0.1, 0.1)];
         tx = self.bounds.size.width*0.5 - path.bounds.size.width*0.75;
    }
    CGFloat ty = self.bounds.size.height - 70.0;
    CGFloat xOffset = [self calcYawOffset:currentYaw];
    [path applyTransform:CGAffineTransformMakeTranslation(tx + xOffset, ty)];
    [path fill];
}

- (void)drawRect:(CGRect)rect {
    [super drawRect:rect];
    [self drawFaceBoundingRect];
    if (livenessAction & nllaRotateYaw) {
        if (livenessAction & nllaBlink) {
            [self drawBlink];
            [self drawText:@"Blink"];
        } else {
            [self drawTarget];
            [self drawArrow];
            [self drawText:[NSString stringWithFormat:@"Turn face on target"]];
        }
    } else if (livenessAction & nllaBlink) {
        [self drawText:@"Blink"];
    } else if (livenessAction & nllaKeepStill) {
        [self drawText:[NSString stringWithFormat:@"Keep still, score: %u", livenessScore]];
    } else if (livenessAction & nllaKeepRotatingYaw) {
        [self drawText:@"Turn face from side to side"];
    }
}

- (void)clear {
    imageSize = (CGSize){0};
    currentYaw = 0.0;
    faceBoundingRect = (CGRect){0};
    livenessAction = nllaNone;
    livenessTargetYaw = 0;
    livenessScore = 0;
}

- (UIBezierPath*)getArrowPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(32.0, 322.0)];
    [path addLineToPoint:CGPointMake(31.0, 315.0)];
    [path addLineToPoint:CGPointMake(29.0, 308.0)];
    [path addLineToPoint:CGPointMake(34.0, 302.0)];
    [path addLineToPoint:CGPointMake(63.0, 259.0)];
    [path addLineToPoint:CGPointMake(92.0, 216.0)];
    [path addLineToPoint:CGPointMake(120.0, 173.0)];
    [path addLineToPoint:CGPointMake(90.0, 129.0)];
    [path addLineToPoint:CGPointMake(61.0, 84.0)];
    [path addLineToPoint:CGPointMake(31.0, 39.0)];
    [path addLineToPoint:CGPointMake(32.0, 35.0)];
    [path addLineToPoint:CGPointMake(30.0, 28.0)];
    [path addLineToPoint:CGPointMake(33.0, 25.0)];
    [path addLineToPoint:CGPointMake(40.0, 25.0)];
    [path addLineToPoint:CGPointMake(47.0, 22.0)];
    [path addLineToPoint:CGPointMake(53.0, 27.0)];
    [path addLineToPoint:CGPointMake(145.0, 73.0)];
    [path addLineToPoint:CGPointMake(236.0, 118.0)];
    [path addLineToPoint:CGPointMake(327.0, 165.0)];
    [path addLineToPoint:CGPointMake(337.0, 181.0)];
    [path addLineToPoint:CGPointMake(317.0, 187.0)];
    [path addLineToPoint:CGPointMake(306.0, 192.0)];
    [path addLineToPoint:CGPointMake(220.0, 236.0)];
    [path addLineToPoint:CGPointMake(134.0, 279.0)];
    [path addLineToPoint:CGPointMake(47.0, 322.0)];
    [path addLineToPoint:CGPointMake(42.0, 322.0)];
    [path addLineToPoint:CGPointMake(37.0, 323.0)];
    [path closePath];
    return path;
}

- (UIBezierPath*)getBlinkPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(135.0, 129.0)];
    [path addLineToPoint:CGPointMake(135.0, 126.0)];
    [path addLineToPoint:CGPointMake(135.0, 122.0)];
    [path addLineToPoint:CGPointMake(135.0, 119.0)];
    [path addLineToPoint:CGPointMake(131.0, 119.0)];
    [path addLineToPoint:CGPointMake(127.0, 118.0)];
    [path addLineToPoint:CGPointMake(122.0, 118.0)];
    [path addLineToPoint:CGPointMake(120.0, 123.0)];
    [path addLineToPoint:CGPointMake(119.0, 129.0)];
    [path addLineToPoint:CGPointMake(115.0, 133.0)];
    [path addLineToPoint:CGPointMake(111.0, 132.0)];
    [path addLineToPoint:CGPointMake(102.0, 132.0)];
    [path addLineToPoint:CGPointMake(103.0, 126.0)];
    [path addLineToPoint:CGPointMake(104.0, 122.0)];
    [path addLineToPoint:CGPointMake(107.0, 117.0)];
    [path addLineToPoint:CGPointMake(107.0, 112.0)];
    [path addLineToPoint:CGPointMake(103.0, 110.0)];
    [path addLineToPoint:CGPointMake(100.0, 108.0)];
    [path addLineToPoint:CGPointMake(97.0, 106.0)];
    [path addLineToPoint:CGPointMake(93.0, 110.0)];
    [path addLineToPoint:CGPointMake(90.0, 115.0)];
    [path addLineToPoint:CGPointMake(85.0, 118.0)];
    [path addLineToPoint:CGPointMake(81.0, 116.0)];
    [path addLineToPoint:CGPointMake(71.0, 111.0)];
    [path addLineToPoint:CGPointMake(77.0, 106.0)];
    [path addLineToPoint:CGPointMake(79.0, 102.0)];
    [path addLineToPoint:CGPointMake(87.0, 97.0)];
    [path addLineToPoint:CGPointMake(81.0, 92.0)];
    [path addLineToPoint:CGPointMake(79.0, 88.0)];
    [path addLineToPoint:CGPointMake(73.0, 85.0)];
    [path addLineToPoint:CGPointMake(74.0, 80.0)];
    [path addLineToPoint:CGPointMake(78.0, 76.0)];
    [path addLineToPoint:CGPointMake(84.0, 73.0)];
    [path addLineToPoint:CGPointMake(90.0, 75.0)];
    [path addLineToPoint:CGPointMake(97.0, 78.0)];
    [path addLineToPoint:CGPointMake(101.0, 85.0)];
    [path addLineToPoint:CGPointMake(108.0, 90.0)];
    [path addLineToPoint:CGPointMake(124.0, 101.0)];
    [path addLineToPoint:CGPointMake(147.0, 103.0)];
    [path addLineToPoint:CGPointMake(166.0, 96.0)];
    [path addLineToPoint:CGPointMake(176.0, 92.0)];
    [path addLineToPoint:CGPointMake(183.0, 85.0)];
    [path addLineToPoint:CGPointMake(191.0, 78.0)];
    [path addLineToPoint:CGPointMake(195.0, 74.0)];
    [path addLineToPoint:CGPointMake(201.0, 73.0)];
    [path addLineToPoint:CGPointMake(205.0, 76.0)];
    [path addLineToPoint:CGPointMake(210.0, 78.0)];
    [path addLineToPoint:CGPointMake(213.0, 82.0)];
    [path addLineToPoint:CGPointMake(208.0, 86.0)];
    [path addLineToPoint:CGPointMake(206.0, 89.0)];
    [path addLineToPoint:CGPointMake(203.0, 93.0)];
    [path addLineToPoint:CGPointMake(200.0, 96.0)];
    [path addLineToPoint:CGPointMake(204.0, 101.0)];
    [path addLineToPoint:CGPointMake(208.0, 105.0)];
    [path addLineToPoint:CGPointMake(211.0, 110.0)];
    [path addLineToPoint:CGPointMake(208.0, 113.0)];
    [path addLineToPoint:CGPointMake(202.0, 123.0)];
    [path addLineToPoint:CGPointMake(198.0, 116.0)];
    [path addLineToPoint:CGPointMake(194.0, 113.0)];
    [path addLineToPoint:CGPointMake(192.0, 106.0)];
    [path addLineToPoint:CGPointMake(187.0, 107.0)];
    [path addLineToPoint:CGPointMake(182.0, 110.0)];
    [path addLineToPoint:CGPointMake(175.0, 113.0)];
    [path addLineToPoint:CGPointMake(180.0, 119.0)];
    [path addLineToPoint:CGPointMake(182.0, 123.0)];
    [path addLineToPoint:CGPointMake(186.0, 130.0)];
    [path addLineToPoint:CGPointMake(179.0, 132.0)];
    [path addLineToPoint:CGPointMake(175.0, 133.0)];
    [path addLineToPoint:CGPointMake(169.0, 136.0)];
    [path addLineToPoint:CGPointMake(168.0, 130.0)];
    [path addLineToPoint:CGPointMake(166.0, 126.0)];
    [path addLineToPoint:CGPointMake(166.0, 118.0)];
    [path addLineToPoint:CGPointMake(161.0, 118.0)];
    [path addLineToPoint:CGPointMake(158.0, 119.0)];
    [path addLineToPoint:CGPointMake(154.0, 119.0)];
    [path addLineToPoint:CGPointMake(150.0, 119.0)];
    [path addLineToPoint:CGPointMake(150.0, 125.0)];
    [path addLineToPoint:CGPointMake(150.0, 132.0)];
    [path addLineToPoint:CGPointMake(150.0, 138.0)];
    [path addLineToPoint:CGPointMake(145.0, 138.0)];
    [path addLineToPoint:CGPointMake(140.0, 138.0)];
    [path addLineToPoint:CGPointMake(135.0, 138.0)];
    [path addLineToPoint:CGPointMake(135.0, 135.0)];
    [path addLineToPoint:CGPointMake(135.0, 132.0)];
    [path closePath];
    return path;
}

- (UIBezierPath*)getTargetPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:40.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    [path moveToPoint:CGPointMake(70.0, 40.0)];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:30.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    [path moveToPoint:CGPointMake(60.0, 40.0)];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:20.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    [path moveToPoint:CGPointMake(50.0, 40.0)];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:10.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    return path;
}

@end
