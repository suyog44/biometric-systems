#import "MenuController.h"
#import "FingerController.h"
#import "VoiceController.h"
#import "EyeController.h"
#import "FaceController.h"
#import "DatabaseController.h"
#import "InfoController.h"

@interface MenuController ()

@end

@implementation MenuController
@synthesize databaseButton, infoButton, faceButton, fingerButton, eyeButton, voiceButton;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = NSLocalizedString(@"menuTitle", nil);
    }
    return self;
}
- (IBAction)fingerClicked:(id)sender{
    FingerController *viewController = [[FingerController alloc] initWithNibName:@"FingerController" bundle:nil];
    [self.navigationController pushViewController:viewController animated:true];
}
- (IBAction)voiceClicked:(id)sender{
    VoiceController *viewController = [[VoiceController alloc] initWithNibName:@"VoiceController" bundle:nil];
    [self.navigationController pushViewController:viewController animated:true];
}
-(IBAction)eyeClicked:(id)sender{
    EyeController *viewController = [[EyeController alloc] initWithNibName:@"EyeController" bundle:nil];
    [self.navigationController pushViewController:viewController animated:true];
}
-(IBAction)faceClicked:(id)sender{
    FaceController *viewController = [[FaceController alloc] initWithNibName:@"FaceController" bundle:nil];
    [self.navigationController pushViewController:viewController animated:true];
}
-(IBAction)databaseClicked:(id)sender{
    DatabaseController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:nil andTitle:NSLocalizedString(@"databaseTitle", nil)];
    [self.navigationController pushViewController:viewController animated:true];
}
-(IBAction)infoClicked:(id)sender{
    InfoController *viewController = [[InfoController alloc] initWithNibName:@"InfoController" bundle:nil];
    [self.navigationController pushViewController:viewController animated:true];
}
- (void)viewDidLoad
{
    [super viewDidLoad];
    self.view.backgroundColor = [UIColor getViewControllerBgColor];
    [databaseButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [infoButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    databaseButton.titleLabel.font = infoButton.titleLabel.font = [UIFont appFontOfSize:16];
    [databaseButton setTitle:NSLocalizedString(@"databaseButton", nil) forState:UIControlStateNormal];
    [infoButton setTitle:NSLocalizedString(@"infoButton", nil) forState:UIControlStateNormal];
    
    fingerButton.titleLabel.font = faceButton.titleLabel.font = voiceButton.titleLabel.font = eyeButton.titleLabel.font = [UIFont appLightFontOfSize:19];
    
    [fingerButton setTitle:NSLocalizedString(@"fingerButton", nil) forState:UIControlStateNormal];
    [faceButton setTitle:NSLocalizedString(@"faceButton", nil) forState:UIControlStateNormal];
    [eyeButton setTitle:NSLocalizedString(@"eyeButton", nil) forState:UIControlStateNormal];
    [voiceButton setTitle:NSLocalizedString(@"voiceButton", nil) forState:UIControlStateNormal];
    fingerButton.titleEdgeInsets = faceButton.titleEdgeInsets = eyeButton.titleEdgeInsets = voiceButton.titleEdgeInsets = UIEdgeInsetsMake(95.0, -faceButton.frame.size.width, 0, 0);
    // Do any additional setup after loading the view from its nib.
}
- (void)viewWillAppear:(BOOL)animated{
    [super viewWillAppear:animated];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
