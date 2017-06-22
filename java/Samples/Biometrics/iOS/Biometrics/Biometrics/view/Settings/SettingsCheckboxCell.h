#import <UIKit/UIKit.h>
#import "SettingHandler.h"
@interface SettingsCheckboxCell : UICollectionViewCell{
    SettingHandler *handler;
}

@property (nonatomic, retain) IBOutlet UIView *topLine;
@property (nonatomic, retain) IBOutlet UIView *bottomLine;
@property (nonatomic, retain) IBOutlet UIView *middleLine;

@property (nonatomic, retain) IBOutlet UISwitch *checkbox;
@property (nonatomic, retain) IBOutlet UILabel *label;

+(float)heightForCell;
-(void)setupWithSettingHandler:(SettingHandler*)handlerArg andType:(UICollectionViewCellType)type;

@end
