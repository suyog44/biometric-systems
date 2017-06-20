#import <UIKit/UIKit.h>
#import "CollectionDelegate.h"

@class DatabaseController;
@protocol DatabaseControllerDelegate <NSObject>
@optional
-(void)databaseController:(DatabaseController*)databaseViewController didChooseFace:(Face*)face;
-(void)databaseController:(DatabaseController*)databaseViewController didChooseFinger:(Finger*)finger;
-(void)databaseController:(DatabaseController*)databaseViewController didChooseVoice:(Voice*)voice;
-(void)databaseController:(DatabaseController*)databaseViewController didChooseEye:(Eye*)eye;
@end

@interface DatabaseController : UIViewController<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout, UIAlertViewDelegate>{
    CollectionDelegate *collectionDelegate;
}

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil andDelegate:(CollectionDelegate*)collectionDelegateArg andTitle:(NSString*)titleArg;
-(IBAction)deleteSubjectClicked:(UIButton*)arg;
@property (nonatomic, retain) IBOutlet UICollectionView *collectionView;
@property (nonatomic, assign) IBOutlet id<DatabaseControllerDelegate> delegate;

@end
