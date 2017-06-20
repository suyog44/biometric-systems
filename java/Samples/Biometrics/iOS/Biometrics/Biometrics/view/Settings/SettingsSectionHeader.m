#import "SettingsSectionHeader.h"

@implementation SettingsSectionHeader
@synthesize label;

- (void)awakeFromNib{
    [super awakeFromNib];
    label.textColor = [UIColor getSettingsDarkTextColor];
    label.font = [UIFont appFontOfSize:12];
    self.backgroundColor = [UIColor getViewControllerBgColor];
}
- (void)prepareForReuse{
    [super prepareForReuse];
    label.text = @"";
}

+(float)heightForView{
    return 40;
}
-(void)setupViewWithLabel:(NSString*)labelString{
    label.text = labelString;
}

@end
