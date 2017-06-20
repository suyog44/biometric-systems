#import "VoiceController.h"
#import "SettingsController.h"

@interface VoiceController ()

@end

@implementation VoiceController

@synthesize soundWave, recordButton, recordTimeLabel, recordTitleLabel, firstStageBottomView, firstStageTopView, secondStageTimeLabel, secondStageTitleLabel, secondStageView, secondStageWaveformView, actionBack, actionBottom, actionEnroll, actionIdentify, actionVerify, resultBackButton, resultBottom, thirdStageTopView, thirdStageLabel, thirdStageLeftFormview, thirdStageRightFormview, secondStagePlayButton, thirdStageLeftPlayButton, thirdStageRightPlayButton;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
        self.title = NSLocalizedString(@"voiceTitle", nil);
        firstLaunch = true;
        self.navigationItem.rightBarButtonItem = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"settings.png"] style:UIBarButtonItemStylePlain target:self action:@selector(settingsClicked:)];
    }
    return self;
}

-(IBAction)settingsClicked:(id)arg{
    UIViewController *viewController = [[SettingsController alloc] initWithNibName:@"SettingsController" bundle:nil andSettingsHandlers:[SettingsHandler makeVoiceSettingsHandlers] andTitle:NSLocalizedString(@"settingsTitle", nil)];
    [self.navigationController pushViewController:viewController animated:true];
}
-(IBAction)playButtonClicked:(UIButton *)arg{
    NSString *playerPath;
    if(arg == secondStagePlayButton || arg == thirdStageLeftPlayButton){
        playerPath = audioRecorder.url.path;
    }
    if (arg == thirdStageRightPlayButton){
        playerPath = [thirdStageVoice getAudioFilePath];
    }
    if (![audioPlayer.url.path isEqualToString:playerPath] && playerPath != nil){
        [audioPlayer stop];
        audioPlayer = [[AVAudioPlayer alloc] initWithContentsOfURL:[[NSURL alloc] initFileURLWithPath:playerPath] error:nil];
        audioPlayer.delegate = self;
        [audioPlayer prepareToPlay];
        [audioPlayer setVolume:1.0f];
    }
    if (arg.selected){
        [self updatePlayerButtons];
        [audioPlayer pause];
        arg.selected = false;
    } else {
        [self updatePlayerButtons];
        [audioPlayer play];
        arg.selected = true;
    }
}
-(void)updatePlayerButtons{
    secondStagePlayButton.selected = thirdStageLeftPlayButton.selected = thirdStageRightPlayButton.selected = false;
}

