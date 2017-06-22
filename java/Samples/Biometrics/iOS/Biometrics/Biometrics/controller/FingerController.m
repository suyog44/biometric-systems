#import "FingerController.h"
#import "UIImage+ImageEffects.h"
#import "NavigationController.h"
#import "ImagePickerController.h"
#import "FingersCollectionViewDelegate.h"
#import "SettingsController.h"

@interface FingerController () {
    NSObject *scanSyncObj;
    BOOL scanningInProgress;
    BOOL isCancellingScan;
}

@end

@implementation FingerController

@synthesize firstStageBottomView, firstStageTopLabel, firstStageTopView, actionBack, actionBottom, actionEnroll, actionIdentify, actionVerify, resultBackButton, resultBottom, mainImageBlurView, mainImageView, actionLabel, leftResultImage, rightResultImage, mainImageScrollView, actionScanImage, cancelScanBottomView, cancelScanButton;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"fingerTitle", nil);
        firstLaunch = true;
        scanSyncObj = [[NSObject alloc] init];
        scanningInProgress = NO;
        isCancellingScan = NO;
        self.navigationItem.rightBarButtonItem = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"settings.png"] style:UIBarButtonItemStylePlain target:self action:@selector(settingsClicked:)];
        // Custom initialization
    }
    return self;
}
-(IBAction)settingsClicked:(id)arg{
    UIViewController *viewController = [[SettingsController alloc] initWithNibName:@"SettingsController" bundle:nil andSettingsHandlers:[SettingsHandler makeFingerSettingsHandlers] andTitle:NSLocalizedString(@"settingsTitle", nil)];
    [self.navigationController pushViewController:viewController animated:true];
}

