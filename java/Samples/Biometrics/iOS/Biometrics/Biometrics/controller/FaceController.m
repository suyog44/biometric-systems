#import "FaceController.h"
#import "SettingsController.h"
#import "UIImageView+geometryConverse.h"
#import "UIImage+ImageEffects.h"
#import "ImagePickerController.h"
#import "FacesCollectionViewDelegate.h"
#import "NavigationController.h"

@interface FaceController ()

@end

@implementation FaceController
@synthesize cameraView, actionLabel, cameraBottomView, mainImageView, imageDescription, mainImageButtonsContainer, selectFaceButton, selectFaceLabel, selectFaceBottom, actionBottom, actionEnroll, actionIdentify, actionVerify, actionBack, mainImageBlurView, resultBackButton, resultBottom, leftResultImage, rightResultImage, mainImageScrollView, mainImageComponentsContainer;
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"faceTitle", nil);
        firstLaunch = true;
        self.navigationItem.rightBarButtonItem = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"settings.png"] style:UIBarButtonItemStylePlain target:self action:@selector(settingsClicked:)];
        // Custom initialization
    }
    return self;
}
-(IBAction)galleryClicked:(id)arg{
    ImagePickerController *imagePickerController = [[ImagePickerController alloc]init];
    imagePickerController.delegate = self;
    imagePickerController.sourceType =  UIImagePickerControllerSourceTypePhotoLibrary;
    
    [self presentViewController:imagePickerController animated:true completion:^{
        
    }];
}
-(IBAction)settingsClicked:(id)arg{
    UIViewController *viewController = [[SettingsController alloc] initWithNibName:@"SettingsController" bundle:nil andSettingsHandlers:[SettingsHandler makeFaceSettingsHandlers] andTitle:NSLocalizedString(@"settingsTitle", nil)];
    [self.navigationController pushViewController:viewController animated:true];
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    self.view.backgroundColor = [UIColor getViewControllerBgColor];
    [cameraView prepareCamera];
    cameraView.delegate = self;
    cameraView.backgroundColor = mainImageScrollView.backgroundColor = mainImageBlurView.backgroundColor = [UIColor getGrayBgColor];
    actionLabel.textColor = [UIColor whiteColor];
    actionLabel.font = [UIFont appFontOfSize:16];
    imageDescription.textColor = [UIColor whiteColor];
    imageDescription.font = [UIFont appFontOfSize:13];
    selectFaceLabel.font = selectFaceButton.titleLabel.font = [UIFont appFontOfSize:16];
    selectFaceLabel.textColor = [UIColor getGrayTextColor];
    [selectFaceButton setTitleColor:[UIColor getAppRedColor] forState:UIControlStateNormal];
    [selectFaceButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [selectFaceButton setBackgroundImage:[UIColor circleWithColor:[UIColor getAppRedColor] andSize:selectFaceButton.frame.size] forState:UIControlStateNormal];
    [selectFaceButton addTarget:self action:@selector(setupViewForFirstStage) forControlEvents:UIControlEventTouchUpInside];
    [selectFaceButton setTitle:NSLocalizedString(@"backButton", nil) forState:UIControlStateNormal];
    selectFaceLabel.text = NSLocalizedString(@"faceSelectTheFace", nil);
    
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
    [mainImageComponentsContainer removeFromSuperview];
    [mainImageButtonsContainer removeFromSuperview];
    [mainImageView removeFromSuperview];
    mainImageView = [[UIImageView alloc] initWithImage:image];
    
    mainImageButtonsContainer = [[UIView alloc] initWithFrame:mainImageView.frame];

    mainImageComponentsContainer = [[UIView alloc] initWithFrame:mainImageView.frame];
    [mainImageComponentsContainer addSubview:mainImageView];
    [mainImageComponentsContainer addSubview:mainImageButtonsContainer];
    [mainImageScrollView addSubview:mainImageComponentsContainer];
    
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
-(void)resultBackClicked:(id)arg{
    if (faceResult.faces.count == 0){
        [self setupViewForFirstStage];
    }  else{
        [self setupViewForSecondStage];
    }
}
-(void)identifyClicked:(id)arg{
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderIdentifying", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] identifySubject:activeFace.subject];
}
-(void)verifyClicked:(id)arg{
    DatabaseController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[FacesCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbChooseFace", nil)];
    viewController.delegate = self;
    NavigationController *nav = [[NavigationController alloc] initWithRootViewController:viewController];
    [self presentViewController:nav animated:true completion:^{
        
    }];
}
- (void)enrollClicked:(id)arg{
    EnrollDialog *popup = [[[NSBundle mainBundle] loadNibNamed:@"EnrollDialog" owner:self options:nil] objectAtIndex:0];
    popup.delegate = self;
    [popup presentDialogWithTitle:NSLocalizedString(@"enrollTitle", nil) placeholder:NSLocalizedString(@"enrollPlaceholder", nil) cancelString:NSLocalizedString(@"cancelButton", nil) acceptString:NSLocalizedString(@"enrollButton", nil) view:self.view.window];
}
- (void)enrollDialog:(EnrollDialog *)dialog acceptedWithValue:(NSString *)value{
    @try {
        int status = [[BiometricsLayer sharedInstance] enrollSubject:activeFace.subject withId:value andImage:faceResult.image withPrefix:PREFIX_FACE];
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
- (void)cameraView:(CameraView *)cameraView didTakePicture:(UIImage *)image{
    [self setupViewForRecognitionLoadingStageWithImage:image];
    
}
-(void)prepareViewForStage{
    resultBottom.hidden = true;
    cameraView.hidden = true;
    cameraBottomView.hidden = true;
    mainImageScrollView.hidden = true;
    mainImageScrollView.alpha = 1;
    mainImageBlurView.hidden = true;
    actionLabel.hidden = true;
    imageDescription.hidden = true;
    selectFaceBottom.hidden = true;
    actionBottom.hidden = true;
    leftResultImage.hidden = true;
    rightResultImage.hidden = true;
    [mainImageButtonsContainer.subviews makeObjectsPerformSelector:@selector(removeFromSuperview)];
    [cameraView pauseCamera];
}
-(void)setupViewForFirstStage{
    [self prepareViewForStage];
    cameraView.hidden = false;
    cameraBottomView.hidden = false;
    actionLabel.hidden = false;
    actionLabel.text = NSLocalizedString(@"faceCameraLabel", nil);
    faceResult = nil;
    activeFace = nil;
    [cameraView startCamera];
}
-(void)setupViewForThirdStage:(Face*)face andFailText:(NSString*)failText{
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
    if (face != nil){
        leftResultImage.hidden = rightResultImage.hidden = false;
        leftResultImage.image = faceResult.image;
        
        rightResultImage.image = [face getImage];

        NSString *name = face.name.length>0?face.name:@"";
        NSString *text = [NSString stringWithFormat:NSLocalizedString(@"faceIdentifySuccess", nil), face.name];
        
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
-(void)setupViewForRecognitionLoadingStageWithImage:(UIImage*)image{
    [self prepareViewForStage];
    cameraBottomView.hidden = false;
    actionLabel.hidden = false;
    mainImageScrollView.hidden = false;
    actionLabel.text = NSLocalizedString(@"faceCameraLabel", nil);
    lastImage = image;
    [self setMainImage:lastImage];
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderExtracting", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] extractFaces:lastImage];
}
-(void)setupViewForSecondStage{
    if (faceResult.faces.count == 0){
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"extractFail", nil)];
        return;
    }
    [self prepareViewForStage];
    imageDescription.hidden = false;
    mainImageScrollView.hidden = false;
    [self setMainImage:faceResult.image];
    for (int i = 0; i < faceResult.faces.count; i++){
        Face *face =[faceResult.faces objectAtIndex:i];
        UIButton *button = [[UIButton alloc] initWithFrame:[mainImageView convertRectFromImage:face.frame]];
        button.layer.borderColor = [UIColor getAppBlueColor].CGColor;
        button.layer.borderWidth = 2;
        button.tag = i;
        [button setBackgroundImage:[UIColor imageWithColor:[UIColor getAppBlueTransparentColor]] forState:UIControlStateHighlighted];
        [button setBackgroundImage:[UIColor imageWithColor:[UIColor getAppBlueTransparentColor]] forState:UIControlStateSelected];
        [button addTarget:self action:@selector(clickedFace:) forControlEvents:UIControlEventTouchUpInside];
        [mainImageButtonsContainer addSubview:button];
        if ((i == 0 && faceResult.faces.count == 1) || activeFace == face){
            [button sendActionsForControlEvents:UIControlEventTouchUpInside];
        }
    }
    [self updateSecondStage];
}
-(void)clickedFace:(UIButton*)button{
    for (UIButton *tmp in mainImageButtonsContainer.subviews){
        tmp.layer.borderColor = [UIColor getAppBlueColor].CGColor;
        [tmp setSelected:false];
    }
    button.layer.borderColor = [UIColor getAppBlueColor].CGColor;
    [button setSelected:true];
    activeFace = [faceResult.faces objectAtIndex:button.tag];
    [self updateSecondStage];
}
-(void)setActiveFace:(Face*)face{
    activeFace = face;
    [self updateSecondStage];
}
-(void)updateSecondStage{
    if (activeFace != nil){
        imageDescription.text = [NSString stringWithFormat:NSLocalizedString(@"extractionNumberOfFaces", nil), faceResult.faces.count];
        imageDescription.text = [NSString stringWithFormat:@"%@\n%@", imageDescription.text, [activeFace getDescription]];
        selectFaceBottom.hidden = true;
        actionBottom.hidden = false;
    } else {
        imageDescription.text = [NSString stringWithFormat:NSLocalizedString(@"extractionNumberOfFaces", nil), faceResult.faces.count];
        selectFaceBottom.hidden = false;
        actionBottom.hidden =  true;
    }
}
- (void)viewWillAppear:(BOOL)animated{
    [super viewWillAppear:animated];
    if (firstLaunch){
        firstLaunch = false;
        if (faceResult != nil){
            [self setupViewForSecondStage];
        } else {
            [self setupViewForFirstStage];
        }
        
    }
    if (!cameraView.hidden) [cameraView startCamera];
}
- (void)viewWillDisappear:(BOOL)animated{
    [super viewWillDisappear:animated];
    [cameraView pauseCamera];
}
- (void)viewDidDisappear:(BOOL)animated{
    [super viewDidDisappear:animated];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}
//Choose face dialog
- (void)databaseController:(DatabaseController *)databaseViewController didChooseFace:(Face *)face{
    [databaseViewController dismissViewControllerAnimated:true completion:^{
        [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderVerifying", nil)];
        [BiometricsLayer sharedInstance].delegate = self;
        lastVerificationFace = face;
        [[BiometricsLayer sharedInstance] verifySubject:activeFace.subject withSubject:lastVerificationFace.subject];
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
    faceResult = [[FaceResult alloc] initWithImage:lastImage andSubject:subject];
    [self setupViewForSecondStage];
}
-(void)biometricsLayer:(BiometricsLayer *)bl didFinishIdentifyingSubject:(HNSubject)subject withSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    Face *tmpFace = nil;
    if (success){
        tmpFace = [[Face alloc] initWithSubject:subject];
    }
    [self setupViewForThirdStage:tmpFace andFailText:NSLocalizedString(@"identifyFail", nil)];
}
- (void)biometricsLayer:(BiometricsLayer *)bl didFinishVerifyingWithSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    if (success){
        [self setupViewForThirdStage:lastVerificationFace andFailText:NSLocalizedString(@"verifyFail", nil)];
    } else {
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"verifyFail", nil)];
    }
}

- (UIView *)viewForZoomingInScrollView:(UIScrollView *)scrollView{
    return mainImageComponentsContainer;
}

@end
