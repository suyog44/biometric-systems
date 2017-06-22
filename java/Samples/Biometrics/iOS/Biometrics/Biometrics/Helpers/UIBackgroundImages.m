#import "UIBackgroundImages.h"

@implementation UIBackgroundImages

+(UIImage*)getSliderMinimumTrack{
    return [self getResizableImageForName:@"slider_minimum_track.png"];
}
+(UIImage*)getSliderMaximumTrack{
    return [self getResizableImageForName:@"slider_maximum_track.png"];
}

+ (UIImage *)getResizableImageForName:(NSString*)name{
    UIImage *img = [UIImage imageNamed:name];
    img = [img resizableImageWithCapInsets:UIEdgeInsetsMake(img.size.height/2-1, img.size.width/2-1, img.size.height/2-1, img.size.width/2-1)];
    return img;
}

@end
