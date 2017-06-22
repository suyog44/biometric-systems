#import <Foundation/Foundation.h>
#import "Face.h"

@interface FaceResult : NSObject

-(id)initWithImage:(UIImage *)imageArg andSubject:(HNSubject)subject;
@property (nonatomic, retain) UIImage *image;
@property (nonatomic, retain) NSMutableArray *faces;

@end
