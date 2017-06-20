#import "EyeController.h"
#import "UIImage+ImageEffects.h"
#import "NavigationController.h"
#import "ImagePickerController.h"
#import "EyesCollectionViewDelegate.h"
#import "UIImageView+geometryConverse.h"
#import "SettingsController.h"

@interface EyeController ()

@end

@implementation EyeController
@synthesize firstStageBottomView, firstStageTopLabel, firstStageTopView, actionBack, actionBottom, actionEnroll, actionIdentify, actionVerify, resultBackButton, resultBottom, mainImageBlurView, mainImageView, actionLabel, leftResultImage, rightResultImage, mainImageScrollView;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"eyeTitle", nil);
        firstLaunch = true;
        self.navigationItem.rightBarButtonItem = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"settings.png"] style:UIBarButtonItemStylePlain target:self action:@selector(settingsClicked:)];
        // Custom initialization
    }
    return self;
}
-(IBAction)settingsClicked:(id)arg{
    UIViewController *viewController = [[SettingsController alloc] initWithNibName:@"SettingsController" bundle:nil andSettingsHandlers:[SettingsHandler makeEyeSettingsHandlers] andTitle:NSLocalizedString(@"settingsTitle", nil)];
    [self.navigationController pushViewController:viewController animated:true];
}

-(IBAction)galleryClicked:(id)arg{
    ImagePickerController *imagePickerController = [[ImagePickerController alloc]init];
    imagePickerController.delegate = self;
    imagePickerController.sourceType =  UIImagePickerControllerSourceTypePhotoLibrary;
    
    [self presentViewController:imagePickerController animated:true completion:^{
        
    }];
}
-(void)resultBackClicked:(id)arg{
    if (eye == nil){
        [self setupViewForFirstStage];
    }  else{
        [self setupViewForSecondStage];
    }
}
-(void)identifyClicked:(id)arg{
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderIdentifying", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] identifySubject:eye.subject];
}
-(void)verifyClicked:(id)arg{
    DatabaseController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[EyesCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbChooseEye", nil)];
    viewController.delegate = self;
    NavigationController *nav = [[NavigationController alloc] initWithRootViewController:viewController];
    [self presentViewController:nav animated:true completion:^{
        
    }];
}
-(void)prepareViewForStage{
    firstStageBottomView.hidden = true;
    firstStageTopView.hidden = true;
    actionBottom.hidden = true;
    resultBottom.hidden = true;
    mainImageScrollView.hidden = true;
    mainImageScrollView.alpha = 1;
    mainImageBlurView.hidden = true;
    actionLabel.hidden = true;
    leftResultImage.hidden = true;
    rightResultImage.hidden = true;
}
-(void)setupViewForFirstStage{
    [self prepareViewForStage];
    firstStageBottomView.hidden = false;
    firstStageTopView.hidden = false;
}
-(void)setupViewForRecognitionLoadingStageWithImage:(UIImage*)image{
    [self prepareViewForStage];
    lastImage = image;
    firstStageBottomView.hidden = false;
    mainImageScrollView.hidden = false;
    [self setMainImage:lastImage];
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderExtracting", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] extractEye:image];
}
-(void)setupViewForSecondStage{
    if (eye == nil){
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"extractFail", nil)];
        return;
    }
    [self prepareViewForStage];
    mainImageScrollView.hidden = false;
    [self setMainImage:[eye getImageWithAttributesOnImage:lastImage]];
    actionBottom.hidden = false;
