#import "ViewController.h"
#import "FaceView.h"
#import "Utils.h"

@interface ViewController ()

@property (weak, nonatomic) IBOutlet FaceView *faceView;
@property (weak, nonatomic) IBOutlet UILabel *statusLabel;

@end

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)showEnterIdDialog:(NSString *)buttonTitle withCallback:(void (^)(NSString *subjectId))callback {
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Enter ID" message:nil preferredStyle:UIAlertControllerStyleAlert];

    UIAlertAction *ok = [UIAlertAction actionWithTitle:buttonTitle style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        callback(alert.textFields[0].text);
    }];

    UIAlertAction *cancel = [UIAlertAction actionWithTitle:@"Cancel" style:UIAlertActionStyleCancel handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
    }];

    [alert addAction:ok];
    [alert addAction:cancel];

    [alert addTextFieldWithConfigurationHandler:^(UITextField * _Nonnull textField) {
        [textField setPlaceholder:@"ID"];
    }];

    [self presentViewController:alert animated:YES completion:nil];
}

- (void)showLivenessModeSelectDialog {
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Select Liveness Mode" message:nil preferredStyle:UIAlertControllerStyleAlert];

    NSString *noneLabel = @"None";
    NSString *passiveLabel = @"Passive";
    NSString *activeLabel = @"Active";
    NSString *passiveAndActiveLabel = @"Passive and Active";
    NSString *simpleLabel = @"Simple";
    NSString *currentLabel = @" (current)";

    NFaceVerificationLivenessMode mode;
    NRESULT_CHECK(NFaceVerificationGetLivenessMode(&mode));
    switch (mode) {
        case nllmNone:
            noneLabel = [noneLabel stringByAppendingString:currentLabel];
            break;
        case nllmPassive:
            passiveLabel = [passiveLabel stringByAppendingString:currentLabel];
            break;
        case nllmActive:
            activeLabel = [activeLabel stringByAppendingString:currentLabel];
            break;
        case nllmPassiveAndActive:
            passiveAndActiveLabel = [passiveAndActiveLabel stringByAppendingString:currentLabel];
            break;
        case nllmSimple:
            simpleLabel = [simpleLabel stringByAppendingString:currentLabel];
            break;
    }

    UIAlertAction *none = [UIAlertAction actionWithTitle:noneLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationSetLivenessMode(nllmNone));
    }];
    UIAlertAction *passive = [UIAlertAction actionWithTitle:passiveLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationSetLivenessMode(nllmPassive));
    }];
    UIAlertAction *active = [UIAlertAction actionWithTitle:activeLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationSetLivenessMode(nllmActive));
    }];
    UIAlertAction *passiveAndActive = [UIAlertAction actionWithTitle:passiveAndActiveLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationSetLivenessMode(nllmPassiveAndActive));
    }];
    UIAlertAction *simple = [UIAlertAction actionWithTitle:simpleLabel style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
        NRESULT_CHECK(NFaceVerificationSetLivenessMode(nllmSimple));
    }];

    [alert addAction:none];
    [alert addAction:passive];
    [alert addAction:active];
    [alert addAction:passiveAndActive];
    [alert addAction:simple];

    [self presentViewController:alert animated:YES completion:nil];
}

static NResult N_API previewCallback(HNFaceVerificationEventInfo hEventInfo, void * pParam) {
    HNImage hImage = NULL;
    NFaceVerificationStatus status = nlesNone;
    NFloat currentYaw = 0;
    NRect nRect = {0};
    CGRect cgRect = {0};
    NFaceVerificationLivenessAction livenessAction = nllaNone;
    NFloat targetYaw = 0;
    NByte livenessScore = 0;

    NRESULT_CHECK(NFaceVerificationEventInfoGetImage(hEventInfo, &hImage));

    NRESULT_CHECK(NFaceVerificationEventInfoGetStatus(hEventInfo, &status));
    NRESULT_CHECK(NFaceVerificationEventInfoGetYaw(hEventInfo, &currentYaw));

    NRESULT_CHECK(NFaceVerificationEventInfoGetBoundingRect(hEventInfo, &nRect));
    cgRect = CGRectMake(nRect.X, nRect.Y, nRect.Width, nRect.Height);

    NRESULT_CHECK(NFaceVerificationEventInfoGetLivenessAction(hEventInfo, &livenessAction));
    NRESULT_CHECK(NFaceVerificationEventInfoGetLivenessTargetYaw(hEventInfo, &targetYaw));
    NRESULT_CHECK(NFaceVerificationEventInfoGetLivenessScore(hEventInfo, &livenessScore));

    @autoreleasepool {
        UIImage *uiImage = nil;
        if (hImage != NULL) {
            uiImage = [Utils uiImageFromHNImage:hImage];
        } else {
            uiImage = nil;
        }
        ViewController *viewController = (__bridge id) pParam;
        dispatch_async(dispatch_get_main_queue(), ^{
            [viewController.statusLabel setText:[NSString stringWithFormat:@"Status: %i - %@", status, [Utils faceVerificationStatusToNSString:status]]];
            [viewController.faceView setFaceImage:uiImage];
            [viewController.faceView setCurrentYaw:currentYaw];
            [viewController.faceView setFaceBoundingRect:cgRect];
            [viewController.faceView setLivenessAction:livenessAction];
            [viewController.faceView setLivenessTargetYaw:targetYaw];
            [viewController.faceView setLivenessScore:livenessScore];
            [viewController.faceView repaintOverlay];
        });
    }

    NRESULT_CHECK(NObjectSet(NULL, &hImage));

    return N_OK;
}

