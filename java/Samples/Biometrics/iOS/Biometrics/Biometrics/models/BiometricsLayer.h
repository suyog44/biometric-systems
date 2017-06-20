#import <Foundation/Foundation.h>
#ifdef N_LIB
#import <NCore.h>
#import <NLicensing.h>
#import <NBiometrics.h>
#import <NBiometricClient.h>
#else
#import <NCore/NCore.h>
#import <NLicensing/NLicensing.h>
#import <NBiometrics/NBiometrics.h>
#import <NBiometricClient/NBiometricClient.h>
#endif
#import "FaceResult.h"
#import "BiometricsLayer.h"
#import "Finger.h"
#import "Voice.h"
#import "Eye.h"

#define NRESULT_CHECK_ANALYTICS

#define NRESULT_CHECK(func)\
    {\
        NResult result = (func);\
        if (N_UNLIKELY(NFailed(result)))\
        {\
            HNError hError = NULL;\
            HNString hMessage = NULL;\
            HNString hCallStack = NULL;\
            NSString *exceptionString = nil;\
            NSString *callStackString = nil;\
            if (NSucceeded(NErrorGetLastEx(0, &hError))) {\
                NObjectToStringN(hError, NULL, &hMessage);\
                NErrorGetCallStackN(hError, &hCallStack);\
                exceptionString = [BiometricsLayer stringFromHNString:hMessage];\
            } else {\
                exceptionString = @"Unknown error";\
            }\
            if (NStringIsEmpty(hCallStack)) {\
                callStackString = [[NSThread callStackSymbols] componentsJoinedByString:@"\n"];\
            } else {\
                callStackString = [BiometricsLayer stringFromHNString:hCallStack];\
            }\
            NSLog(@"Error detected:\n%@Call stack:\n%@", exceptionString, callStackString);\
            NObjectSet(NULL, &hError);\
            NStringSet(NULL, &hMessage);\
            NStringSet(NULL, &hCallStack);\
            @throw [NSException exceptionWithName:@"NException" reason:exceptionString userInfo:nil];\
        }\
    };

@class BiometricsLayer;

@protocol BiometricsLayerDelegate <NSObject>

-(void)biometricsLayer:(BiometricsLayer*)bl didFinishExtractingSubject:(HNSubject)subject withSuccess:(BOOL)success;
-(void)biometricsLayer:(BiometricsLayer*)bl didFinishIdentifyingSubject:(HNSubject)subject withSuccess:(BOOL)success;
-(void)biometricsLayer:(BiometricsLayer*)bl didFinishVerifyingWithSuccess:(BOOL)success;

@end

@interface BiometricsLayer : NSObject{
    HNBiometricClient biometricClient;
    
}

+ (BiometricsLayer *) sharedInstance;

-(BOOL)activateBiometrics;
-(void)setupSettings;
-(HNBiometricClient)getBiometricClient;
-(void)extractFaces:(UIImage*)image;
-(void)extractFinger:(HNImage)hImage;
-(void)extractVoice:(NSString*)path;
-(void)extractEye:(UIImage*)image;

-(int)getConnectedScannerCount;
-(void)scanImageFromDevice:(void (^)(HNImage hImage, NBiometricStatus status, NSString *errorMessage))callback;
-(void)cancelScan:(void (^)(void))callback;

-(void)identifySubject:(HNSubject)subject;

-(void)verifySubject:(HNSubject)subject withSubject:(HNSubject)otherSubject;

-(int)enrollSubject:(HNSubject)subject withId:(NSString*)subjectId andImage:(UIImage*)image withPrefix:(NSString*)prefix;
-(int)enrollSubject:(HNSubject)subject withId:(NSString*)subjectId andPath:(NSString*)path withPrefix:(NSString*)prefix;
-(void)removeSubject:(HNSubject)subject;

-(NSString*)getPrefixForSubject:(HNSubject)subject;
-(NSString*)getNameForSubject:(HNSubject)subject;
-(NSArray*)getAllSubjectsWithPrefix:(NSString*)prefix;

-(BOOL)fillSubjectWithAttributes:(HNSubject)subject;

+(NSString*)stringFromHNString:(HNString)string;
+(UIImage*)uiImageFromHNImage:(HNImage)hImage;
+(HNBuffer)nbufferFromNSData:(NSData *)data;

@property (nonatomic, assign) id<BiometricsLayerDelegate> delegate;
@property (nonatomic) HNSubject lastOperationSubject;

@end
