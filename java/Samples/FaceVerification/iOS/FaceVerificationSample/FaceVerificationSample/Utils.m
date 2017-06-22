#import "Utils.h"

@implementation Utils

+ (NSString* _Nonnull)nsStringFromHNString:(HNString _Nonnull)hString {
    const char *cString;
    NInt stringLength;
    NResult res = -1;
    // we do not NRESULT_CHECK here because NRESULT_CHECK uses nsStringFromHNString
    res = NStringGetLengthA(hString, &stringLength);
    if (NFailed(res)) {
        @throw [NSException exceptionWithName:@"NStringGetLengthAException" reason:nil userInfo:nil];
    }
    res = NStringGetBufferA(hString, &stringLength, &cString);
    if (NFailed(res)) {
        @throw [NSException exceptionWithName:@"NStringGetBufferAException" reason:nil userInfo:nil];
    }
    return [NSString stringWithCString:cString encoding:NSUTF8StringEncoding];
}

+ (UIImage* _Nonnull)uiImageFromHNImage:(HNImage _Nonnull)hImage {
    UIImage *uiImage = nil;
    CGColorSpaceRef cgColorSpace = NULL;
    CGContextRef cgContext = NULL;
    CGImageRef cgImage = NULL;
    HNImage hImageRGBA = NULL;
    @try {
        void * pPixels;
        NUInt width, height;

        NRESULT_CHECK(NImageCreateFromImageEx2(NPF_RGB_A_8U, 0, hImage, 0, &hImageRGBA));

        NRESULT_CHECK(NImageGetPixelsPtr(hImageRGBA, &pPixels));
        NRESULT_CHECK(NImageGetWidth(hImageRGBA, &width));
        NRESULT_CHECK(NImageGetHeight(hImageRGBA, &height));

        cgColorSpace = CGColorSpaceCreateDeviceRGB();
        cgContext = CGBitmapContextCreate(pPixels, width, height, 8, width*4, cgColorSpace, kCGImageAlphaNoneSkipLast);
        cgImage = CGBitmapContextCreateImage(cgContext);

        uiImage = [[UIImage alloc] initWithCGImage:cgImage];
    }
    @catch (NSException *exception) {
        NSLog(@"Exception: %@", exception);
    }
    @finally {
        NObjectSet(NULL, &hImageRGBA);
        CGColorSpaceRelease(cgColorSpace);
        CGContextRelease(cgContext);
        CGImageRelease(cgImage);
        return uiImage;
    }
}

+ (NSString* _Nonnull)getAppSupportDir {
    NSString *appSupportDir = [NSSearchPathForDirectoriesInDomains(NSApplicationSupportDirectory, NSUserDomainMask, YES) lastObject];
    NSFileManager *manager = [NSFileManager defaultManager];
    if(![manager fileExistsAtPath:appSupportDir]) {
        NSLog(@"App support dir does not exist, creating one: %@", appSupportDir);
        NSError *error;
        BOOL ret = [manager createDirectoryAtPath:appSupportDir withIntermediateDirectories:NO attributes:nil error:&error];
        if(!ret) {
            @throw [NSException exceptionWithName:@"AppSupportDirCreationException" reason:[NSString stringWithFormat:@"Error: %@", error] userInfo:nil];
        }
    }
    return appSupportDir;
}

+ (void)removeFileAtPath:( NSString* _Nonnull )path {
    NSFileManager *manager = [NSFileManager defaultManager];
    if([manager fileExistsAtPath:path]) {
        NSError *error;
        BOOL ret = [manager removeItemAtPath:path error:&error];
        if(!ret) {
            @throw [NSException exceptionWithName:@"RemoveFileException" reason:[NSString stringWithFormat:@"Failed to remove %@: %@", path, error] userInfo:nil];
        }
    } else {
        NSLog(@"Nothing to remove: %@", path);
    }
}

+ (UIAlertController* _Nonnull)createSimpleAlert:(NSString* _Nullable)title withMessage:(NSString* _Nonnull)message {
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:title message:message preferredStyle:UIAlertControllerStyleAlert];

    UIAlertAction *ok = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
        [alert dismissViewControllerAnimated:YES completion:nil];
    }];

    [alert addAction:ok];

    return alert;
}

+ (NSString* _Nonnull)faceVerificationStatusToNSString:(NFaceVerificationStatus)status {
    HNString hString = NULL;
    NSString *nsString = nil;
    @try {
        NRESULT_CHECK(NEnumToStringP(N_TYPE_OF(NFaceVerificationStatus), status, NULL, &hString));
        nsString = [self nsStringFromHNString:hString];
    } @catch (NSException *exception) {
        NSLog(@"Exception: %@", exception);
    } @finally {
        NRESULT_CHECK(NStringSet(NULL, &hString));
        return nsString;
    }
}

@end
