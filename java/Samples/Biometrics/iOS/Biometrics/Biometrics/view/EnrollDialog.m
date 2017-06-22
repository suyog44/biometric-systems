#import "EnrollDialog.h"

#import "UIMotionEffectHelper.h"

#define WIDTH 270
#define HEIGHT 140

@implementation EnrollDialog
@synthesize titleLabel, textField, cancelButton, acceptButton, delegate;

- (void)awakeFromNib{
    [super awakeFromNib];
    // Set vertical effect
    [UIMotionEffectHelper addMotionEffectToView:self];
}

-(void)presentDialogWithTitle:(NSString*)titleArg placeholder:(NSString*)placeHolderArg cancelString:(NSString*)cancelArg acceptString:(NSString*)acceptArg view:(UIView*)view{
    self.backgroundColor = [UIColor getViewControllerBgColor];
    titleLabel.text = titleArg;
    textField.placeholder = placeHolderArg;
    textField.delegate = self;
    textField.backgroundColor = [UIColor whiteColor];
    textField.layer.borderColor = [UIColor getDialogFieldBorderColor].CGColor;    textField.layer.borderWidth = 0.5f;
    
    [textField addTarget:self action:@selector(textFieldDidChange:) forControlEvents:UIControlEventEditingChanged];
    
    [cancelButton setTitle:cancelArg forState:UIControlStateNormal];
    [acceptButton setTitle:acceptArg forState:UIControlStateNormal];
    acceptButton.enabled = false;
    
    textField.textColor = titleLabel.textColor = [UIColor getDialogDarkTextColor];
    [acceptButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [cancelButton setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
    [cancelButton setBackgroundImage:[UIColor imageWithColor:[UIColor getAppRedColor]] forState:UIControlStateNormal];
    [acceptButton setBackgroundImage:[UIColor imageWithColor:[UIColor getAppBlueColor]] forState:UIControlStateNormal];
    titleLabel.font = [UIFont appFontOfSize:18];
    acceptButton.titleLabel.font = cancelButton.titleLabel.font = [UIFont appFontOfSize:14];
    textField.font = [UIFont appFontOfSize:14];
    
    contentView = [[UIView alloc] initWithFrame:CGRectMake(0, 0, view.frame.size.width, view.frame.size.height)];
    [contentView setAutoresizingMask:UIViewAutoresizingFlexibleHeight|UIViewAutoresizingFlexibleWidth];
    contentView.backgroundColor = [UIColor getScreenTintOnModalAppearColor];
    
    self.frame = CGRectMake((contentView.frame.size.width-WIDTH)/2, (contentView.frame.size.height-HEIGHT)/4, WIDTH, HEIGHT);
    [contentView addSubview:self];
    
    self.layer.cornerRadius = acceptButton.layer.cornerRadius = cancelButton.layer.cornerRadius = textField.layer.cornerRadius = 2;
    
    [view addSubview:contentView];
    [self attachPopUpAnimation];
    [textField becomeFirstResponder];
}
- (void)dealloc{
    if (textField.delegate == self) textField.delegate = nil;
}
- (void) attachPopUpAnimation
{
    CAKeyframeAnimation *animation = [CAKeyframeAnimation
                                      animationWithKeyPath:@"transform"];
    
    CATransform3D scale1 = CATransform3DMakeScale(0.5, 0.5, 1);
    CATransform3D scale2 = CATransform3DMakeScale(1.05, 1.05, 1);
    CATransform3D scale3 = CATransform3DMakeScale(0.95, 0.95, 1);
    CATransform3D scale4 = CATransform3DMakeScale(1.0, 1.0, 1);
    
    NSArray *frameValues = [NSArray arrayWithObjects:
                            [NSValue valueWithCATransform3D:scale1],
                            [NSValue valueWithCATransform3D:scale2],
                            [NSValue valueWithCATransform3D:scale3],
                            [NSValue valueWithCATransform3D:scale4],
                            nil];
    [animation setValues:frameValues];
    
    NSArray *frameTimes = [NSArray arrayWithObjects:
                           [NSNumber numberWithFloat:0.0],
                           [NSNumber numberWithFloat:0.5],
                           [NSNumber numberWithFloat:0.9],
                           [NSNumber numberWithFloat:1.0],
                           nil];
    [animation setKeyTimes:frameTimes];
    
    animation.fillMode = kCAFillModeForwards;
    animation.removedOnCompletion = NO;
    animation.duration = .2;
    animation.delegate = (id) self;
    
    [self.layer addAnimation:animation forKey:@"popup"];
}
-(void)dismissView{
    [contentView removeFromSuperview];
}
-(IBAction)buttonClicked:(id)sender{
    if (sender == acceptButton){
        if ([delegate respondsToSelector:@selector(enrollDialog:acceptedWithValue:)]){
            [delegate enrollDialog:self acceptedWithValue:textField.text];
        }
    }
    if (sender == cancelButton){
        [self dismissView];
    }
}
- (void)textFieldDidChange:(id)arg{
    if (textField.text.length >= 3){
        acceptButton.enabled = true;
    } else {
        acceptButton.enabled = false;
    }
}
-(void)handleStatusCode:(int)status{
    if (status != 0){
        NSString *title = NSLocalizedString(@"enrollErrorAlertTitle", nil);
        NSString *text = @"";
        NSString *button = NSLocalizedString(@"enrollErrorAlertButton", nil);
        if (status == 611){
            text = NSLocalizedString(@"dublicateEnrollError", nil);
        } else if (status == 602){
            text = NSLocalizedString(@"dublicateIdEnrollError", nil);
        } else {
            text = NSLocalizedString(@"unknownEnrollError", nil);
        }
        [[[UIAlertView alloc] initWithTitle:title message:text delegate:nil cancelButtonTitle:button otherButtonTitles: nil] show];
    }
}
-(void)handleException:(NSException*)exception {
    NSString *button = NSLocalizedString(@"enrollErrorAlertButton", nil);
    [[[UIAlertView alloc] initWithTitle:[exception name] message:[exception reason] delegate:nil cancelButtonTitle:button otherButtonTitles: nil] show];
}

@end
