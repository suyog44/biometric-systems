#import <UIKit/UIKit.h>
#import <NFaceVerification/NFaceVerification.h>

@interface FaceView : UIView

- (void)setFaceImage:(UIImage*)image;
- (void)setCurrentYaw:(NFloat)yaw;
- (void)setFaceBoundingRect:(CGRect)rect;
- (void)setLivenessAction:(NFaceVerificationLivenessAction)action;
- (void)setLivenessTargetYaw:(NFloat)yaw;
- (void)setLivenessScore:(NByte)score;
- (void)repaintOverlay;
- (void)clearOverlay;

@end

