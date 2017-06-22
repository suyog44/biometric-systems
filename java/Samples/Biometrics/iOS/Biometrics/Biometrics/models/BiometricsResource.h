#import <Foundation/Foundation.h>

@interface BiometricsResource : NSObject

-(id)initWithSubject:(HNSubject)subjectArg;
-(void)retrieveDataFromSubject;

@property (nonatomic) HNSubject subject;
@property (nonatomic, retain) NSString *name;
@property (nonatomic, retain) NSString *namePrefix;

-(NSString*)getImagePath;
-(UIImage*)getImage;

@end