-(IBAction)galleryClicked:(id)arg{
    ImagePickerController *imagePickerController = [[ImagePickerController alloc]init];
    imagePickerController.delegate = self;
    imagePickerController.sourceType =  UIImagePickerControllerSourceTypePhotoLibrary;
    
    [self presentViewController:imagePickerController animated:true completion:^{
        
    }];
}
-(void)scanClicked:(id)arg{
    @synchronized(scanSyncObj) {
        if (scanningInProgress) {
            [[[UIAlertView alloc] initWithTitle:NSLocalizedString(@"errorTitle", nil) message:NSLocalizedString(@"lastScanNotFinished", nil) delegate:nil cancelButtonTitle:NSLocalizedString(@"close", nil) otherButtonTitles: nil] show];
            return;
        }
        scanningInProgress = YES;
    }
    if ([[BiometricsLayer sharedInstance] getConnectedScannerCount] > 0) {
        [self setupViewForScanStage];
        [[BiometricsLayer sharedInstance] scanImageFromDevice:^(HNImage hImage, NBiometricStatus status, NSString *errorMessage){
            if (hImage) {
                UIImage *image = [BiometricsLayer uiImageFromHNImage:hImage];
                [self setupViewForRecognitionLoadingStageWithUIImage:image andNImage:hImage];
            }
            else {
                if (errorMessage) {
                    [[[UIAlertView alloc] initWithTitle:NSLocalizedString(@"errorTitle", nil) message:errorMessage delegate:nil cancelButtonTitle:NSLocalizedString(@"close", nil) otherButtonTitles: nil] show];
                }
                else {
                    NSString *message;
                    switch (status) {
                        case nbsCanceled:
                            message = NSLocalizedString(@"scanCancelled", nil);
                            break;
                        case nbsTimeout:
                            message = NSLocalizedString(@"scanTimeout", nil);
                            break;
                        default:
                            message = NSLocalizedString(@"scanUnexpectedStatus", nil);
                            break;
                    }
                    @synchronized(scanSyncObj) {
                        if (isCancellingScan) {
                            isCancellingScan = NO;
                        }
                        else {
                            [[[UIAlertView alloc] initWithTitle:NSLocalizedString(@"scanFailedTitle", nil) message:message delegate:nil cancelButtonTitle:NSLocalizedString(@"close", nil) otherButtonTitles: nil] show];
                        }
                    }
                }
                [self setupViewForFirstStage];
            }
            @synchronized(scanSyncObj) {
                scanningInProgress = NO;
            }
        }];
    }
    else {
        [[[UIAlertView alloc] initWithTitle:NSLocalizedString(@"scannerNotFoundTitle", nil) message:NSLocalizedString(@"scannerNotFoundMessage", nil) delegate:nil cancelButtonTitle:NSLocalizedString(@"ok", nil) otherButtonTitles: nil] show];
        @synchronized(scanSyncObj) {
            scanningInProgress = NO;
        }
    }
}
-(void)cancelScanClicked:(id)arg{
    [self cancelScan];
    [self setupViewForFirstStage];
}
-(void)cancelScan{
    @synchronized(scanSyncObj) {
        if (scanningInProgress) {
            isCancellingScan = YES;
            [[BiometricsLayer sharedInstance] cancelScan:^{
                @synchronized(scanSyncObj) {
                    scanningInProgress = NO;
                }
            }];
        }
    }
}
-(void)resultBackClicked:(id)arg{
    if (finger == nil){
        [self setupViewForFirstStage];
    }  else{
        [self setupViewForSecondStage];
    }
}
-(void)identifyClicked:(id)arg{
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderIdentifying", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] identifySubject:finger.subject];
}
-(void)verifyClicked:(id)arg{
    DatabaseController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[FingersCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbChooseFinger", nil)];
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
    cancelScanBottomView.hidden = true;
    mainImageScrollView.hidden = true;
    mainImageScrollView.alpha = 1;
    mainImageBlurView.hidden = true;
    actionLabel.hidden = true;
    leftResultImage.hidden = true;
    rightResultImage.hidden = true;
}
-(void)setupViewForFirstStage{
    [self prepareViewForStage];
    firstStageTopLabel.text = NSLocalizedString(@"chooseFromGalleryOrScanLabel", nil);
    firstStageBottomView.hidden = false;
    firstStageTopView.hidden = false;
}
-(void)setupViewForScanStage{
    [self prepareViewForStage];
    firstStageTopLabel.text = NSLocalizedString(@"putFingerOnScanner", nil);
    cancelScanBottomView.hidden = false;
    firstStageTopView.hidden = false;
}
-(void)setupViewForRecognitionLoadingStageWithUIImage:(UIImage*)image andNImage:(HNImage)hImage{
    [self prepareViewForStage];
    lastImage = image;
    mainImageScrollView.hidden = false;
    [self setMainImage:lastImage];
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderExtracting", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] extractFinger:hImage];
}
-(void)setupViewForSecondStage{
    if (finger == nil){
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"extractFail", nil)];
        return;
    }
    [self prepareViewForStage];
    mainImageScrollView.hidden = false;
    [self setMainImage:[finger getImageWithAttributesOnImage:lastImage]];
    actionBottom.hidden = false;
