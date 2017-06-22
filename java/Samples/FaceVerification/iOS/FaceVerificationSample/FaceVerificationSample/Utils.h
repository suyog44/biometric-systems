#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <NFaceVerification/NFaceVerification.h>
#import <NCore/Core/NEnum.h>

#define NRESULT_CHECK(func)\
    {\
        NResult result = (func);\
        if (NFailed(result))\
        {\
            HNError hError = NULL;\
            HNString hMessage = NULL;\
            HNString hStackTrace = NULL;\
            if (NSucceeded(NErrorGetLastEx(0, &hError))) {\
                NErrorGetMessageN(hError, &hMessage);\
                NErrorGetCallStackN(hError, &hStackTrace);\
            }\
            NSString *exceptionString = [NSString stringWithFormat:@"%i %@\n%@\n%@", result, [Utils nsStringFromHNString:hMessage], [[NSThread callStackSymbols] lastObject], [Utils nsStringFromHNString:hStackTrace]];\
            NObjectSet(NULL, &hError);\
            NStringSet(NULL, &hMessage);\
            NStringSet(NULL, &hStackTrace);\
            @throw [NSException exceptionWithName:@"NException" reason:exceptionString userInfo:nil];\
        }\
    };

@interface Utils : NSObject

+ (NSString* _Nonnull)nsStringFromHNString:(HNString _Nonnull)hString;
+ (UIImage* _Nonnull)uiImageFromHNImage:(HNImage _Nonnull)hImage;
+ (NSString* _Nonnull)getAppSupportDir;
+ (void)removeFileAtPath:( NSString* _Nonnull )path;
+ (UIAlertController* _Nonnull)createSimpleAlert:(NSString* _Nullable)title withMessage:(NSString* _Nonnull)message;
+ (NSString* _Nonnull)faceVerificationStatusToNSString:(NFaceVerificationStatus)status;

@end
