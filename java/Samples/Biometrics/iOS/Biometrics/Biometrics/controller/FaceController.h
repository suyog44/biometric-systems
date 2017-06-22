#import <UIKit/UIKit.h>
#import "CameraView.h"
#import "FaceResult.h"
#import "EnrollDialog.h"
#import "DatabaseController.h"

@interface FaceController : UIViewController<CameraViewDelegate, UIImagePickerControllerDelegate, UINavigationControllerDelegate, EnrollDialogDelegate, DatabaseControllerDelegate, BiometricsLayerDelegate, UIScrollViewDelegate>{
    Face *activeFace;
    FaceResult *faceResult;
    UIImage *lastImage;
    Face *lastVerificationFace;
    BOOL firstLaunch;
}

@property (nonatomic, retain) IBOutlet CameraView *cameraView;
@property (nonatomic, retain) IBOutlet UIView *cameraBottomView;
@property (nonatomic, retain) UIImageView *mainImageView;
@property (nonatomic, retain) IBOutlet UIScrollView *mainImageScrollView;
@property (nonatomic, retain) IBOutlet UIImageView *mainImageBlurView;
@property (nonatomic, retain) IBOutlet UILabel *actionLabel;
@property (nonatomic, retain) IBOutlet UILabel *imageDescription;
@property (nonatomic, retain) UIView *mainImageButtonsContainer;
@property (nonatomic, retain) UIView *mainImageComponentsContainer;

@property (nonatomic, retain) IBOutlet UILabel *selectFaceLabel;
@property (nonatomic, retain) IBOutlet UIButton *selectFaceButton;
@property (nonatomic, retain) IBOutlet UIView *selectFaceBottom;

@property (nonatomic, retain) IBOutlet UIView *actionBottom;
@property (nonatomic, retain) IBOutlet UIButton *actionBack;
@property (nonatomic, retain) IBOutlet UIButton *actionVerify;
@property (nonatomic, retain) IBOutlet UIButton *actionEnroll;
@property (nonatomic, retain) IBOutlet UIButton *actionIdentify;

@property (nonatomic, retain) IBOutlet UIButton *resultBackButton;
@property (nonatomic, retain) IBOutlet UIView *resultBottom;

@property (nonatomic, retain) IBOutlet UIImageView *leftResultImage;
@property (nonatomic, retain) IBOutlet UIImageView *rightResultImage;

@end