//    actionLabel.hidden = false;
//    NSDictionary * blueAttributes =
//    @{ NSForegroundColorAttributeName : [UIColor getAppBlueColor],
//       NSFontAttributeName            : [UIFont appFontOfSize:18] };
//    actionLabel.attributedText = [[NSAttributedString alloc] initWithString:NSLocalizedString(@"fingerExtractSuccess", nil) attributes:blueAttributes];
}
-(void)setupViewForThirdStage:(Finger*)fingerNew andFailText:(NSString*)failText{
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
    if (fingerNew != nil){
        leftResultImage.hidden = rightResultImage.hidden = false;
        leftResultImage.image = lastImage;
        
        rightResultImage.image = [fingerNew getImage];
        
        NSString *name = fingerNew.name.length>0?fingerNew.name:@"";
        NSString *text = [NSString stringWithFormat:NSLocalizedString(@"fingerIdentifySuccess", nil), fingerNew.name];
        
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
    firstStageTopLabel.numberOfLines = 0;
    firstStageTopLabel.textAlignment = NSTextAlignmentCenter;
    
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
    
    [actionScanImage setTitleColor:[UIColor getAppBlueColor] forState:UIControlStateNormal];
    [actionScanImage setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [actionScanImage setBackgroundImage:[UIColor circleWithColor:[UIColor getAppBlueColor] andSize:actionScanImage.frame.size] forState:UIControlStateNormal];
    [actionScanImage setTitle:NSLocalizedString(@"scan", nil) forState:UIControlStateNormal];
    [actionScanImage addTarget:self action:@selector(scanClicked:) forControlEvents:UIControlEventTouchUpInside];
    
    resultBackButton.titleLabel.font = [UIFont appFontOfSize:16];
    [resultBackButton setTitleColor:[UIColor getAppRedColor] forState:UIControlStateNormal];
    [resultBackButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [resultBackButton setBackgroundImage:[UIColor circleWithColor:[UIColor getAppRedColor] andSize:resultBackButton.frame.size] forState:UIControlStateNormal];
    [resultBackButton addTarget:self action:@selector(resultBackClicked:) forControlEvents:UIControlEventTouchUpInside];
    [resultBackButton setTitle:NSLocalizedString(@"backButton", nil) forState:UIControlStateNormal];
    
    cancelScanButton.titleLabel.font = [UIFont appFontOfSize:16];
    [cancelScanButton setTitleColor:[UIColor getAppRedColor] forState:UIControlStateNormal];
    [cancelScanButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [cancelScanButton setBackgroundImage:[UIColor circleWithColor:[UIColor getAppRedColor] andSize:cancelScanButton.frame.size] forState:UIControlStateNormal];
    [cancelScanButton addTarget:self action:@selector(cancelScanClicked:) forControlEvents:UIControlEventTouchUpInside];
    [cancelScanButton setTitle:NSLocalizedString(@"cancel", nil) forState:UIControlStateNormal];
    
    // Do any additional setup after loading the view from its nib.
    mainImageScrollView.delegate = self;
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
    }
}

- (void)viewWillDisappear:(BOOL)animated{
    [super viewWillDisappear:animated];
    [self cancelScan];
}

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
        int status = [[BiometricsLayer sharedInstance] enrollSubject:finger.subject withId:value andImage:lastImage withPrefix:PREFIX_FINGER];
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
- (void)databaseController:(DatabaseController *)databaseViewController didChooseFinger:(Finger *)fingerArg{
    [databaseViewController dismissViewControllerAnimated:true completion:^{
        [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderVerifying", nil)];
        [BiometricsLayer sharedInstance].delegate = self;
        lastVerificationFinger = fingerArg;
        [[BiometricsLayer sharedInstance] verifySubject:finger.subject withSubject:lastVerificationFinger.subject];
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
        HNImage hImage = NULL;
        @try {
            NSData *pngData = UIImagePNGRepresentation(image);
            NRESULT_CHECK(NImageCreateFromMemory([pngData bytes], [pngData length], NULL, 0, NULL, NULL, &hImage));
            [self setupViewForRecognitionLoadingStageWithUIImage:image andNImage:hImage];
        }
        @finally {
            NObjectSet(NULL, &hImage);
        }
    }];
}

//BiometricsLayerDelegate
- (void)biometricsLayer:(BiometricsLayer *)bl didFinishExtractingSubject:(HNSubject)subject withSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    if (success) {
        finger = [[Finger alloc] initWithSubject:subject];
    }
    else {
        finger = nil;
    }
    [self setupViewForSecondStage];
}
-(void)biometricsLayer:(BiometricsLayer *)bl didFinishIdentifyingSubject:(HNSubject)subject withSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    Finger *tmpFinger = nil;
    if (success){
        tmpFinger = [[Finger alloc] initWithSubject:subject];
    }
    [self setupViewForThirdStage:tmpFinger andFailText:NSLocalizedString(@"identifyFail", nil)];
}
- (void)biometricsLayer:(BiometricsLayer *)bl didFinishVerifyingWithSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    if (success){
        [self setupViewForThirdStage:lastVerificationFinger andFailText:NSLocalizedString(@"verifyFail", nil)];
    } else {
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"verifyFail", nil)];
    }
}

- (UIView *)viewForZoomingInScrollView:(UIScrollView *)scrollView{
    return mainImageView;
}

@end
