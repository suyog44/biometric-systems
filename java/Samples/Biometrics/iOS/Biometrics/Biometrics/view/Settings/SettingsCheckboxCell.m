#import "SettingsCheckboxCell.h"

@implementation SettingsCheckboxCell
@synthesize label, checkbox, topLine, bottomLine, middleLine;

- (void)awakeFromNib{
    [super awakeFromNib];
    label.textColor = [UIColor getSettingsDarkTextColor];
    label.font = [UIFont appFontOfSize:16];
//    [checkbox setTintColor:[UIColor getAppBlueColor]];
    [checkbox setOnTintColor:[UIColor getAppBlueColor]];
    [checkbox addTarget:self action:@selector(checkboxValueChanged:) forControlEvents:UIControlEventValueChanged];
    topLine.backgroundColor = middleLine.backgroundColor = bottomLine.backgroundColor = [UIColor getDBLineColor];
}
-(void)checkboxValueChanged:(id)arg{
    [handler setBoolProperty:(NBoolean)checkbox.isOn];
    [checkbox setOn:[handler getBoolProperty] animated:true];
}
-(void)prepareForReuse{
    [super prepareForReuse];
    [checkbox setOn:false];
    label.text = @"";
}

+(float)heightForCell{
    return 44;
}
-(void)setupWithSettingHandler:(SettingHandler*)handlerArg andType:(UICollectionViewCellType)type{
    handler = handlerArg;
    label.text = handler.title;
    [checkbox setOn:[handler getBoolProperty]];
    
    switch (type) {
        case UICollectionViewCellTypeFirst:
        {
            topLine.hidden = false;
            bottomLine.hidden = true;
            middleLine.hidden = false;
        }
            break;
        case UICollectionViewCellTypeFirstAndLast:
        {
            topLine.hidden = false;
            bottomLine.hidden = false;
            middleLine.hidden = true;
        }
            break;
        case UICollectionViewCellTypeLast:
        {
            topLine.hidden = true;
            bottomLine.hidden = false;
            middleLine.hidden = true;
        }
            break;
        case UICollectionViewCellTypeMiddle:
        {
            topLine.hidden = true;
            bottomLine.hidden = true;
            middleLine.hidden = false;
        }
            break;
            
        default:
            break;
    }

}

@end