//    actionLabel.hidden = false;
//    NSDictionary * blueAttributes =
//    @{ NSForegroundColorAttributeName : [UIColor getAppBlueColor],
//       NSFontAttributeName            : [UIFont appFontOfSize:18] };
//    actionLabel.attributedText = [[NSAttributedString alloc] initWithString:NSLocalizedString(@"eyeExtractSuccess", nil) attributes:blueAttributes];
}
-(void)setupViewForThirdStage:(Eye*)eyeNew andFailText:(NSString*)failText{
    [self prepareViewForStage];
    mainImageScrollView.hidden = false;
    resultBottom.hidden = false;
    mainImageBlurView.hidden = false;
    actionLabel.hidden = false;
    [UIView animateWithDuration:0.34 delay:0 options:UIViewAnimationOptionCurveEaseInOut animations:^{
        mainImageScrollView.alpha = 0;
    } completion:nil];
    
    NSDictionary * redAttributes =
    @{ NSForegroundColorAttributeName : [UIColor getAppRedColor],
       NSFontAttributeName            : [UIFont appFontOfSize:18] };
    NSDictionary * blueAttributes =
    @{ NSForegroundColorAttributeName : [UIColor getAppBlueColor],
       NSFontAttributeName            : [UIFont appFontOfSize:18] };
    NSDictionary * whiteAttributes =
    @{ NSForegroundColorAttributeName : [UIColor whiteColor],
       NSFontAttributeName            : [UIFont appFontOfSize:18] };
    if (eyeNew != nil){
        leftResultImage.hidden = rightResultImage.hidden = false;
        leftResultImage.image = lastImage;
        
        rightResultImage.image = [eyeNew getImage];
        
        NSString *name = eyeNew.name.length>0?eyeNew.name:@"";
        NSString *text = [NSString stringWithFormat:NSLocalizedString(@"eyeIdentifySuccess", nil), eyeNew.name];
        
        NSMutableAttributedString *attributedString = [[NSMutableAttributedString alloc] initWithString:text attributes:whiteAttributes];
        [attributedString setAttributes:blueAttributes range:[text rangeOfString:name]];
        actionLabel.attributedText = attributedString;
    } else {
        actionLabel.attributedText = [[NSAttributedString alloc] initWithString:failText attributes:redAttributes];
    }
    
    CGSize size;
    size.width = [UIScreen mainScreen].scale * mainImageBlurView.frame.size.width;
    size.height = [UIScreen mainScreen].scale * mainImageBlurView.frame.size.height;
    
    mainImageBlurView.image = [[mainImageView.image imageScaledToSize:size] applyImageEffect];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    self.view.backgroundColor = [UIColor getViewControllerBgColor];
    mainImageScrollView.backgroundColor = mainImageBlurView.backgroundColor = [UIColor getGrayBgColor];
    firstStageTopView.backgroundColor = [UIColor getGrayBgColor];
    firstStageTopLabel.textColor = [UIColor whiteColor];
    firstStageTopLabel.font = [UIFont appFontOfSize:18];
    firstStageTopLabel.text = NSLocalizedString(@"chooseFromGalleryLabel", nil);
    
    actionLabel.textColor = [UIColor whiteColor];
    actionLabel.font = [UIFont appFontOfSize:16];
    
    actionBack.titleLabel.font = actionEnroll.titleLabel.font = actionIdentify.titleLabel.font = actionVerify.titleLabel.font = [UIFont appFontOfSize:14];
    [actionBack setTitleColor:[UIColor getAppRedColor] forState:UIControlStateNormal];
    [actionEnroll setTitleColor:[UIColor getAppBlueColor] forState:UIControlStateNormal];
    [actionIdentify setTitleColor:[UIColor getAppBlueColor] forState:UIControlStateNormal];
    [actionVerify setTitleColor:[UIColor getAppBlueColor] forState:UIControlStateNormal];
    [actionBack setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [actionEnroll setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [actionIdentify setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [actionVerify setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [actionBack setBackgroundImage:[UIColor circleWithColor:[UIColor getAppRedColor] andSize:actionBack.frame.size] forState:UIControlStateNormal];
    [actionEnroll setBackgroundImage:[UIColor circleWithColor:[UIColor getAppDarkBlueColor] andSize:actionEnroll.frame.size] forState:UIControlStateNormal];
    [actionIdentify setBackgroundImage:[UIColor circleWithColor:[UIColor getAppBlueColor] andSize:actionIdentify.frame.size] forState:UIControlStateNormal];
    [actionVerify setBackgroundImage:[UIColor circleWithColor:[UIColor getAppBlueColor] andSize:actionVerify.frame.size] forState:UIControlStateNormal];
    [actionBack setTitle:NSLocalizedString(@"backButton", nil) forState:UIControlStateNormal];
    [actionEnroll setTitle:NSLocalizedString(@"enroll", nil) forState:UIControlStateNormal];
    [actionIdentify setTitle:NSLocalizedString(@"identify", nil) forState:UIControlStateNormal];
    [actionVerify setTitle:NSLocalizedString(@"verify", nil) forState:UIControlStateNormal];
    
    
    [actionBack addTarget:self action:@selector(setupViewForFirstStage) forControlEvents:UIControlEventTouchUpInside];
    [actionEnroll addTarget:self action:@selector(enrollClicked:) forControlEvents:UIControlEventTouchUpInside];
    [actionIdentify addTarget:self action:@selector(identifyClicked:) forControlEvents:UIControlEventTouchUpInside];
    [actionVerify addTarget:self action:@selector(verifyClicked:) forControlEvents:UIControlEventTouchUpInside];
    
    resultBackButton.titleLabel.font = [UIFont appFontOfSize:16];
    [resultBackButton setTitleColor:[UIColor getAppRedColor] forState:UIControlStateNormal];
    [resultBackButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [resultBackButton setBackgroundImage:[UIColor circleWithColor:[UIColor getAppRedColor] andSize:resultBackButton.frame.size] forState:UIControlStateNormal];
    [resultBackButton addTarget:self action:@selector(resultBackClicked:) forControlEvents:UIControlEventTouchUpInside];
    [resultBackButton setTitle:NSLocalizedString(@"backButton", nil) forState:UIControlStateNormal];
    
    
    mainImageScrollView.delegate = self;
    // Do any additional setup after loading the view from its nib.
}
-(void)setMainImage:(UIImage*)image{
    [mainImageView removeFromSuperview];
    mainImageView = [[UIImageView alloc] initWithImage:image];
    [mainImageScrollView addSubview:mainImageView];
    
    float widthRatio = mainImageScrollView.frame.size.width/mainImageView.frame.size.width;
    float heightRatio = mainImageScrollView.frame.size.height/mainImageView.frame.size.height;
    float ratio = widthRatio<heightRatio?widthRatio:heightRatio;
    mainImageScrollView.contentSize = mainImageView.frame.size;
    mainImageScrollView.minimumZoomScale = ratio;
    mainImageScrollView.maximumZoomScale = ratio>1.0f?ratio:1.0f;
    mainImageScrollView.zoomScale = ratio;
    mainImageScrollView.bouncesZoom = false;
    mainImageScrollView.bounces = false;
    mainImageScrollView.showsHorizontalScrollIndicator = false;
    mainImageScrollView.showsVerticalScrollIndicator = false;
    
    float widthWithRatio = image.size.width * ratio;
    float heightWithRatio = image.size.height * ratio;
    mainImageScrollView.contentInset = UIEdgeInsetsMake((mainImageScrollView.frame.size.height-heightWithRatio)/2, (mainImageScrollView.frame.size.width-widthWithRatio)/2, (mainImageScrollView.frame.size.height-heightWithRatio)/2, (mainImageScrollView.frame.size.width-widthWithRatio)/2);
}

- (void)viewWillAppear:(BOOL)animated{
    [super viewWillAppear:animated];
    if (firstLaunch){
        firstLaunch = false;
        [self setupViewForFirstStage];
    }}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}
//Actions
- (void)enrollClicked:(id)arg{
    EnrollDialog *popup = [[[NSBundle mainBundle] loadNibNamed:@"EnrollDialog" owner:self options:nil] objectAtIndex:0];
    popup.delegate = self;
    [popup presentDialogWithTitle:NSLocalizedString(@"enrollTitle", nil) placeholder:NSLocalizedString(@"enrollPlaceholder", nil) cancelString:NSLocalizedString(@"cancelButton", nil) acceptString:NSLocalizedString(@"enrollButton", nil) view:self.view.window];
}
- (void)enrollDialog:(EnrollDialog *)dialog acceptedWithValue:(NSString *)value{
    @try {
        int status = [[BiometricsLayer sharedInstance] enrollSubject:eye.subject withId:value andImage:lastImage withPrefix:PREFIX_EYE];
        if (status != 0){
            [dialog handleStatusCode:status];
        } else {
            [dialog dismissView];
        }
    }
    @catch (NSException *exception) {
        [dialog handleException:exception];
    }
}
//Choose face dialog
- (void)databaseController:(DatabaseController *)databaseViewController didChooseEye:(Eye *)eyeArg{
    [databaseViewController dismissViewControllerAnimated:true completion:^{
        [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderVerifying", nil)];
        [BiometricsLayer sharedInstance].delegate = self;
        lastVerificationEye = eyeArg;
        [[BiometricsLayer sharedInstance] verifySubject:eye.subject withSubject:lastVerificationEye.subject];
    }];
}

//ImagePicker
- (void)navigationController:(UINavigationController *)navigationController willShowViewController:(UIViewController *)viewController animated:(BOOL)animated
{
    [[UIApplication sharedApplication] setStatusBarStyle:UIStatusBarStyleLightContent];
}
- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info{
    UIImage *image = [info objectForKey:UIImagePickerControllerOriginalImage];
    [self dismissViewControllerAnimated:true completion:^{
        [self setupViewForRecognitionLoadingStageWithImage:image];
    }];
}

//BiometricsLayerDelegate
- (void)biometricsLayer:(BiometricsLayer *)bl didFinishExtractingSubject:(HNSubject)subject withSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    if (success) eye = [[Eye alloc] initWithSubject:subject];
    [self setupViewForSecondStage];
}
-(void)biometricsLayer:(BiometricsLayer *)bl didFinishIdentifyingSubject:(HNSubject)subject withSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    NSLog(@"is current thread == main thread: %i", [NSThread currentThread] == [NSThread mainThread]);
    Eye *tmpEye = nil;
    if (success){
        tmpEye = [[Eye alloc] initWithSubject:subject];
    }
    [self setupViewForThirdStage:tmpEye andFailText:NSLocalizedString(@"identifyFail", nil)];
}
- (void)biometricsLayer:(BiometricsLayer *)bl didFinishVerifyingWithSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    if (success){
        [self setupViewForThirdStage:lastVerificationEye andFailText:NSLocalizedString(@"verifyFail", nil)];
    } else {
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"verifyFail", nil)];
    }
}

- (UIView *)viewForZoomingInScrollView:(UIScrollView *)scrollView{
    return mainImageView;
}

@end
