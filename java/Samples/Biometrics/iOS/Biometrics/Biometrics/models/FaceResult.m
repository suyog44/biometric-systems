#import "FaceResult.h"

@implementation FaceResult
@synthesize image, faces;
-(id)initWithImage:(UIImage *)imageArg andSubject:(HNSubject)subject{
    if (self = [super init]){
        image = imageArg;
        faces = [NSMutableArray array];
        
        NInt faceCount = 0;
        int num = 0;
        HNFace hFace = NULL;
        HNLAttributes hAttributes = NULL;
        HNSubject hNewSubject = NULL;
        
        @try {
            NRESULT_CHECK(NSubjectGetFaceCount(subject, &faceCount));
            if (faceCount > 0)
            {
                NRESULT_CHECK(NSubjectGetFace(subject, 0, &hFace));
                NRESULT_CHECK(NFaceGetObjectCount(hFace, &num));
            }
            for (int i = 0; i < num; i++){
                if (i == 0){
                    NRESULT_CHECK(NObjectSet(subject, &hNewSubject));
                } else {
                    NRESULT_CHECK(NFaceGetObject(hFace, i, &hAttributes));
                    NRESULT_CHECK(NBiometricAttributesGetChildSubject(hAttributes, &hNewSubject));
                }
                Face *face = [[Face alloc] initWithSubject:hNewSubject];
                [faces addObject:face];
            }
        }
        @catch (NSException *exception) {
        }
        @finally {
            NObjectSet(NULL, &hFace);
            NObjectSet(NULL, &hAttributes);
            NObjectSet(NULL, &hNewSubject);
        }
    }
    return self;
}

@end
