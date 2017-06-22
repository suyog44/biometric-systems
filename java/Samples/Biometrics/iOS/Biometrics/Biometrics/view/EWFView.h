#import <UIKit/UIKit.h>
#import <MediaPlayer/MediaPlayer.h>
#import <AVFoundation/AVFoundation.h>

@protocol EWFViewDelegate;

@interface EWFView : UIView
@property (nonatomic, weak) id<EWFViewDelegate> delegate;
@property (nonatomic, strong) NSURL *audioURL;
@property (nonatomic, assign, readonly) unsigned long int samplesCount;
@property (nonatomic, assign) unsigned long int zoomStartSamplesCount;
@property (nonatomic, assign) unsigned long int zoomEndSamplesCount;
@property (nonatomic, assign) unsigned long int progressSamplesCount;
@property (nonatomic) BOOL allowStretchAndScroll;
@property (nonatomic) BOOL allowScrubbing;
@property (nonatomic, copy) UIColor *getProgressColor;
@property (nonatomic, copy) UIColor *getWavesColor;
@end

@protocol EWFViewDelegate <NSObject>
@optional
- (void)waveformViewWasSupported:(EWFView *)waveformView;
- (void)waveformViewIsSupported:(EWFView *)waveformView;
@end
