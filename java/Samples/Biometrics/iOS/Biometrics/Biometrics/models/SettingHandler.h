#import <Foundation/Foundation.h>

@interface SettingHandler : NSObject

@property (nonatomic) float minimumValue;
@property (nonatomic) float maximumValue;
@property (nonatomic) NBoolean checkForDublicates;
@property (nonatomic) float defaultDPI;
@property (nonatomic, retain) NSString *title;
@property (nonatomic, retain) NSString *key;
@property (nonatomic) NTypeOfProc type;
@property (nonatomic, assign) SettingHandler *boolANDRelation;

-(id)initWithTitle:(NSString*)titleArg key:(NSString*)keyArg type:(NTypeOfProc)typeArg;
-(void)setMinimum:(float)min andMaximum:(float)max;

-(BOOL)isPropertySet;
-(NBoolean)getBoolProperty;
-(int)getIntProperty;
-(float)getFloatProperty;
-(BOOL)setBoolProperty:(NBoolean)value;
-(void)setIntProperty:(int)value;
-(void)setFloatProperty:(float)value;

@end
