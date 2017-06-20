#import "ProgressView.h"
#import "UIMotionEffectHelper.h"

@implementation ProgressView

static ProgressView *sharedInstance;

+ (ProgressView *) sharedInstance
{
	if (sharedInstance == nil)
	{
		sharedInstance = [[self alloc] init];
	}
	return sharedInstance;
}

- (id)init
{
    self = [super init];
    if (self) 
	{
        self.backgroundColor = [UIColor progressViewScreenTint];
        
        contentView = [[UIView alloc] initWithFrame:CGRectMake(0, 0, 150, 60)];
        contentView.backgroundColor = [UIColor progressViewSquareColor];
        contentView.layer.cornerRadius = 5;
        [self addSubview:contentView];
        
        
        spinner = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleWhite];
        spinner.frame = CGRectMake(0, 12, contentView.frame.size.width, 24);
		spinner.hidesWhenStopped = YES;
        spinner.color = [UIColor whiteColor];
		[spinner stopAnimating];
        [contentView addSubview:spinner];
        
        text = [[UILabel alloc] initWithFrame:CGRectMake(10, spinner.frame.origin.y+spinner.frame.size.height, contentView.frame.size.width-20, 20)];
        text.font = [UIFont appFontOfSize:12];
        text.lineBreakMode = NSLineBreakByWordWrapping;
        text.numberOfLines = 0;
        text.textAlignment = NSTextAlignmentCenter;
        text.backgroundColor = [UIColor clearColor];
        text.textColor = [UIColor whiteColor];
        [contentView addSubview:text];
        
        [UIMotionEffectHelper addMotionEffectToView:contentView];
        
    }
    return self;
}

-(void)startInView:(UIView *)view withText:(NSString*)textString
{
	[view addSubview:self];

    self.frame = view.bounds;
    
    CGRect frame = contentView.frame;
    frame.origin.x = (self.frame.size.width-contentView.frame.size.width)/2;
    frame.origin.y = (self.frame.size.height-contentView.frame.size.height)/2;
    contentView.frame = frame;

    text.text = textString;
    

	[spinner startAnimating];

    self.alpha = 0;
    [UIView animateWithDuration:0.1 animations:^{
        self.alpha = 1;
    }];
}

- (void)startOverAppWithText:(NSString*)textString
{
    NSLog(@"startOverApp: %@", textString);
	[self startInView:[[UIApplication sharedApplication].windows objectAtIndex:0] withText:textString];
}

-(void)stop
{
    NSLog(@"stop");
    [UIView animateWithDuration:0.1 animations:^{
        self.alpha = 0;
    } completion:^(BOOL finished) {
        [self removeFromSuperview];
        [spinner stopAnimating];
    }];
}

@end
