#import <UIKit/UIKit.h>
#import "SettingHandler.h"
@interface SettingsSliderCell : UICollectionViewCell{
    SettingHandler *handler;
}

@property (nonatomic, retain) IBOutlet UIView *topLine;
@property (nonatomic, retain) IBOutlet UIView *bottomLine;
@property (nonatomic, retain) IBOutlet UIView *middleLine;

@property (nonatomic, retain) IBOutlet UISlider *slider;
@property (nonatomic, retain) IBOutlet UILabel *label;
@property (nonatomic, retain) IBOutlet UILabel *maxValueLabel;
@property (nonatomic, retain) IBOutlet UILabel *minValueLabel;
@property (nonatomic, retain) IBOutlet UILabel *valueLabel;

+(float)heightForCell;
-(void)setupWithSettingHandler:(SettingHandler*)handlerArg andType:(UICollectionViewCellType)type;

@end
