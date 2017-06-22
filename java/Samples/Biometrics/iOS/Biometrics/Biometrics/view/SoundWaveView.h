#import <UIKit/UIKit.h>

@interface SoundWaveView : UIView{
    NSMutableArray *values;
}
-(void)addValue:(double)value;
-(void)cleanValues;

@end
