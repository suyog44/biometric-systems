#import <UIKit/UIKit.h>
#import "SettingsHandler.h"
#import "SettingHandler.h"
#import "SettingsSectionHeader.h"
#import "SettingsCheckboxCell.h"
#import "SettingsSliderCell.h"

@interface SettingsController : UIViewController<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout>{
    NSArray *settingsHandlers;
}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil andSettingsHandlers:(NSArray*)settingsHandlersArg andTitle:(NSString*)title;

@property (nonatomic, retain) IBOutlet UICollectionView *collectionView;

@end
