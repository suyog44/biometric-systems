#import <UIKit/UIKit.h>

@interface UIColor (applicationColors)

+ (UIImage *)imageWithColor:(UIColor *)color;
+ (UIImage *)circleWithColor:(UIColor*)color andSize:(CGSize)size ;
+(UIColor*)colorWithCustomRed:(CGFloat)red green:(CGFloat)green blue:(CGFloat)blue alpha:(CGFloat)alpha;
+(UIColor*)getViewControllerBgColor;
+(UIColor*)getRecorderTimeActiveColor;
+(UIColor*)getGrayBgColor;
+(UIColor*)getAppBlueColor;
+(UIColor*)getAppBlueTransparentColor;
+(UIColor*)getAppDarkBlueColor;
+(UIColor*)getSettingsDarkTextColor;
+(UIColor*)getGrayTextColor;
+(UIColor*)getAppRedColor;
+(UIColor*)getDialogDarkTextColor;
+(UIColor*)getScreenTintOnModalAppearColor;
+(UIColor*)getDialogFieldBorderColor;
+(UIColor*)getDBLineColor;

+(UIColor*)getDrawLineColor;

+(UIColor*)getViewFormColor;

+(UIColor*)progressViewScreenTint;
+(UIColor*)progressViewSquareColor;

@end
