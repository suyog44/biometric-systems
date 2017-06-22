#import "SettingHandler.h"

@implementation SettingHandler
@synthesize type, title, key, maximumValue, minimumValue, checkForDublicates, defaultDPI, boolANDRelation;

-(id)initWithTitle:(NSString*)titleArg key:(NSString*)keyArg type:(NTypeOfProc)typeArg{
    if (self == [super init]){
        title = titleArg;
        key = keyArg;
        type = typeArg;
        if (type == N_TYPE_OF(NMatchingSpeed)){
            minimumValue = 0;
            maximumValue = 2;
        } else if (type == N_TYPE_OF(NTemplateSize)){
            minimumValue = 0;
            maximumValue = 3;
        }
    }
    return self;
}
-(void)setMinimum:(float)min andMaximum:(float)max{
    minimumValue = min;
    maximumValue = max;
}
- (BOOL)isPropertySet{
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    return [defaults objectForKey:self.key] != nil;
}
-(NBoolean)getBoolProperty{
    if ([self isPropertySet]){
        NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
        return [[defaults objectForKey:self.key] boolValue];
    }
    NBoolean value;
    if ([key  isEqual: @"Enrollment.CheckForDublicates"])
        value = checkForDublicates;
    else
    {
        NBoolean hasValue = 0;
        NSLog(@"getBoolProperty:%i", NObjectGetPropertyP([[BiometricsLayer sharedInstance] getBiometricClient], [key cStringUsingEncoding:NSUTF8StringEncoding], type, naNone, &value, sizeof(value), 1, &hasValue));
    }
    return value;
}
-(int)getIntProperty{
    if ([self isPropertySet]){
        NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
        return [[defaults objectForKey:self.key] intValue];
    }
    int value;
    NBoolean hasValue = 0;
    NSLog(@"getIntProperty:%i", NObjectGetPropertyP([[BiometricsLayer sharedInstance] getBiometricClient], [key cStringUsingEncoding:NSUTF8StringEncoding], type, naNone, &value, sizeof(value), 1, &hasValue));
    return value;
}
-(float)getFloatProperty{
    if ([self isPropertySet]){
        NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
        return [[defaults objectForKey:self.key] floatValue];
    }
    float value;
    if ([key isEqual:@"Fingers.DefaultDPI"])
        value = 500;
    else
    {
        NBoolean hasValue = 0;
        NSLog(@"getFloatProperty:%i", NObjectGetPropertyP([[BiometricsLayer sharedInstance] getBiometricClient], [key cStringUsingEncoding:NSUTF8StringEncoding], type, naNone, &value, sizeof(value), 1, &hasValue));
    }
    return value;
}
-(BOOL)setBoolProperty:(NBoolean)value{
    if (boolANDRelation!=nil){
        if (![boolANDRelation getBoolProperty] && !value) return false;
    }
    if ([key  isEqual: @"Enrollment.CheckForDublicates"])
        checkForDublicates = value;
    else
        NSLog(@"setBoolProperty:%i", NObjectSetPropertyP([[BiometricsLayer sharedInstance] getBiometricClient], [key cStringUsingEncoding:NSUTF8StringEncoding], type, naNone, &value, sizeof(value), 1, NTrue));
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    [defaults setObject:[NSNumber numberWithBool:value] forKey:self.key];
    [defaults synchronize];
    return true;
}
-(void)setIntProperty:(int)value{
    NSLog(@"setIntProperty:%i", NObjectSetPropertyP([[BiometricsLayer sharedInstance] getBiometricClient], [key cStringUsingEncoding:NSUTF8StringEncoding], type, naNone, &value, sizeof(value), 1, NTrue));
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    [defaults setObject:[NSNumber numberWithInt:value] forKey:self.key];
    [defaults synchronize];
}
-(void)setFloatProperty:(float)value{
    if ([key isEqual: @"Fingers.DefaultDPI"])
        defaultDPI = value;
    else
        NSLog(@"setFloatProperty:%i", NObjectSetPropertyP([[BiometricsLayer sharedInstance] getBiometricClient], [key cStringUsingEncoding:NSUTF8StringEncoding], type, naNone, &value, sizeof(value), 1, NTrue));
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    [defaults setObject:[NSNumber numberWithFloat:value] forKey:self.key];
    [defaults synchronize];
}

@end
