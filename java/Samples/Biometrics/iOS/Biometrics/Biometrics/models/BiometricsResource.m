#import "BiometricsResource.h"

@implementation BiometricsResource
@synthesize name, namePrefix, subject;

- (id)initWithSubject:(HNSubject)subjectArg{
    if (self = [super init]){
        NRESULT_CHECK(NObjectSet(subjectArg, &subject));
        
        [self retrieveDataFromSubject];
    }
    return self;
}

- (void)retrieveDataFromSubject{
    name = [[BiometricsLayer sharedInstance] getNameForSubject:subject];
    namePrefix = [[BiometricsLayer sharedInstance] getPrefixForSubject:subject];
}

-(NSString*)getImagePath{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsPath = [paths objectAtIndex:0]; //Get the docs directory
    return [documentsPath stringByAppendingPathComponent:[NSString stringWithFormat:@"%@%@.png", self.namePrefix, self.name]]; //Add the file name
}
- (UIImage *)getImage{
    return [UIImage imageWithContentsOfFile:[self getImagePath]];
}
- (void)dealloc{
    NObjectSet(NULL, &subject);
}

@end
