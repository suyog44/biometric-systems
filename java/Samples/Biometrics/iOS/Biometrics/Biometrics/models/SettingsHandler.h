#import <Foundation/Foundation.h>
#import "SettingHandler.h"

@interface SettingsHandler : NSObject

@property (nonatomic, retain) NSMutableArray *settingHandlers;
@property (nonatomic, retain) NSString *title;

-(id)initWithTitle:(NSString*)titleArg;

+(NSArray*)makeFaceSettingsHandlers;
+(NSArray*)makeEyeSettingsHandlers;
+(NSArray*)makeVoiceSettingsHandlers;
+(NSArray*)makeFingerSettingsHandlers;

+(SettingHandler*)getCheckForDublicatesHandler;
+(SettingHandler*)getDefaultDPIHandler;

@end
