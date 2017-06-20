#import "SettingsSliderCell.h"

@implementation SettingsSliderCell
@synthesize label, slider, topLine, bottomLine, middleLine, minValueLabel, maxValueLabel, valueLabel;

- (void)awakeFromNib{
    [super awakeFromNib];
    label.textColor = [UIColor getSettingsDarkTextColor];
    label.font = [UIFont appFontOfSize:16];
    topLine.backgroundColor = middleLine.backgroundColor = bottomLine.backgroundColor = [UIColor getDBLineColor];
    [slider setThumbImage:[UIImage imageNamed:@"slider_handle.png"] forState:UIControlStateNormal];
    [slider setMinimumTrackImage:[UIBackgroundImages getSliderMinimumTrack] forState:UIControlStateNormal];
    [slider setMaximumTrackImage:[UIBackgroundImages getSliderMaximumTrack] forState:UIControlStateNormal];

    valueLabel = [[UILabel alloc] initWithFrame:CGRectMake(0, 0, 0, 0)];
    [self addSubview:valueLabel];
    valueLabel.textAlignment = NSTextAlignmentCenter;
    minValueLabel.textColor = maxValueLabel.textColor = valueLabel.textColor = [UIColor getSettingsDarkTextColor];
    minValueLabel.font = maxValueLabel.font = valueLabel.font = [UIFont appFontOfSize:10];
    [slider addTarget:self action:@selector(sliderValueChanged:) forControlEvents:UIControlEventValueChanged];
}
-(void)sliderValueChanged:(id)arg{
    if (handler.type == N_TYPE_OF(NTemplateSize)){
        slider.value = roundf(slider.value);
        switch ((int)slider.value) {
            case 0:{
                valueLabel.text = NSLocalizedString(@"ntsCompact", nil);
                [handler setIntProperty:ntsCompact];
            }
                break;
            case 1:{
                valueLabel.text = NSLocalizedString(@"ntsSmall", nil);
                [handler setIntProperty:ntsSmall];
            }
                break;
            case 2:{
                valueLabel.text = NSLocalizedString(@"ntsMedium", nil);
                [handler setIntProperty:ntsMedium];
            }
                break;
            case 3:{
                valueLabel.text = NSLocalizedString(@"ntsLarge", nil);
                [handler setIntProperty:ntsLarge];
            }
                break;
            default:
                break;
        }
    } else if (handler.type ==  N_TYPE_OF(NMatchingSpeed)){
        slider.value = roundf(slider.value);
        switch ((int)slider.value) {
            case 0:{
                valueLabel.text = NSLocalizedString(@"nmsLow", nil);
                [handler setIntProperty:nmsLow];
            }
                break;
            case 1:{
                valueLabel.text = NSLocalizedString(@"nmsMedium", nil);
                [handler setIntProperty:nmsMedium];
            }
                break;
            case 2:{
                valueLabel.text = NSLocalizedString(@"nmsHigh", nil);
                [handler setIntProperty:nmsHigh];
            }
                break;
            default:
                break;
        }
    } else if ([handler.key isEqualToString:@"Matching.Threshold"]){
        slider.value = ((int)(slider.value/12))*12;
        valueLabel.text = [self matchingThresholdToString:(int)slider.value];
        [handler setIntProperty:(int)slider.value];
        
    } else if (handler.type == N_TYPE_OF(NSingle)){
        valueLabel.text = [NSString stringWithFormat:@"%.2f", slider.value];
        [handler setFloatProperty:slider.value];
    } else {
        valueLabel.text = [NSString stringWithFormat:@"%i", (int)slider.value];
        slider.value = (int)slider.value;
        [handler setIntProperty:(int)slider.value];
    }
    
    
    [valueLabel sizeToFit];
    
//    float thumbCenterX = [self xPositionFromSliderValue:slider];
    CGRect frame = valueLabel.frame;
    frame.origin.y = minValueLabel.frame.origin.y;
    frame.size.height = minValueLabel.frame.size.height;
//    frame.origin.x = thumbCenterX-(frame.size.width/2);
    frame.origin.x = slider.frame.size.width/2-(frame.size.width/2);
//    float minDistance = 0;
//    if (minValueLabel.frame.origin.x+minValueLabel.frame.size.width+minDistance >= frame.origin.x){
//        frame.origin.x = minValueLabel.frame.origin.x+minValueLabel.frame.size.width+minDistance;
//    }
//    if (maxValueLabel.frame.origin.x-minDistance <= frame.origin.x+frame.size.width){
//        frame.origin.x = maxValueLabel.frame.origin.x-minDistance - frame.size.width;
//    }
    
    valueLabel.frame = frame;
}
- (float)xPositionFromSliderValue:(UISlider *)aSlider;
{
    float sliderRange = aSlider.frame.size.width - aSlider.currentThumbImage.size.width;
    float sliderOrigin = aSlider.frame.origin.x + (aSlider.currentThumbImage.size.width / 2.0);
    
    float sliderValueToPixels = ((((aSlider.value-aSlider.minimumValue)/(aSlider.maximumValue-aSlider.minimumValue)) * sliderRange)+sliderOrigin);
    
    return sliderValueToPixels;
}
-(void)prepareForReuse{
    [super prepareForReuse];
    label.text = @"";
    minValueLabel.text =  @"";
    maxValueLabel.text = @"";
}

