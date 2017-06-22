#import <UIKit/UIKit.h>
#import <AVFoundation/AVFoundation.h>

@class CameraView;
@protocol CameraViewDelegate <NSObject>

-(void)cameraView:(CameraView*)cameraView didTakePicture:(UIImage*)image;
@end

@interface CameraView : UIView{
    BOOL canTakePhotos;
    
    UIBackgroundTaskIdentifier backgroundRecordingID;
    AVCaptureStillImageOutput *stillImageOutput;
    UITapGestureRecognizer *gesture;
}
@property (nonatomic) AVCaptureSession *session;
@property (nonatomic, retain) dispatch_queue_t sessionQueue;
@property (nonatomic) id runtimeErrorHandlingObserver;
@property (nonatomic) AVCaptureDeviceInput *videoDeviceInput;
@property (nonatomic, assign) id<CameraViewDelegate> delegate;

-(void)prepareCamera;
-(void)startCamera;
-(void)pauseCamera;
+ (AVCaptureDevice *)deviceWithMediaType:(NSString *)mediaType preferringPosition:(AVCaptureDevicePosition)position;
@end
