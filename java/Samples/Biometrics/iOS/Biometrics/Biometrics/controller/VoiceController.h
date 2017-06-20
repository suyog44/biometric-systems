#import <UIKit/UIKit.h>
#import <AVFoundation/AVFoundation.h>
#import "SoundWaveView.h"
#import "EWFView.h"
#import "Voice.h"
#import "EnrollDialog.h"
#import "DatabaseController.h"
#import "VoicesCollectionViewDelegate.h"
#import "NavigationController.h"

@interface VoiceController : UIViewController<AVAudioRecorderDelegate, AVAudioPlayerDelegate, BiometricsLayerDelegate, EnrollDialogDelegate, DatabaseControllerDelegate>{
    AVAudioRecorder *audioRecorder;
    AVAudioPlayer *audioPlayer;
    NSTimer *audioRecorderTimer;
    float lowPassResults;
    BOOL firstLaunch;
    Voice *voice;
    Voice *lastVerificationVoice;
    Voice *thirdStageVoice;
}

@property (nonatomic, retain) IBOutlet SoundWaveView *soundWave;

@property (nonatomic, retain) IBOutlet UIView *firstStageTopView;
@property (nonatomic, retain) IBOutlet UIView *firstStageBottomView;

@property (nonatomic, retain) IBOutlet UIButton *recordButton;
@property (nonatomic, retain) IBOutlet UILabel *recordTimeLabel;
@property (nonatomic, retain) IBOutlet UILabel *recordTitleLabel;

@property (nonatomic, retain) IBOutlet UILabel *secondStageTimeLabel;
@property (nonatomic, retain) IBOutlet UILabel *secondStageTitleLabel;
@property (nonatomic, retain) IBOutlet UIView *secondStageView;
@property (nonatomic, retain) IBOutlet UIButton *secondStagePlayButton;
@property (nonatomic, retain) IBOutlet EWFView *secondStageWaveformView;

@property (nonatomic, retain) IBOutlet UIView *actionBottom;
@property (nonatomic, retain) IBOutlet UIButton *actionBack;
@property (nonatomic, retain) IBOutlet UIButton *actionVerify;
@property (nonatomic, retain) IBOutlet UIButton *actionEnroll;
@property (nonatomic, retain) IBOutlet UIButton *actionIdentify;

@property (nonatomic, retain) IBOutlet UIButton *resultBackButton;
@property (nonatomic, retain) IBOutlet UIView *resultBottom;
@property (nonatomic, retain) IBOutlet UIView *thirdStageTopView;
@property (nonatomic, retain) IBOutlet UILabel *thirdStageLabel;
@property (nonatomic, retain) IBOutlet UIButton *thirdStageLeftPlayButton;
@property (nonatomic, retain) IBOutlet UIButton *thirdStageRightPlayButton;
@property (nonatomic, retain) IBOutlet EWFView *thirdStageLeftFormview;
@property (nonatomic, retain) IBOutlet EWFView *thirdStageRightFormview;

@end
