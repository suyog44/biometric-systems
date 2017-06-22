#import <UIKit/UIKit.h>

@class EnrollDialog;

@protocol EnrollDialogDelegate <NSObject>

-(void)enrollDialog:(EnrollDialog*)dialog acceptedWithValue:(NSString*)value;

@end

@interface EnrollDialog : UIView<UITextFieldDelegate>{
    UIView *contentView;
}

-(void)presentDialogWithTitle:(NSString*)titleArg placeholder:(NSString*)placeHolderArg cancelString:(NSString*)cancelArg acceptString:(NSString*)acceptArg view:(UIView*)view;
-(void)handleStatusCode:(int)status;
-(void)handleException:(NSException*)exception;
-(void)dismissView;

@property (nonatomic, retain) IBOutlet UILabel *titleLabel;
@property (nonatomic, retain) IBOutlet UITextField *textField;
@property (nonatomic, retain) IBOutlet UIButton *cancelButton;
@property (nonatomic, retain) IBOutlet UIButton *acceptButton;
@property (nonatomic, assign) id<EnrollDialogDelegate> delegate;

@end
