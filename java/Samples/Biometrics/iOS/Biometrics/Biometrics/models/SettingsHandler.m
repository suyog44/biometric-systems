#import "SettingsHandler.h"

@implementation SettingsHandler
@synthesize title, settingHandlers;

-(id)initWithTitle:(NSString*)titleArg{
    if (self = [super init]){
        title = titleArg;
        settingHandlers = [NSMutableArray array];
    }
    return self;
}

+(NSArray*)makeFaceSettingsHandlers{
    SettingHandler *handler;
    
    SettingsHandler *extraction = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsExtraction", nil)];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.TemplateSize",nil) key:@"Faces.TemplateSize" type:N_TYPE_OF(NTemplateSize)];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.ConfidenceThreshold",nil) key:@"Faces.ConfidenceThreshold" type:N_TYPE_OF(NInt32)];
    [handler setMinimum:0 andMaximum:100];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.MinimalInterOcularDistance",nil) key:@"Faces.MinimalInterOcularDistance" type:N_TYPE_OF(NInt32)];
    [handler setMinimum:8 andMaximum:16384];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.MaximalRoll",nil) key:@"Faces.MaximalRoll" type:N_TYPE_OF(NSingle)];
    [handler setMinimum:0 andMaximum:180];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.MaximalYaw",nil) key:@"Faces.MaximalYaw" type:N_TYPE_OF(NSingle)];
    [handler setMinimum:0 andMaximum:90];
    [extraction.settingHandlers addObject:handler];
    
    SettingsHandler *matching = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsMatching", nil)];
    
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.MatchingSpeed",nil) key:@"Faces.MatchingSpeed" type:N_TYPE_OF(NMatchingSpeed)];
    [matching.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Matching.Threshold",nil) key:@"Matching.Threshold" type:N_TYPE_OF(NInt32)];
    [handler setMinimum:12 andMaximum:72];
    [matching.settingHandlers addObject:handler];
    
    SettingsHandler *advanced = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsAdvanced", nil)];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.DetectAllFeaturePoints",nil) key:@"Faces.DetectAllFeaturePoints" type:N_TYPE_OF(NBoolean)];
    [advanced.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.DetectBaseFeaturePoints",nil) key:@"Faces.DetectBaseFeaturePoints" type:N_TYPE_OF(NBoolean)];
    [advanced.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.DetermineGender",nil) key:@"Faces.DetermineGender" type:N_TYPE_OF(NBoolean)];
    [advanced.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.DetectProperties",nil) key:@"Faces.DetectProperties" type:N_TYPE_OF(NBoolean)];
    [advanced.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.RecognizeExpression",nil) key:@"Faces.RecognizeExpression" type:N_TYPE_OF(NBoolean)];
    [advanced.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.RecognizeEmotion",nil) key:@"Faces.RecognizeEmotion" type:N_TYPE_OF(NBoolean)];
    [advanced.settingHandlers addObject:handler];
    
    
    SettingsHandler *thumbnail = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsThumbnail", nil)];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.CreateThumbnailImage",nil) key:@"Faces.CreateThumbnailImage" type:N_TYPE_OF(NBoolean)];
    [thumbnail.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Faces.TokenImageWidth",nil) key:@"Faces.TokenImageWidth" type:N_TYPE_OF(NUInt32)];
    [handler setMinimum:240 andMaximum:24000];
    [thumbnail.settingHandlers addObject:handler];
    
    SettingsHandler *enrollment = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsEnrollment", nil)];
    handler = [self getCheckForDublicatesHandler];
    [enrollment.settingHandlers addObject:handler];
    return [NSArray arrayWithObjects:extraction, matching, advanced, thumbnail, enrollment, nil];
}
+(NSArray*)makeEyeSettingsHandlers{
    SettingHandler *handler;
    
    SettingsHandler *extraction = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsExtraction", nil)];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Irises.TemplateSize",nil) key:@"Irises.TemplateSize" type:N_TYPE_OF(NTemplateSize)];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Irises.FastExtraction",nil) key:@"Irises.FastExtraction" type:N_TYPE_OF(NBoolean)];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Irises.QualityThreshold",nil) key:@"Irises.QualityThreshold" type:N_TYPE_OF(NUInt32)];
    [handler setMinimum:0 andMaximum:100];
    [extraction.settingHandlers addObject:handler];
    
    SettingsHandler *matching = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsMatching", nil)];
    
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Irises.MatchingSpeed",nil) key:@"Irises.MatchingSpeed" type:N_TYPE_OF(NMatchingSpeed)];
    [matching.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Irises.MaximalRotation",nil) key:@"Irises.MaximalRotation" type:N_TYPE_OF(NSingle)];
    [handler setMinimum:0 andMaximum:180];
    [matching.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Matching.Threshold",nil) key:@"Matching.Threshold" type:N_TYPE_OF(NInt32)];
    [handler setMinimum:12 andMaximum:72];
    [matching.settingHandlers addObject:handler];
    
    SettingsHandler *enrollment = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsEnrollment", nil)];
    handler = [self getCheckForDublicatesHandler];
    [enrollment.settingHandlers addObject:handler];

    return [NSArray arrayWithObjects:extraction, matching, enrollment, nil];
}
+(NSArray*)makeVoiceSettingsHandlers{
    SettingHandler *handler;
    SettingHandler *textIndependent;
    SettingHandler *textDependent;
    
    SettingsHandler *extraction = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsExtraction", nil)];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Voices.UniquePhrasesOnly", nil) key:@"Voices.UniquePhrasesOnly" type:N_TYPE_OF(NBoolean)];
    [extraction.settingHandlers addObject:handler];
    textDependent = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Voices.ExtractTextDependentFeatures", nil) key:@"Voices.ExtractTextDependentFeatures" type:N_TYPE_OF(NBoolean)];
    [extraction.settingHandlers addObject:textDependent];
    textIndependent = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Voices.ExtractTextIndependentFeatures", nil) key:@"Voices.ExtractTextIndependentFeatures" type:N_TYPE_OF(NBoolean)];
    [extraction.settingHandlers addObject:textIndependent];
    textDependent.boolANDRelation = textIndependent;
    textIndependent.boolANDRelation = textDependent;
    
    SettingsHandler *enrollment = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsEnrollment", nil)];
    handler = [self getCheckForDublicatesHandler];
    [enrollment.settingHandlers addObject:handler];
    
    return [NSArray arrayWithObjects:extraction, enrollment, nil];
}
+(NSArray*)makeFingerSettingsHandlers{
    SettingHandler *handler;
    
    SettingsHandler *extraction = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsExtraction", nil)];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Fingers.FastExtraction",nil) key:@"Fingers.FastExtraction" type:N_TYPE_OF(NBoolean)];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Fingers.TemplateSize",nil) key:@"Fingers.TemplateSize" type:N_TYPE_OF(NTemplateSize)];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Fingers.QualityThreshold",nil) key:@"Fingers.QualityThreshold" type:N_TYPE_OF(NUInt32)];
    [handler setMinimum:0 andMaximum:100];
    [extraction.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Fingers.ReturnProcessedImage",nil) key:@"Fingers.ReturnProcessedImage" type:N_TYPE_OF(NBoolean)];
    [extraction.settingHandlers addObject:handler];
    handler = [self getDefaultDPIHandler];
    [extraction.settingHandlers addObject:handler];
    
    SettingsHandler *matching = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsMatching", nil)];
    
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Fingers.MatchingSpeed",nil) key:@"Fingers.MatchingSpeed" type:N_TYPE_OF(NMatchingSpeed)];
    [matching.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Fingers.MaximalRotation",nil) key:@"Fingers.MaximalRotation" type:N_TYPE_OF(NSingle)];
    [handler setMinimum:0 andMaximum:180];
    [matching.settingHandlers addObject:handler];
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Matching.Threshold",nil) key:@"Matching.Threshold" type:N_TYPE_OF(NInt32)];
    [handler setMinimum:12 andMaximum:72];
    [matching.settingHandlers addObject:handler];
    
    SettingsHandler *enrollment = [[SettingsHandler alloc] initWithTitle:NSLocalizedString(@"settingsEnrollment", nil)];
    handler = [self getCheckForDublicatesHandler];
    [enrollment.settingHandlers addObject:handler];
    return [NSArray arrayWithObjects:extraction, matching, enrollment, nil];
}
+(SettingHandler*)getCheckForDublicatesHandler{
    return [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Enrollment.CheckForDublicates",nil) key:@"Enrollment.CheckForDublicates" type:N_TYPE_OF(NBoolean)];
}
+(SettingHandler*)getDefaultDPIHandler{
    SettingHandler *handler;
    handler = [[SettingHandler alloc] initWithTitle:NSLocalizedString(@"Fingers.DefaultDPI",nil) key:@"Fingers.DefaultDPI" type:N_TYPE_OF(NSingle)];
    [handler setMinimum:250 andMaximum:750];
    return handler;
}

@end
