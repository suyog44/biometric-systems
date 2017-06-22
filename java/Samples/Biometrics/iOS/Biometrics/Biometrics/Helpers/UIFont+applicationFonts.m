#import "UIFont+applicationFonts.h"

@implementation UIFont (applicationFonts)

+(UIFont*)appFontOfSize:(CGFloat)size{return [UIFont fontWithName:@"HelveticaNeue" size:size];}
+(UIFont*)appMediumFontOfSize:(CGFloat)size{return [UIFont fontWithName:@"HelveticaNeue-Medium" size:size];}
+(UIFont*)appLightFontOfSize:(CGFloat)size{return [UIFont fontWithName:@"HelveticaNeue-Light" size:size];}
+(UIFont*)appBoldFontOfSize:(CGFloat)size{return [UIFont fontWithName:@"HelveticaNeue-Bold" size:size];}
+(UIFont*)appThinFontOfSize:(CGFloat)size{return [UIFont fontWithName:@"HelveticaNeue-Thin" size:size];};

@end
