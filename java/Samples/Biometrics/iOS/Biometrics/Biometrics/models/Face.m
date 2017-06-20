#import "Face.h"

@implementation Face
@synthesize frame, gender, eyesColor, hairColor, expression;

-(void)retrieveDataFromSubject{
    [super retrieveDataFromSubject];
    HNLAttributes attributes = NULL;
    HNFace face = NULL;
    NRect rect;
    NInt faceCount = 0;
    
    @try {
        NRESULT_CHECK(NSubjectGetFaceCount(self.subject, &faceCount));
        if (faceCount <= 0)
            return;
        
        NRESULT_CHECK(NSubjectGetFace(self.subject, 0, &face));
        NRESULT_CHECK(NFaceGetObject(face, 0, &attributes));
        
        NRESULT_CHECK(NLAttributesGetGender(attributes, &gender));
        NRESULT_CHECK(NLAttributesGetExpression(attributes, &expression));
        NRESULT_CHECK(NLAttributesGetBoundingRect(attributes, &rect));
        
        frame = CGRectMake(rect.X, rect.Y, rect.Width, rect.Height);
    }
    @catch (NSException *exception) {
    }
    @finally {
        NObjectSet(NULL, &attributes);
        NObjectSet(NULL, &face);
    }
}

-(NSString *)getDescription{
    NSMutableString *description = [NSMutableString stringWithFormat:@""];
    NSString *genderStr = @"";
    NSString *expressionStr = @"";
    switch (gender) {
        case ngFemale:
            genderStr = NSLocalizedString(@"ngFemale", nil);
            break;
        case ngMale:
            genderStr = NSLocalizedString(@"ngMale", nil);
            break;
            
        default:
            break;
    }
    
    switch (expression) {
        case nleNeutral:
            expressionStr = NSLocalizedString(@"nleNeutral", nil);
            break;
        case nleEyesAway:
            expressionStr = NSLocalizedString(@"nleEyesAway", nil);
            break;
        case nleFrowning:
            expressionStr = NSLocalizedString(@"nleFrowning", nil);
            break;
        case nleRaisedBrows:
            expressionStr = NSLocalizedString(@"nleRaisedBrows", nil);
            break;
        case nleSmile:
            expressionStr = NSLocalizedString(@"nleSmile", nil);
            break;
        case nleSmileOpenedJaw:
            expressionStr = NSLocalizedString(@"nleSmileOpenedJaw", nil);
            break;
        case nleSquinting:
            expressionStr = NSLocalizedString(@"nleSquinting", nil);
            break;
            
        default:
            break;
    }
    if (genderStr.length > 0){
        [description appendFormat:@"%@ %@\n", NSLocalizedString(@"faceGender", nil), genderStr];
    }
    if (expressionStr.length > 0){
        [description appendFormat:@"%@ %@", NSLocalizedString(@"faceExpression", nil), expressionStr];
    }
    return description;
}

@end
