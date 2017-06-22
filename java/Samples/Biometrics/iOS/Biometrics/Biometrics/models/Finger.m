#import "Finger.h"
#define MINUTIA_RADIUS 8
#define MINUTIA_LINE_LENGTH 10
#define CORE_LINE_LENGTH 15
#define DELTA_LINE_LENGTH 15

/** Degrees to Radian **/
#define biometricDegreesToRadians( degrees ) ( ( degrees ) / 128.0 * M_PI )
#define correctDrawParamaterForImageResolution(drawParameter, size) ((size.width+size.height)/600 * drawParameter)

@implementation Finger

-(void)retrieveDataFromSubject{
    [super retrieveDataFromSubject];
}

-(UIImage*)getImageWithAttributesOnImage:(UIImage*)originalImage{
    UIColor *lineColor = [UIColor getDrawLineColor];
    
    UIGraphicsBeginImageContext(originalImage.size);
    
    [originalImage drawAtPoint:CGPointMake(0,0)];
    
    CGContextRef context = UIGraphicsGetCurrentContext();
    CGContextSetStrokeColorWithColor(context, [lineColor CGColor]);
    CGContextSetLineWidth(context, 2.0);
    
    HNFrictionRidge finger = NULL;
    HNFAttributes attributes = NULL;
    HNFRecord record = NULL;
    int num = 0;
    
    @try {
        NRESULT_CHECK(NSubjectGetFinger(self.subject, 0, &finger));
        NRESULT_CHECK(NFrictionRidgeGetObject(finger, 0, &attributes));
        NRESULT_CHECK(NFAttributesGetTemplate(attributes, &record));

        NRESULT_CHECK(NFRecordGetMinutiaCount(record, &num));
        NSLog(@"minutias count%i", num);
        for (int i = 0; i < num; i++){
            NFMinutia minutia;
            NRESULT_CHECK(NFRecordGetMinutia(record, i, &minutia));
            [self drawMinutia:minutia inContext:context withSize:originalImage.size];
        }

        CGContextStrokePath(context);

        NRESULT_CHECK(NFRecordGetCoreCount(record, &num));
        NSLog(@"cores count%i", num);
        for (int i = 0; i < num; i++){
            NFCore core;
            NRESULT_CHECK(NFRecordGetCore(record, i, &core));
            [self drawCore :core inContext:context withSize:originalImage.size];
        }

        CGContextStrokePath(context);

        NRESULT_CHECK(NFRecordGetDeltaCount(record, &num));
        NSLog(@"deltas count%i", num);
        for (int i = 0; i < num; i++){
            NFDelta delta;
            NRESULT_CHECK(NFRecordGetDelta(record, i, &delta));
            [self drawDelta:delta inContext:context withSize:originalImage.size];
        }

        CGContextStrokePath(context);

        NRESULT_CHECK(NFRecordGetDoubleCoreCount(record, &num));
        NSLog(@"double cores count%i", num);
        for (int i = 0; i < num; i++){
            NFDoubleCore doubleCore;
            NRESULT_CHECK(NFRecordGetDoubleCore(record, i, &doubleCore));
            [self drawDoubleCore:doubleCore inContext:context withSize:originalImage.size];
        }
        
        CGContextStrokePath(context);
    }
    @catch (NSException *exception) {
    }
    @finally {
        NObjectSet(NULL, &record);
        NObjectSet(NULL, &attributes);
        NObjectSet(NULL, &finger);
    }
    
    // Create new image
    UIImage *newImage = UIGraphicsGetImageFromCurrentImageContext();
    
    // Tidy up
    UIGraphicsEndImageContext();
    return newImage;
}

-(void)drawMinutia:(NFMinutia)minutia inContext:(CGContextRef)context withSize:(CGSize)sizeArg{
    float S = correctDrawParamaterForImageResolution(MINUTIA_RADIUS, sizeArg);
    float SHalf = S / 2;
    float L = correctDrawParamaterForImageResolution(MINUTIA_LINE_LENGTH, sizeArg);
    
    float x = minutia.X;
    float y = minutia.Y;
    
    CGContextAddEllipseInRect(context, CGRectMake(x-SHalf, y-SHalf, S, S));
    NSLog(@"angle:%hhu", minutia.Angle);
    double angle = minutia.Angle;
    if (minutia.Type == nfmtBifurcation)
    {
        float dx = (float)(L * cos(biometricDegreesToRadians(angle-12.0f)));
        float dy = (float)(L * sin(biometricDegreesToRadians(angle-12.0f)));
        CGContextMoveToPoint(context, x, y);
        CGContextAddLineToPoint(context, x+dx, y+dy);
        dx = (float)(L * cos(biometricDegreesToRadians(angle+12.0f)));
        dy = (float)(L * sin(biometricDegreesToRadians(angle+12.0f)));
        CGContextMoveToPoint(context, x, y);
        CGContextAddLineToPoint(context, x+dx, y+dy);
    }
    else
    {
        float dx = (float)(L * cos(biometricDegreesToRadians(angle)));
        float dy = (float)(L * sin(biometricDegreesToRadians(angle)));
        CGContextMoveToPoint(context, x, y);
        CGContextAddLineToPoint(context, x+dx, y+dy);
    }
}

