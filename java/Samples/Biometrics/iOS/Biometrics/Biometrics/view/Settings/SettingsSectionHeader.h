#import <UIKit/UIKit.h>

@interface SettingsSectionHeader : UICollectionReusableView

@property (nonatomic, retain) IBOutlet UILabel *label;

+(float)heightForView;
-(void)setupViewWithLabel:(NSString*)labelString;

@end
