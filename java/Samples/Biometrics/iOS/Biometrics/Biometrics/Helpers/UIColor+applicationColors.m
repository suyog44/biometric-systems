#import "UIColor+applicationColors.h"

@implementation UIColor (applicationColors)

+ (UIImage *)imageWithColor:(UIColor *)color {
    CGRect rect = CGRectMake(0.0f, 0.0f, 1.0f, 1.0f);
    UIGraphicsBeginImageContext(rect.size);
    CGContextRef context = UIGraphicsGetCurrentContext();
    
    CGContextSetFillColorWithColor(context, [color CGColor]);
    CGContextFillRect(context, rect);
    
    UIImage *image = UIGraphicsGetImageFromCurrentImageContext();
    UIGraphicsEndImageContext();
    
    return image;
}
+ (UIImage *)circleWithColor:(UIColor*)color andSize:(CGSize)size {
    static UIImage *blueCircle = nil;
        UIGraphicsBeginImageContextWithOptions(CGSizeMake(size.width, size.height), NO, 0.0f);
        CGContextRef ctx = UIGraphicsGetCurrentContext();
        CGContextSaveGState(ctx);
        
        CGRect rect = CGRectMake(0, 0, size.width, size.height);
        CGContextSetFillColorWithColor(ctx, color.CGColor);
        CGContextFillEllipseInRect(ctx, rect);
        
        CGContextRestoreGState(ctx);
        blueCircle = UIGraphicsGetImageFromCurrentImageContext();
        UIGraphicsEndImageContext();
    return blueCircle;
}

+(UIColor*)colorWithCustomRed:(CGFloat)red green:(CGFloat)green blue:(CGFloat)blue alpha:(CGFloat)alpha
{
    return [UIColor colorWithRed:red/255.0 green:green/255.0 blue:blue/255.0 alpha:alpha];
}
+(UIColor*)getViewControllerBgColor{
    return [UIColor colorWithCustomRed:239 green:239 blue:239 alpha:1];
}
+(UIColor*)getRecorderTimeActiveColor{
    return [UIColor colorWithCustomRed:91 green:170 blue:170 alpha:1];
}
+(UIColor*)getViewFormColor{
    return [UIColor colorWithCustomRed:91 green:170 blue:170 alpha:1];
}
+(UIColor*)getGrayBgColor{
    return [UIColor colorWithCustomRed:119 green:119 blue:119 alpha:1];
}
+(UIColor*)getGrayTextColor{
    return [UIColor colorWithCustomRed:137 green:137 blue:137 alpha:1];
}
+(UIColor*)getAppRedColor{
    return  [UIColor colorWithCustomRed:222 green:72 blue:72 alpha:1];
}
+(UIColor*)getAppBlueColor{
    return [UIColor colorWithCustomRed:53 green:102 blue:102 alpha:1];
}
+(UIColor*)getAppBlueTransparentColor{
    return [UIColor colorWithCustomRed:51 green:102 blue:102 alpha:0.3];
}
+(UIColor*)getDialogFieldBorderColor{
    return [UIColor colorWithCustomRed:51 green:102 blue:102 alpha:0.6];
}
+(UIColor*)getAppDarkBlueColor{
    return [UIColor colorWithCustomRed:33 green:65 blue:65 alpha:1];
}
+(UIColor*)getSettingsDarkTextColor{
    return [UIColor colorWithCustomRed:57 green:57 blue:57 alpha:1];
}
+(UIColor*)getDialogDarkTextColor{
    return [UIColor colorWithCustomRed:57 green:57 blue:57 alpha:1];
}
+(UIColor*)getScreenTintOnModalAppearColor{
    return [UIColor colorWithCustomRed:0 green:0 blue:0 alpha:0.75];
}
+(UIColor*)getDBLineColor{
    return [UIColor colorWithCustomRed:204 green:221 blue:221 alpha:1];
}

+(UIColor*)getDrawLineColor{
    return [UIColor redColor];
}

+(UIColor*)progressViewScreenTint{
    return [UIColor colorWithCustomRed:0 green:0 blue:0 alpha:0.3];
}
+(UIColor*)progressViewSquareColor{
    return [UIColor colorWithCustomRed:53 green:102 blue:102 alpha:0.85];
}

@end