+(float)heightForCell{
    return 70;
}
-(NSString*)matchingThresholdToString:(int)value{
    double p = -value / 12.0;
    return [NSString stringWithFormat:@"%g%%", pow(10, p+2)];
}
-(void)setupWithSettingHandler:(SettingHandler*)handlerArg andType:(UICollectionViewCellType)type{
    handler = handlerArg;
    label.text = handler.title;
    slider.minimumValue = handler.minimumValue;
    slider.maximumValue = handler.maximumValue;
    if (handler.type == N_TYPE_OF(NTemplateSize)){
        minValueLabel.text = NSLocalizedString(@"ntsCompact", nil);
        maxValueLabel.text = NSLocalizedString(@"ntsLarge", nil);
        switch ([handler getIntProperty]) {
            case ntsCompact:
                slider.value = 0;
                break;
            case ntsSmall:
                slider.value = 1;
                break;
            case ntsMedium:
                slider.value = 2;
                break;
            case ntsLarge:
                slider.value = 3;
                break;
            default:
                break;
        }
    } else if (handler.type == N_TYPE_OF(NMatchingSpeed)){
        minValueLabel.text = NSLocalizedString(@"nmsLow", nil);
        maxValueLabel.text = NSLocalizedString(@"nmsHigh", nil);
        switch ([handler getIntProperty]) {
            case nmsLow:
                slider.value = 0;
                break;
            case nmsMedium:
                slider.value = 1;
                break;
            case nmsHigh:
                slider.value = 2;
                break;
            default:
                break;
        }
    } else if ([handler.key isEqualToString:@"Matching.Threshold"]){
        slider.value = [handler getIntProperty];
        minValueLabel.text = [self matchingThresholdToString:(int)slider.minimumValue];
        maxValueLabel.text = [self matchingThresholdToString:(int)slider.maximumValue];
        
    } else if (handler.type == N_TYPE_OF(NSingle)){
        slider.value = [handler getFloatProperty];
        minValueLabel.text = [NSString stringWithFormat:@"%i", (int)slider.minimumValue];
        maxValueLabel.text = [NSString stringWithFormat:@"%i", (int)slider.maximumValue];
    } else {
        slider.value = [handler getIntProperty];
        minValueLabel.text = [NSString stringWithFormat:@"%i", (int)slider.minimumValue];
        maxValueLabel.text = [NSString stringWithFormat:@"%i", (int)slider.maximumValue];
    }
    [self sliderValueChanged:slider];
    
    switch (type) {
        case UICollectionViewCellTypeFirst:
        {
            topLine.hidden = false;
            bottomLine.hidden = true;
            middleLine.hidden = false;
        }
            break;
        case UICollectionViewCellTypeFirstAndLast:
        {
            topLine.hidden = false;
            bottomLine.hidden = false;
            middleLine.hidden = true;
        }
            break;
        case UICollectionViewCellTypeLast:
        {
            topLine.hidden = true;
            bottomLine.hidden = false;
            middleLine.hidden = true;
        }
            break;
        case UICollectionViewCellTypeMiddle:
        {
            topLine.hidden = true;
            bottomLine.hidden = true;
            middleLine.hidden = false;
        }
            break;
            
        default:
            break;
    }

}

@end
