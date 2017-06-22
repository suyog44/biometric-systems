#import <Foundation/Foundation.h>
#ifdef N_LIB
#import <NBiometricClient.h>
#else
#import <NBiometricClient/NBiometricClient.h>
#endif
#import "BiometricsResource.h"

@interface Face : BiometricsResource

@property (nonatomic) CGRect frame;
@property (nonatomic) NGender gender;
@property (nonatomic) NLExpression expression;
@property (nonatomic, retain) NSString *eyesColor;
@property (nonatomic, retain) NSString *hairColor;

-(NSString *)getDescription;

@end