- (void)enrollTask:(NSString *)subjectId {
    NSLog(@"enrollTask started");

    NFaceVerificationStatus status = nlesNone;

    NRESULT_CHECK(NFaceVerificationAddCapturePreviewCallback(previewCallback, (__bridge void *)self));

    NRESULT_CHECK(NFaceVerificationEnroll([subjectId UTF8String], 60000, NULL, &status));

    dispatch_async(dispatch_get_main_queue(), ^{
        UIAlertController *alert = [Utils createSimpleAlert:[NSString stringWithFormat:@"Enroll ID: %@", subjectId] withMessage:[NSString stringWithFormat:@"Enroll status: %i - %@", status, [Utils faceVerificationStatusToNSString:status]]];
        [self presentViewController:alert animated:YES completion:nil];
    });

    NRESULT_CHECK(NFaceVerificationRemoveCapturePreviewCallback(previewCallback, (__bridge void *)self));

    NSLog(@"enrollTask done");
}

- (void)verifyTask:(NSString *)subjectId {
    NSLog(@"verifyTask started");

    NFaceVerificationStatus status = nlesNone;

    NRESULT_CHECK(NFaceVerificationAddCapturePreviewCallback(previewCallback, (__bridge void *)self));

    NRESULT_CHECK(NFaceVerificationVerify([subjectId UTF8String], 60000, &status));

    dispatch_async(dispatch_get_main_queue(), ^{
        UIAlertController *alert = [Utils createSimpleAlert:[NSString stringWithFormat:@"Verify ID: %@", subjectId] withMessage:[NSString stringWithFormat:@"Verification status: %i - %@", status, [Utils faceVerificationStatusToNSString:status]]];
        [self presentViewController:alert animated:YES completion:nil];
    });

    NRESULT_CHECK(NFaceVerificationRemoveCapturePreviewCallback(previewCallback, (__bridge void *)self));

    NSLog(@"verifyTask done");
}

- (void)cancelOperationTask {
    NSLog(@"cancelOperationTask started");

    NRESULT_CHECK(NFaceVerificationCancel());

    NSLog(@"cancelOperationTask done");
}

- (void)clearDBTask {
    NSLog(@"clearDBTask started");

    NRESULT_CHECK(NFaceVerificationClearUsers());
    dispatch_async(dispatch_get_main_queue(), ^{
        UIAlertController *alert = [Utils createSimpleAlert:@"Clear DB" withMessage:@"Done."];
        [self presentViewController:alert animated:YES completion:nil];
    });

    NSLog(@"clearDBTask done");
}

- (IBAction)enrollClicked {
    NSLog(@"enrollClicked");

    [self showEnterIdDialog:@"Enroll" withCallback:^(NSString *subjectId) {
        dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            [self enrollTask:subjectId];
        });
    }];
}

- (IBAction)verifyClicked {
    NSLog(@"verifyClicked");

    [self showEnterIdDialog:@"Verify" withCallback:^(NSString *subjectId) {
        dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
            [self verifyTask:subjectId];
        });
    }];
}

- (IBAction)cancelClicked {
    NSLog(@"cancelClicked");

    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        [self cancelOperationTask];
    });
}

- (IBAction)clearDBClicked {
    NSLog(@"clearDBClicked");

    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        [self clearDBTask];
    });
}

- (IBAction)livenessModeClicked {
    NSLog(@"livenessModeClicked");

    [self showLivenessModeSelectDialog];
}

@end