-(void)drawCore:(NFCore)core inContext:(CGContextRef)context withSize:(CGSize)sizeArg{
    float S = correctDrawParamaterForImageResolution(CORE_LINE_LENGTH, sizeArg) / 2;
    float x = core.X;
    float y = core.Y;
    
    float s1 = (int)round(S);
    int size = 2 * s1;
    
    
    CGContextAddRect(context, CGRectMake(x-s1, y-s1, size, size));
    

    double angle = core.Angle;
//    if ((core.RawAngle != -1) && (!Double.IsNaN(angle)))
    {
        float dx = (float)(S * 2 * cos(biometricDegreesToRadians(angle)));
        float dy = (float)(S * 2 * sin(biometricDegreesToRadians(angle)));
        CGContextMoveToPoint(context, x, y);
        CGContextAddLineToPoint(context, x+dx, y+dy);
    }
}

-(void)drawDelta:(NFDelta)delta inContext:(CGContextRef)context withSize:(CGSize)sizeArg{
    const float S = correctDrawParamaterForImageResolution(DELTA_LINE_LENGTH, sizeArg) / 2;
    float x = delta.X;
    float y = delta.Y;
    
    float s1 = (int)round(S);
    
    int z = (int)round((s1 * sqrt(3)) / 2);
    int r = s1 / 2;
    
    int x1 = (int)round(x);
    int y1 = (int)round(y);
    
    
    CGPoint p1 = CGPointMake(x1, y1-s1);
    CGPoint p2 = CGPointMake(x1-z, y1+r);
    CGPoint p3 = CGPointMake(x1+z, y1+r);
    
    
    CGContextMoveToPoint(context, p1.x, p1.y);
    CGContextAddLineToPoint(context, p2.x, p2.y);
    CGContextAddLineToPoint(context, p3.x, p3.y);
    CGContextAddLineToPoint(context, p1.x, p1.y);
    CGContextAddLineToPoint(context, p2.x, p2.y);

    
    double angle = delta.Angle1;
    float dy;
    float dx;
//    if ((delta.RawAngle1 != -1) && (!Double.IsNaN(angle)))
    {
        dx = (float)(S * 2 * cos(biometricDegreesToRadians(angle)));
        dy = (float)(S * 2 * sin(biometricDegreesToRadians(angle)));
        CGContextMoveToPoint(context, x, y);
        CGContextAddLineToPoint(context, x+dx, y+dy);
    }
    angle = delta.Angle2;
//    if ((delta.RawAngle2 != -1) && (!Double.IsNaN(angle)))
    {
        dx = (float)(S * 2 * cos(biometricDegreesToRadians(angle)));
        dy = (float)(S * 2 * sin(biometricDegreesToRadians(angle)));
        CGContextMoveToPoint(context, x, y);
        CGContextAddLineToPoint(context, x+dx, y+dy);
    }
    angle = delta.Angle3;
//    if ((delta.RawAngle3 != -1) && (!Double.IsNaN(angle)))
    {
        dx = (float)(S * 2 * cos(biometricDegreesToRadians(angle)));
        dy = (float)(S * 2 * sin(biometricDegreesToRadians(angle)));
        CGContextMoveToPoint(context, x, y);
        CGContextAddLineToPoint(context, x+dx, y+dy);
    }
}

-(void)drawDoubleCore:(NFDoubleCore)core inContext:(CGContextRef)context withSize:(CGSize)sizeArg{
    float S = correctDrawParamaterForImageResolution(CORE_LINE_LENGTH, sizeArg) / 2;
    float x = core.X;
    float y = core.Y;

    int s1 = (int)round(S);

    int size = 2 * s1;

    CGContextAddEllipseInRect(context, CGRectMake(round(x - s1), round(y - s1), size, size));
}

@end