-(void)resultBackClicked:(id)arg{
    if (voice == nil){
        [self setupViewForFirstStage];
    }  else{
        [self setupViewForSecondStage];
    }
}
-(void)identifyClicked:(id)arg{
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderIdentifying", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] identifySubject:voice.subject];
}
- (void)enrollClicked:(id)arg{
    EnrollDialog *popup = [[[NSBundle mainBundle] loadNibNamed:@"EnrollDialog" owner:self options:nil] objectAtIndex:0];
    popup.delegate = self;
    [popup presentDialogWithTitle:NSLocalizedString(@"enrollTitle", nil) placeholder:NSLocalizedString(@"enrollPlaceholder", nil) cancelString:NSLocalizedString(@"cancelButton", nil) acceptString:NSLocalizedString(@"enrollButton", nil) view:self.view.window];
}
-(void)verifyClicked:(id)arg{
    DatabaseController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[VoicesCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbChooseVoice", nil)];
    viewController.delegate = self;
    NavigationController *nav = [[NavigationController alloc] initWithRootViewController:viewController];
    [self presentViewController:nav animated:true completion:^{
        
    }];
}
- (void)enrollDialog:(EnrollDialog *)dialog acceptedWithValue:(NSString *)value{
    @try {
        int status = [[BiometricsLayer sharedInstance] enrollSubject:voice.subject withId:value andPath:audioRecorder.url.path withPrefix:PREFIX_VOICE];
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

- (void)viewDidLoad
{
    [super viewDidLoad];
    self.view.backgroundColor = [UIColor getViewControllerBgColor];
    [self prepareRecorder];
    

    recordTimeLabel.font = [UIFont appThinFontOfSize:36];
    recordTitleLabel.font = [UIFont appFontOfSize:16];
    recordTitleLabel.textColor = recordTimeLabel.textColor = [UIColor whiteColor];
    recordTitleLabel.text = NSLocalizedString(@"voiceRecord", nil);
    
    secondStageTitleLabel.font = [UIFont appFontOfSize:16];
    secondStageTimeLabel.font = [UIFont appThinFontOfSize:36];
    secondStageTimeLabel.textColor = secondStageTitleLabel.textColor = [UIColor whiteColor];
    secondStageTitleLabel.text = NSLocalizedString(@"voiceExtractSuccess", nil);
    
    thirdStageLabel.font = [UIFont appFontOfSize:16];
    thirdStageLabel.textColor = [UIColor whiteColor];
    
    secondStageWaveformView.getWavesColor = thirdStageLeftFormview.getWavesColor = thirdStageRightFormview.getWavesColor = [UIColor getViewFormColor];
    
    [self updateRecorderView];
    
    secondStageView.backgroundColor = firstStageTopView.backgroundColor = thirdStageTopView.backgroundColor = [UIColor getGrayBgColor];
    
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
    
    secondStagePlayButton.alpha = thirdStageRightPlayButton.alpha = thirdStageLeftPlayButton.alpha = 0.8;
    
      // Do any additional setup after loading the view from its nib.
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
    if ([audioRecorder isRecording]){
        [self setupViewForFirstStage];
    }
    [audioPlayer stop];
    [self updatePlayerButtons];
}
-(void)prepareViewForStage{
    [audioPlayer stop];
    [self updatePlayerButtons];
    firstStageTopView.hidden = true;
    firstStageBottomView.hidden = true;
    secondStageView.hidden = true;
    actionBottom.hidden = true;
    thirdStageTopView.hidden = true;
    resultBottom.hidden = true;
    thirdStageLeftFormview.hidden = true;
    thirdStageRightFormview.hidden = true;
    thirdStageLeftPlayButton.hidden = true;
    thirdStageRightPlayButton.hidden = true;
    if ([audioRecorder isRecording])
        [audioRecorder stop];
}
-(void)setupViewForFirstStage{
    [self prepareViewForStage];
    [soundWave cleanValues];
    recordTimeLabel.text = @"00:00";
    firstStageTopView.hidden = false;
    firstStageBottomView.hidden = false;
}
-(void)setupViewForRecognitionLoadingStageWithPath:(NSString*)path{
    [self prepareViewForStage];
    firstStageTopView.hidden = false;
    firstStageBottomView.hidden = false;
    [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderExtracting", nil)];
    [BiometricsLayer sharedInstance].delegate = self;
    [[BiometricsLayer sharedInstance] extractVoice:path];
}
-(void)setupViewForSecondStage{
    if (voice == nil){
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"extractFail", nil)];
        return;
    }
    [self prepareViewForStage];
    actionBottom.hidden = false;
    secondStageView.hidden = false;
    secondStageWaveformView.audioURL = audioRecorder.url;
}
-(void)setupViewForThirdStage:(Voice*)voiceArg andFailText:(NSString*)failText{
    [self prepareViewForStage];
    thirdStageVoice = voiceArg;
    resultBottom.hidden = false;
    thirdStageTopView.hidden = false;
    
    NSDictionary * redAttributes =
    @{ NSForegroundColorAttributeName : [UIColor getAppRedColor],
       NSFontAttributeName            : [UIFont appFontOfSize:18] };
    NSDictionary * blueAttributes =
    @{ NSForegroundColorAttributeName : [UIColor getAppBlueColor],
       NSFontAttributeName            : [UIFont appFontOfSize:18] };
    NSDictionary * whiteAttributes =
    @{ NSForegroundColorAttributeName : [UIColor whiteColor],
       NSFontAttributeName            : [UIFont appFontOfSize:18] };
    if (voiceArg != nil){
        thirdStageLeftFormview.audioURL = audioRecorder.url;
        thirdStageRightFormview.audioURL = [[NSURL alloc] initFileURLWithPath:[voiceArg getAudioFilePath]];
        
        thirdStageLeftFormview.hidden = false;
        thirdStageRightFormview.hidden =false;
        thirdStageLeftPlayButton.hidden=  false;
        thirdStageRightPlayButton.hidden = false;
//        
        NSString *name = voiceArg.name.length>0?voiceArg.name:@"";
        NSString *text = [NSString stringWithFormat:NSLocalizedString(@"voiceIdentifySuccess", nil), voiceArg.name];
        
        NSMutableAttributedString *attributedString = [[NSMutableAttributedString alloc] initWithString:text attributes:whiteAttributes];
        [attributedString setAttributes:blueAttributes range:[text rangeOfString:name]];
        thirdStageLabel.attributedText = attributedString;
    } else {
        thirdStageLabel.attributedText = [[NSAttributedString alloc] initWithString:failText attributes:redAttributes];
    }
}

//Recorder
-(void)prepareRecorder{
    NSArray *dirPaths;
    NSString *docsDir;
    
    dirPaths = NSSearchPathForDirectoriesInDomains(
                                                   NSDocumentDirectory, NSUserDomainMask, YES);
    docsDir = dirPaths[0];
    
    NSString *soundFilePath = [docsDir
                               stringByAppendingPathComponent:@"sound.wav"];
    
    NSURL *soundFileURL = [NSURL fileURLWithPath:soundFilePath];
    
    NSDictionary *recordSettings = [NSDictionary
                                    dictionaryWithObjectsAndKeys:
                                    [NSNumber numberWithInt:AVAudioQualityMin],
                                    AVEncoderAudioQualityKey,
                                    [NSNumber numberWithInt:16],
                                    AVEncoderBitRateKey,
                                    [NSNumber numberWithInt: 2],
                                    AVNumberOfChannelsKey,
                                    [NSNumber numberWithFloat:44100.0],
                                    AVSampleRateKey,
                                    nil];
    
    NSError *error = nil;
    
    AVAudioSession *audioSession = [AVAudioSession sharedInstance];
    [audioSession setCategory:AVAudioSessionCategoryPlayAndRecord
                        error:nil];
    
    audioRecorder = [[AVAudioRecorder alloc]
                     initWithURL:soundFileURL
                     settings:recordSettings
                     error:&error];
    audioRecorder.delegate = self;
    audioRecorder.meteringEnabled = YES;
    if (error)
    {
        NSLog(@"error: %@", [error localizedDescription]);
    } else {
        [audioRecorder prepareToRecord];
    }
    
}
-(void)updateRecorderView{
    if (audioRecorder.recording){
        recordButton.selected = true;
        recordTimeLabel.textColor = [UIColor getRecorderTimeActiveColor];
        NSDate *date = [NSDate dateWithTimeIntervalSince1970:audioRecorder.currentTime];
        NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
        [formatter setDateFormat:@"mm:ss"];
        recordTimeLabel.text = [formatter stringFromDate:date];
        secondStageTimeLabel.text = recordTimeLabel.text;
    } else {
        recordButton.selected = false;
        recordTimeLabel.textColor = [UIColor whiteColor];
    }
}
- (void)levelTimerCallback:(NSTimer *)timer {
    [audioRecorder updateMeters];
    
    float   level;                // The linear 0.0 .. 1.0 value we need.
    float   minDecibels = -60.0f; // Or use -60dB, which I measured in a silent room.
    float   decibels    = [audioRecorder averagePowerForChannel:0];
    
    if (decibels < minDecibels)
    {
        level = 0.0f;
    }
//    else if (decibels >= 0.0f)
//    {
//        level = 1.0f;
//    }
    else
    {
        float   root            = 2.0f;
        float   minAmp          = powf(10.0f, 0.05f * minDecibels);
        float   inverseAmpRange = 1.0f / (1.0f - minAmp);
        float   amp             = powf(10.0f, 0.05f * decibels);
        float   adjAmp          = (amp - minAmp) * inverseAmpRange;
        
        level = powf(adjAmp, 1.0f / root);
    }
    
    NSLog(@"%f", level);
    [soundWave addValue:level];
    [self updateRecorderView];
}
-(IBAction)recordButtonClicked:(id)sender{
    if (!audioRecorder.recording){
        [audioRecorder record];
        [soundWave cleanValues];
        audioRecorderTimer = [NSTimer scheduledTimerWithTimeInterval: 0.025 target: self selector: @selector(levelTimerCallback:) userInfo: nil repeats: YES];
        [self updateRecorderView];
    } else {
        [audioRecorder stop];
    }
}
//AudioRecorderDelegate
- (void)audioPlayerDidFinishPlaying:(AVAudioPlayer *)player successfully:(BOOL)flag{
    [self updatePlayerButtons];
}
-(void)audioPlayerDecodeErrorDidOccur:(AVAudioPlayer *)player error:(NSError *)error
{
    NSLog(@"Decode Error occurred");
}

-(void)audioRecorderDidFinishRecording:(AVAudioRecorder *)recorder successfully:(BOOL)flag
{
    if (flag){
        [self setupViewForRecognitionLoadingStageWithPath:recorder.url.path];
    }
    [audioRecorderTimer invalidate];
    [self updateRecorderView];
}

-(void)audioRecorderEncodeErrorDidOccur:(AVAudioRecorder *)recorder error:(NSError *)error
{
    [audioRecorderTimer invalidate];
    [self updateRecorderView];
    NSLog(@"Encode Error occurred");
}
//Choose face dialog
- (void)databaseController:(DatabaseController *)databaseViewController didChooseVoice:(Voice *)voiceArg{
    [databaseViewController dismissViewControllerAnimated:true completion:^{
        [[ProgressView sharedInstance] startOverAppWithText:NSLocalizedString(@"loaderVerifying", nil)];
        [BiometricsLayer sharedInstance].delegate = self;
        lastVerificationVoice = voiceArg;
        [[BiometricsLayer sharedInstance] verifySubject:voice.subject withSubject:lastVerificationVoice.subject];
    }];
}

//BiometricsLayer Delegate
-(void)biometricsLayer:(BiometricsLayer *)bl didFinishExtractingSubject:(HNSubject)subject withSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    if (success){
        voice = [[Voice alloc] initWithSubject:subject];
    }
    [self setupViewForSecondStage];
}
-(void)biometricsLayer:(BiometricsLayer *)bl didFinishIdentifyingSubject:(HNSubject)subject withSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    Voice *tmpVoice = nil;
    if (success){
        tmpVoice = [[Voice alloc] initWithSubject:subject];
        NSLog(@"identifySuccess:%@", tmpVoice.name);
    }
    [self setupViewForThirdStage:tmpVoice andFailText:NSLocalizedString(@"identifyFail", nil)];
}
- (void)biometricsLayer:(BiometricsLayer *)bl didFinishVerifyingWithSuccess:(BOOL)success{
    [[ProgressView sharedInstance] stop];
    if (success){
        [self setupViewForThirdStage:lastVerificationVoice andFailText:NSLocalizedString(@"verifyFail", nil)];
    } else {
        [self setupViewForThirdStage:nil andFailText:NSLocalizedString(@"verifyFail", nil)];
    }
}

@end
