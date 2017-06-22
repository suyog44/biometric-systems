#import <UIKit/UIKit.h>
#import "Finger.h"
#import "EnrollDialog.h"
#import "DatabaseController.h"

@interface FingerController : UIViewController<UIImagePickerControllerDelegate, UINavigationControllerDelegate, EnrollDialogDelegate, BiometricsLayerDelegate, DatabaseControllerDelegate, UIScrollViewDelegate>{
    BOOL firstLaunch;
    Finger *finger;
    Finger *lastVerificationFinger;
    UIImage *lastImage;
}

@property (nonatomic, retain) IBOutlet UIView *firstStageTopView;
@property (nonatomic, retain) IBOutlet UIView *firstStageBottomView;
@property (nonatomic, retain) IBOutlet UILabel *firstStageTopLabel;
@property (nonatomic, retain) UIImageView *mainImageView;
@property (nonatomic, retain) IBOutlet UIImageView *mainImageBlurView;
@property (nonatomic, retain) IBOutlet UILabel *actionLabel;
@property (nonatomic, retain) IBOutlet UIScrollView *mainImageScrollView;
@property (nonatomic, retain) IBOutlet UIButton *actionScanImage;

@property (nonatomic, retain) IBOutlet UIView *actionBottom;
@property (nonatomic, retain) IBOutlet UIButton *actionBack;
@property (nonatomic, retain) IBOutlet UIButton *actionVerify;
@property (nonatomic, retain) IBOutlet UIButton *actionEnroll;
@property (nonatomic, retain) IBOutlet UIButton *actionIdentify;

@property (nonatomic, retain) IBOutlet UIButton *resultBackButton;
@property (nonatomic, retain) IBOutlet UIView *resultBottom;

@property (nonatomic, retain) IBOutlet UIView *cancelScanBottomView;
@property (nonatomic, retain) IBOutlet UIButton *cancelScanButton;

@property (nonatomic, retain) IBOutlet UIImageView *leftResultImage;
@property (nonatomic, retain) IBOutlet UIImageView *rightResultImage;

@end
