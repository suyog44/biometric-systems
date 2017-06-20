// This is a private class of FaceView. Please, do not use it directly, use FaceView instead.

#import <UIKit/UIKit.h>
#import <NFaceVerification/NFaceVerification.h>

@interface FaceOverlayView : UIView {
    @public
    CGSize imageSize;
    NFloat currentYaw;
    CGRect faceBoundingRect;
    NFaceVerificationLivenessAction livenessAction;
    NFloat livenessTargetYaw;
    NByte livenessScore;
}

- (void)clear;

@end
