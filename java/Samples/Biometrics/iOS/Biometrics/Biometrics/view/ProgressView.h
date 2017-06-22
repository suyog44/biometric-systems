#import <UIKit/UIKit.h>

@interface ProgressView : UIView 
{
    UIView *contentView;
	UIActivityIndicatorView *spinner;
    UILabel *text;
}

- (id) init;
+ (ProgressView *) sharedInstance;

- (void) startInView:(UIView *)view withText:(NSString*)textString;
- (void) startOverAppWithText:(NSString*)textString;
- (void) stop;

@end
