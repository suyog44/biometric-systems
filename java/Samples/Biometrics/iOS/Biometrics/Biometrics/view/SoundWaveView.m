#import "SoundWaveView.h"
#define MIN_VALUE 0
#define MAX_VALUE 1

@implementation SoundWaveView

- (void)awakeFromNib{
    values = [NSMutableArray array];
    [super awakeFromNib];
}
- (void)addValue:(double)value{
    [values addObject:[NSNumber numberWithDouble:fabs(value)]];
    [self setNeedsDisplay];
}
- (void)cleanValues{
    [values removeAllObjects];
    [self setNeedsDisplay];
}
- (void)drawRect:(CGRect)rect {
    [super drawRect:rect];
    
    CGContextRef context = UIGraphicsGetCurrentContext();
    CGContextSetStrokeColorWithColor(context, [UIColor whiteColor].CGColor);
    
    // Draw them with a 2.0 stroke width so they are a bit more visible.
    CGContextSetLineWidth(context, 1.0f);
    
    CGContextMoveToPoint(context, 0.0f, self.frame.size.height/2); //start at this point
    
    CGContextAddLineToPoint(context, self.frame.size.width, self.frame.size.height/2); //draw to this point
    
    // and now draw the Path!
    float x = self.frame.size.width/2;
    for (long i = values.count-1; i >= 0; i--){
        if (x == 0) break;
        x--;
        if (i % 2 == 0) x--;
        double value = [[values objectAtIndex:i] doubleValue];
        float percent = (100.0f/(MAX_VALUE-MIN_VALUE)) * value;
        if (percent < 0) percent = 0;
        if (percent > 100) percent = 100;
        float size = (100/self.frame.size.height) * percent;
        CGContextMoveToPoint(context, x, (self.frame.size.height-size)/2);
        CGContextAddLineToPoint(context, x, (self.frame.size.height-size)/2+size);
    }
    
    CGContextStrokePath(context);
}

@end
