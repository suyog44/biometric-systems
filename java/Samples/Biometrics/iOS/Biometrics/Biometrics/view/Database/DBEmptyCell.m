#import "DBEmptyCell.h"

@implementation DBEmptyCell
@synthesize label;

- (void)awakeFromNib{
    label.textColor = [UIColor getSettingsDarkTextColor];
    label.font = [UIFont appLightFontOfSize:16];
    label.text = NSLocalizedString(@"emptyList", nil);
    [super awakeFromNib];
}
/*
// Only override drawRect: if you perform custom drawing.
// An empty implementation adversely affects performance during animation.
- (void)drawRect:(CGRect)rect
{
    // Drawing code
}
*/

@end
