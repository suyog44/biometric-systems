#import "SettingsHandler.h"

@implementation BiometricsLayer {
    HNDeviceManager hDeviceManager;
}

@synthesize delegate, lastOperationSubject;

static BiometricsLayer *sharedInstance;

+(BiometricsLayer *)sharedInstance{
    if (sharedInstance == nil){
        sharedInstance = [[self alloc] init];
    }
    return  sharedInstance;
}
- (void)dealloc{
    NObjectSet(NULL, &biometricClient);
    NObjectSet(NULL, &hDeviceManager);
    NObjectSet(NULL, &lastOperationSubject);
}
-(id)init{
    if (self = [super init]){
        HNSQLiteBiometricConnection hSQLiteBiometricConnection = NULL;
        // create biometric client
        @try {
            NRESULT_CHECK(NBiometricClientCreate(&biometricClient));
            // set SQLite database
            NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
            NSString *documentsPath = [paths objectAtIndex:0]; //Get the docs directory
            NRESULT_CHECK(NBiometricClientSetDatabaseConnectionToSQLite(biometricClient, [[documentsPath stringByAppendingPathComponent:@"database.sql"] cStringUsingEncoding:NSUTF8StringEncoding], &hSQLiteBiometricConnection));
            NRESULT_CHECK(NBiometricEngineInitialize(biometricClient));
            // init device manager
            hDeviceManager = NULL;
            NRESULT_CHECK(NDeviceManagerCreateEx(&hDeviceManager));
            NRESULT_CHECK(NDeviceManagerSetDeviceTypes(hDeviceManager, ndtFScanner));
            NRESULT_CHECK(NDeviceManagerInitialize(hDeviceManager));
        }
        @catch (NSException *exception) {
        }
        @finally {
            
        }
    }
    return self;
}
-(void)setupSettings{
    NSMutableArray *settings = [NSMutableArray array];
    [settings addObjectsFromArray:[SettingsHandler makeFaceSettingsHandlers]];
    [settings addObjectsFromArray:[SettingsHandler makeEyeSettingsHandlers]];
    [settings addObjectsFromArray:[SettingsHandler makeFingerSettingsHandlers]];
    [settings addObjectsFromArray:[SettingsHandler makeVoiceSettingsHandlers]];
    for (SettingsHandler *settingsHandler in settings){
        for (SettingHandler *settingHandler in settingsHandler.settingHandlers){
            if ([settingHandler isPropertySet]){
                if (settingHandler.type == N_TYPE_OF(NBoolean)){
                    [settingHandler setBoolProperty:[settingHandler getBoolProperty]];
                } else if (settingHandler.type == N_TYPE_OF(NSingle)){
                    [settingHandler setFloatProperty:[settingHandler getFloatProperty]];
                } else {
                    [settingHandler setIntProperty:[settingHandler getIntProperty]];
                }
            }
        }
    }
}
- (HNBiometricClient)getBiometricClient{
    return biometricClient;
}
-(BOOL)activateBiometrics{
    NResult result = N_OK;
    const NChar serverAddress[] = N_T("/local"); //The IP address of PC where Neurotechnology licensing service is running on. In your case, the IP address of your local machine.
    const NChar serverPort[] = N_T("5000"); //Licensing server port. Must be 5000
    
    const NChar * components [] = {
        N_T("Biometrics.FaceExtraction"),
        N_T("Biometrics.FaceMatching"),
        N_T("Biometrics.FaceSegmentsDetection"),
        N_T("Biometrics.IrisExtraction"),
        N_T("Biometrics.IrisMatching"),
        N_T("Biometrics.VoiceExtraction"),
        N_T("Biometrics.VoiceMatching"),
        N_T("Biometrics.FingerExtraction"),
        N_T("Biometrics.FingerMatching"),
        N_T("Devices.FingerScanners")
    };
    //Full list of components is provided in Neurotec_Biometric_5_0_SDK_Trial\Documentation\Neurotechnology Biometric SDK.pdf document (Section 2.6.5.1 Licensed API functionality)s
    for (int i = 0; i < sizeof(components)/sizeof(components[0]); i++)
    {
        NBool available = NFalse;
        result = NLicenseObtainComponents(serverAddress, serverPort, components[i], &available);
        if (NFailed(result))
        {
            NSLog(@"NLicenseObtainComponents() failed (result = %d)!", result);
            return false;
        }
        
        if (available) NSLog(@"Licenses for %s obtained", components[i]);
        else NSLog(@"Licenses for %s not available", components[i]);
    }
    
    return true;
}

-(BOOL)fillSubjectWithAttributes:(HNSubject)subject{
    NBiometricStatus status;
    @try {
        NRESULT_CHECK(NBiometricEngineGet(biometricClient, subject, &status));
        return true;
    }
    @catch (NSException *exception) {
        return false;
    }
    @finally {
    }
}
-(NSString*)getPrefixForSubject:(HNSubject)subject{
    NSString *fullId = [self getFullIdForSubject:subject];
    if ([fullId hasPrefix:PREFIX_FACE]){
        return PREFIX_FACE;
    }
    if ([fullId hasPrefix:PREFIX_EYE]){
        return PREFIX_EYE;
    }
    if ([fullId hasPrefix:PREFIX_FINGER]){
        return PREFIX_FINGER;
    }
    if ([fullId hasPrefix:PREFIX_VOICE]){
        return PREFIX_VOICE;
    }
    return @"";
}
-(NSString*)getNameForSubject:(HNSubject)subject{
    NSString *fullId = [self getFullIdForSubject:subject];
    if ([fullId hasPrefix:PREFIX_FACE]){
        return [fullId substringFromIndex:PREFIX_FACE.length];
    }
    if ([fullId hasPrefix:PREFIX_EYE]){
        return [fullId substringFromIndex:PREFIX_EYE.length];
    }
    if ([fullId hasPrefix:PREFIX_FINGER]){
        return [fullId substringFromIndex:PREFIX_FINGER.length];
    }
    if ([fullId hasPrefix:PREFIX_VOICE]){
        return [fullId substringFromIndex:PREFIX_VOICE.length];
    }
    return @"";
}
+(NSString*)stringFromHNString:(HNString)string{
    const char *stringChar;
    NInt stringLength;
    @try {
        NRESULT_CHECK(NStringGetLengthA(string, &stringLength));
        NRESULT_CHECK(NStringGetBufferA(string, &stringLength, &stringChar));
        return [NSString stringWithCString:stringChar encoding:NSUTF8StringEncoding];
    }
    @catch (NSException *exception) {
        return nil;
    }
    @finally {
    }
}
+(UIImage*)uiImageFromHNImage:(HNImage)hImage{
    HNBuffer hBuffer = NULL;
    UIImage *image = nil;
    @try {
        void *bufPtr;
        NSizeType bufSize;
        NRESULT_CHECK(NImageSaveToMemoryN(hImage, NULL, NULL, 0, &hBuffer));
        NRESULT_CHECK(NBufferGetPtr(hBuffer, &bufPtr));
        NRESULT_CHECK(NBufferGetSize(hBuffer, &bufSize));
        NSData *data = [NSData dataWithBytes:bufPtr length:bufSize];
        image = [UIImage imageWithData:data];
    }
    @catch (NSException *exception) {
        NSLog(@"Exception: %@", exception);
    }
    @finally {
        NRESULT_CHECK(NObjectSet(NULL, &hBuffer));
        return image;
    }
}

+(HNBuffer)nbufferFromNSData:(NSData *)data{
    HNBuffer hData = NULL;
    void * dataPtr = NULL;
    @try {
        NRESULT_CHECK(NBufferCreate([data length], &hData));
        NRESULT_CHECK(NBufferGetPtr(hData, &dataPtr));
        [data getBytes:dataPtr length:[data length]];
    }
    @catch (NSException *exception) {
        NObjectSet(NULL, &hData);
        return NULL;
    }
    return hData;
}

-(NSString*)getFullIdForSubject:(HNSubject)subject{
    NSString *fullId = @"";
    HNString subjectId = NULL;
    @try {
        NRESULT_CHECK(NSubjectGetId(subject, &subjectId));
        fullId = [BiometricsLayer stringFromHNString:subjectId];
    }
    @catch (NSException *exception) {
    }
    @finally {
        NStringSet(NULL, &subjectId);
        
    }
    return fullId;
}

-(NSArray*)getAllSubjectsWithPrefix:(NSString*)prefix{
    NSMutableArray *array = [NSMutableArray array];
    NInt count = 0;
    HNSubject *subjects = NULL;
    
    @try {
        NRESULT_CHECK(NBiometricEngineList(biometricClient, &subjects, &count));
    }
    @catch (NSException *exception) {
        return array;
    }
    @finally {
    }
    
    for (int i = 0; i < count; i++){
        HNSubject subject = subjects[i];
        NSLog(@"subjectId:%@", [self getFullIdForSubject:subject]);
        if ([[self getPrefixForSubject:subject] isEqualToString:prefix]){
            if ([prefix isEqualToString:PREFIX_FACE]){
                [array addObject:[[Face alloc] initWithSubject:subject]];
            }
            if ([prefix isEqualToString:PREFIX_VOICE]){
                [array addObject:[[Voice alloc] initWithSubject:subject]];
            }
            if ([prefix isEqualToString:PREFIX_FINGER]){
                [array addObject:[[Finger alloc] initWithSubject:subject]];
            }
            if ([prefix isEqualToString:PREFIX_EYE]){
                [array addObject:[[Eye alloc] initWithSubject:subject]];
            }
        }
    }
    return array;
}

//Verification
-(void)verifySubject:(HNSubject)subject withSubject:(HNSubject)otherSubject{
    verifySubject(biometricClient, subject, otherSubject);
}
static NResult N_API verifySubjectCallback(HNObject hObject, void * pParam)
{
	HNValue hValue = NULL;
	NBiometricStatus status;
	HNAsyncOperation hOperation = (HNAsyncOperation)hObject;
    NResult result = N_OK;
    
    @try {
        NRESULT_CHECK(NAsyncOperationGetResult(hOperation, &hValue));
		NRESULT_CHECK(NValueToValueP(hValue, N_TYPE_OF(NBiometricStatus), naNone, NULL, &status, sizeof(status)));
    }
    @catch (NSException *exception) {
        result = -1;
    }
    @finally {
        NObjectSet(NULL, &hValue);
        [[BiometricsLayer sharedInstance] performSelectorOnMainThread:@selector(callDelegateForVerificationSuccess:) withObject:[NSNumber numberWithBool:(NSucceeded(result)&&status==nbsOk)] waitUntilDone:false];
        return result;
    }
}

bool verifySubject(HNBiometricClient hBiometricClient, HNSubject hSubject, HNSubject hOtherSubject)
{
	NResult result = N_OK;
	HNAsyncOperation hAsyncOperation = NULL;
	HNSyncEvent hSyncEvent = NULL;
    
    @try {
        NRESULT_CHECK(NSyncEventCreate(NFalse, NFalse, &hSyncEvent));
        NRESULT_CHECK(NBiometricEngineVerifyOfflineAsync(hBiometricClient, hSubject, hOtherSubject, &hAsyncOperation));
        NRESULT_CHECK(NAsyncOperationAddCompletedCallback(hAsyncOperation, &verifySubjectCallback, hSyncEvent));
    }
    @catch (NSException *exception) {
        result = -1;
    }
    @finally {
        NObjectSet(NULL, &hSyncEvent);
        NObjectSet(NULL, &hAsyncOperation);
        return NSucceeded(result);
    }
}
-(void)callDelegateForVerificationSuccess:(NSNumber*)success{
    if ([[BiometricsLayer sharedInstance].delegate respondsToSelector:@selector(biometricsLayer:didFinishVerifyingWithSuccess:)]){
        [[BiometricsLayer sharedInstance].delegate biometricsLayer:[BiometricsLayer sharedInstance] didFinishVerifyingWithSuccess:[success boolValue]];
    }
}
//Identification
-(void)identifySubject:(HNSubject)subject{
    NRESULT_CHECK(NObjectSet(subject, &lastOperationSubject));
    if (!identifySubject(biometricClient, subject))
        [self callDelegateForIdentificationSuccess:[NSNumber numberWithBool:false]];
}

static NResult N_API identifySubjectCallback(HNObject hObject, void * pParam)
{
	HNValue hValue = NULL;
	NBiometricStatus status;
	HNAsyncOperation hOperation = (HNAsyncOperation)hObject;
	NResult result = N_OK;
    
    @try {
        NRESULT_CHECK(NAsyncOperationGetResult(hOperation, &hValue));
		NRESULT_CHECK(NValueToValueP(hValue, N_TYPE_OF(NBiometricStatus), naNone, NULL, &status, sizeof(status)));
    }
    @catch (NSException *exception) {
        result = -1;
    }
    @finally {
        NObjectSet(NULL, &hValue);
        [[BiometricsLayer sharedInstance] performSelectorOnMainThread:@selector(callDelegateForIdentificationSuccess:) withObject:[NSNumber numberWithBool:(NSucceeded(result)&&status==nbsOk)] waitUntilDone:false];
        return result;
    }
}

bool identifySubject(HNBiometricClient hBiometricClient, HNSubject hSubject)
{
	NResult result = N_OK;
	HNAsyncOperation hAsyncOperation = NULL;
	HNSyncEvent hSyncEvent = NULL;
    
    @try {
        NRESULT_CHECK(NSyncEventCreate(NFalse, NFalse, &hSyncEvent));
        NRESULT_CHECK(NBiometricEngineIdentifyAsync(hBiometricClient, hSubject, &hAsyncOperation));
        NRESULT_CHECK(NAsyncOperationAddCompletedCallback(hAsyncOperation, &identifySubjectCallback, hSyncEvent));
    }
    @catch (NSException *exception) {
        result = -1;
    }
    @finally {
        NObjectSet(NULL, &hSyncEvent);
        NObjectSet(NULL, &hAsyncOperation);
        return NSucceeded(result);
    }

}
-(void)callDelegateForIdentificationSuccess:(NSNumber*)success{
    HNSubject matchingSubject = NULL;
    if ([success boolValue]){
        HNMatchingResult matchinResult = NULL;
        HNString subjectId = NULL;
        @try {
            
            NRESULT_CHECK(NSubjectGetMatchingResult(lastOperationSubject, 0, &matchinResult));
            NRESULT_CHECK(NSubjectCreate(&matchingSubject));
            NRESULT_CHECK(NMatchingResultGetId(matchinResult, &subjectId));
            NRESULT_CHECK(NSubjectSetIdN(matchingSubject, subjectId));
        }
        @catch (NSException *exception) {
        }
        @finally {
            NObjectSet(NULL, &matchinResult);
            NStringSet(NULL, &subjectId);
        }
    }
    if ([[BiometricsLayer sharedInstance].delegate respondsToSelector:@selector(biometricsLayer:didFinishIdentifyingSubject:withSuccess:)]){
        [[BiometricsLayer sharedInstance].delegate biometricsLayer:[BiometricsLayer sharedInstance] didFinishIdentifyingSubject:matchingSubject withSuccess:[success boolValue]];
    }
}
//Extraction
-(void)extractVoice:(NSString*)path{
    HNSubject hSubject = NULL;
    HNVoice hVoice = NULL;
    
    @try {
        NRESULT_CHECK(NSubjectCreate(&hSubject));
        NRESULT_CHECK(NVoiceCreate(&hVoice));
        NRESULT_CHECK(NBiometricSetFileName(hVoice, [path cStringUsingEncoding:NSUTF8StringEncoding]));
        NRESULT_CHECK(NSubjectAddVoice(hSubject, hVoice, NULL));
        NRESULT_CHECK(NObjectSet(hSubject, &lastOperationSubject));
        if (!extractSubject(biometricClient, hSubject))
            [self callDelegateForExtractionSuccess:[NSNumber numberWithBool:false]];
    }
    @catch (NSException *exception) {
        [self callDelegateForExtractionSuccess:false];
    }
    @finally {
        NObjectSet(NULL, &hVoice);
        NObjectSet(NULL, &hSubject);
    }
}

-(void)extractEye:(UIImage*)image{
    NSData *pngData = UIImagePNGRepresentation(image);
    HNSubject hSubject = NULL;
    HNIris hIris = NULL;
    HNBuffer hImageData = NULL;
    @try {
        hImageData = [BiometricsLayer nbufferFromNSData:pngData];
        NRESULT_CHECK(NSubjectCreate(&hSubject));
        NRESULT_CHECK(NIrisCreate(&hIris));
        NRESULT_CHECK(NBiometricSetSampleBuffer(hIris, hImageData));
        NRESULT_CHECK(NSubjectAddIris(hSubject, hIris, NULL));
        NRESULT_CHECK(NObjectSet(hSubject, &lastOperationSubject));
        if (!extractSubject(biometricClient, hSubject)) [self callDelegateForExtractionSuccess:[NSNumber numberWithBool:false]];
    }
    @catch (NSException *exception) {

    }
    @finally {
        NObjectSet(NULL, &hImageData);
        NObjectSet(NULL, &hIris);
        NObjectSet(NULL, &hSubject);
    }
}
-(void)extractFinger:(HNImage)hImage{
    HNSubject hSubject = NULL;
    HNFinger hFinger = NULL;
    NFloat horzRes, vertRes;
    @try {
        NRESULT_CHECK(NImageGetHorzResolution(hImage, &horzRes));
        NRESULT_CHECK(NImageGetVertResolution(hImage, &vertRes));
        if (!(int)horzRes || !(int)vertRes) {
            SettingHandler *settingHandler = [SettingsHandler getDefaultDPIHandler];
            NFloat defaultRes = [settingHandler getFloatProperty];
            NRESULT_CHECK(NImageSetHorzResolution(hImage, defaultRes));
            NRESULT_CHECK(NImageSetVertResolution(hImage, defaultRes));
        }
        NRESULT_CHECK(NSubjectCreate(&hSubject));
        NRESULT_CHECK(NFingerCreate(&hFinger));
        NRESULT_CHECK(NFrictionRidgeSetImage(hFinger, hImage));
        NRESULT_CHECK(NSubjectAddFinger(hSubject, hFinger, NULL));
        NRESULT_CHECK(NObjectSet(hSubject, &lastOperationSubject));
        if (!extractSubject(biometricClient, hSubject))
            [self callDelegateForExtractionSuccess:[NSNumber numberWithBool:false]];
    }
    @catch (NSException *exception) {
        [self callDelegateForExtractionSuccess:false];
    }
    @finally {
        NObjectSet(NULL, &hFinger);
        NObjectSet(NULL, &hSubject);
    }
}
-(void)extractFaces:(UIImage*)image{
    NSData *pngData = UIImagePNGRepresentation(image);
    HNSubject hSubject = NULL;
    HNFace hFace = NULL;
    HNBuffer hImageData = NULL;
    @try {
        hImageData = [BiometricsLayer nbufferFromNSData:pngData];
        NRESULT_CHECK(NSubjectCreate(&hSubject));
        NRESULT_CHECK(NFaceCreate(&hFace));
        NRESULT_CHECK(NBiometricSetSampleBuffer(hFace, hImageData));
        NRESULT_CHECK(NSubjectAddFace(hSubject, hFace, NULL));
        NRESULT_CHECK(NSubjectSetMultipleSubjects(hSubject, true));
        NRESULT_CHECK(NObjectSet(hSubject, &lastOperationSubject));
        if (!extractSubject(biometricClient, hSubject)) [self callDelegateForExtractionSuccess:[NSNumber numberWithBool:false]];
    }
    @catch (NSException *exception) {
        [self callDelegateForExtractionSuccess:false];
    }
    @finally {
        NObjectSet(NULL, &hImageData);
        NObjectSet(NULL, &hFace);
        NObjectSet(NULL, &hSubject);
    }
}
static NResult N_API extractSubjectCallback(HNObject hObject, void * pParam)
{
    NResult result = N_OK;
    HNValue hValue = NULL;
    NBiometricStatus status = nbsNone;
    HNAsyncOperation hOperation = (HNAsyncOperation)hObject;
    @try {
        NRESULT_CHECK(NAsyncOperationGetResult(hOperation, &hValue));
        NRESULT_CHECK(NValueToValueP(hValue, N_TYPE_OF(NBiometricStatus), naNone, NULL, &status, sizeof(status)));
    }
    @catch (NSException *exception) {
        result = -1;
        dispatch_async(dispatch_get_main_queue(), ^{
            NSString *button = NSLocalizedString(@"enrollErrorAlertButton", nil);
            [[[UIAlertView alloc] initWithTitle:[exception name] message:[exception reason] delegate:nil cancelButtonTitle:button otherButtonTitles: nil] show];
        });
    }
    @finally {
        NObjectSet(NULL, &hValue);
        [[BiometricsLayer sharedInstance] performSelectorOnMainThread:@selector(callDelegateForExtractionSuccess:) withObject:[NSNumber numberWithBool:(NSucceeded(result)&&status==nbsOk)] waitUntilDone:false];
        return result;
    }
}

bool extractSubject(HNBiometricClient hBiometricClient, HNSubject hSubject)
{
    bool result;
    HNAsyncOperation hAsyncOperation = NULL;
    HNSyncEvent hSyncEvent = NULL;
    @try {
        NRESULT_CHECK(NSyncEventCreate(NFalse, NFalse, &hSyncEvent));
        NRESULT_CHECK(NBiometricEngineCreateTemplateAsync(hBiometricClient, hSubject, &hAsyncOperation));
        NRESULT_CHECK(NAsyncOperationAddCompletedCallback(hAsyncOperation, &extractSubjectCallback, hSyncEvent));
        result = true;
    }
    @catch (NSException *exception) {
        result = false;
    }
    @finally {
        NObjectSet(NULL, &hSyncEvent);
        NObjectSet(NULL, &hAsyncOperation);
        return result;
    }
}
-(void)callDelegateForExtractionSuccess:(NSNumber*)success{
    if ([[BiometricsLayer sharedInstance].delegate respondsToSelector:@selector(biometricsLayer:didFinishExtractingSubject:withSuccess:)]){
        [[BiometricsLayer sharedInstance].delegate biometricsLayer:[BiometricsLayer sharedInstance] didFinishExtractingSubject:[BiometricsLayer sharedInstance].lastOperationSubject withSuccess:[success boolValue]];
    }
}
//Enrollment
-(int)enrollSubject:(HNSubject)subject withId:(NSString*)subjectId andImage:(UIImage*)image withPrefix:(NSString*)prefix{
    NSString *subjectIdWithPrefix = [NSString stringWithFormat:@"%@%@", prefix, subjectId];
    NBiometricStatus status;
    SettingHandler *handler = [SettingsHandler getCheckForDublicatesHandler];
    int result = 0;
    
    NRESULT_CHECK(NSubjectSetId(subject, [subjectIdWithPrefix cStringUsingEncoding:NSUTF8StringEncoding]));
    NRESULT_CHECK(NBiometricEngineEnroll(biometricClient, subject, [handler getBoolProperty], &status));
    if (status == nbsOk){
        NSData *pngData = UIImagePNGRepresentation(image);
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsPath = [paths objectAtIndex:0]; //Get the docs directory
        NSString *filePath = [documentsPath stringByAppendingPathComponent:[NSString stringWithFormat:@"%@.png", subjectIdWithPrefix]]; //Add the file name
        [pngData writeToFile:filePath atomically:YES];
        result = 0;
    } else {
        result = status;
    }
    
    return result;
}
-(int)enrollSubject:(HNSubject)subject withId:(NSString*)subjectId andPath:(NSString*)path withPrefix:(NSString*)prefix{
    
    NSString *subjectIdWithPrefix = [NSString stringWithFormat:@"%@%@", prefix, subjectId];
    NBiometricStatus status;
    SettingHandler *handler = [SettingsHandler getCheckForDublicatesHandler];
    int result = 0;
    
    NRESULT_CHECK(NSubjectSetId(subject, [subjectIdWithPrefix cStringUsingEncoding:NSUTF8StringEncoding]));
    NRESULT_CHECK(NBiometricEngineEnroll(biometricClient, subject, [handler getBoolProperty], &status));
    if (status == nbsOk){
        NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
        NSString *documentsPath = [paths objectAtIndex:0]; //Get the docs directory
        NSString *filePath = [documentsPath stringByAppendingPathComponent:[NSString stringWithFormat:@"%@.%@", subjectIdWithPrefix, path.pathExtension]]; //Add the file name
        NSFileManager *fileManager = [NSFileManager defaultManager];
        if ([fileManager fileExistsAtPath:filePath] == YES) {
            [fileManager removeItemAtPath:filePath error:nil];
        }
        [fileManager copyItemAtPath:path toPath:filePath error:nil];
        result = 0;
    } else{
        result = status;
    }
    
    return result;
}
//RemoveSubject
-(void)removeSubject:(HNSubject)subject{
    @try {
        HNString subjectId;
        NBiometricStatus status;
        NRESULT_CHECK(NSubjectGetId(subject, &subjectId));
        NRESULT_CHECK(NBiometricEngineDelete(biometricClient, subjectId, &status));
    }
    @catch (NSException *exception) {
    }
    @finally {
    }
}

-(int)getConnectedScannerCount{
    @try {
        NInt scannerCount = 0;
        NRESULT_CHECK(NDeviceManagerGetDeviceCount(hDeviceManager, &scannerCount));
        return scannerCount;
    }
    @catch (NSException *exception) {
        return 0;
    }
}

-(void)scanImageFromDevice:(void (^)(HNImage hImage, NBiometricStatus status, NSString *errorMessage))callback{
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        HNDevice hDevice = NULL;
        HNFinger hFinger = NULL;
        HNError hError = NULL;
        HNString hString = NULL;
        HNImage hImage = NULL;
        NBiometricStatus status;
        
        @try {
            NRESULT_CHECK(NDeviceManagerGetDevice(hDeviceManager, 0, &hDevice));
            NRESULT_CHECK(NFingerCreate(&hFinger));
            NRESULT_CHECK(NFrictionRidgeSetPosition(hFinger, nfpUnknown));
            NRESULT_CHECK(NErrorCapture(NBiometricDeviceCapture(hDevice, hFinger, -1, &status), &hError));
            if (hError) {
                NRESULT_CHECK(NErrorGetMessageN(hError, &hString));
                dispatch_async(dispatch_get_main_queue(), ^{
                    callback(NULL, -1, [BiometricsLayer stringFromHNString:hString]);
                });
            }
            else if (status == nbsOk) {
                NRESULT_CHECK(NFrictionRidgeGetImage(hFinger, &hImage));
                dispatch_async(dispatch_get_main_queue(), ^{
                    callback(hImage, status, nil);
                });
            }
            else {
                dispatch_async(dispatch_get_main_queue(), ^{
                    callback(NULL, status, nil);
                });
            }
        }
        @finally {
            NStringSet(NULL, &hString);
            NObjectSet(NULL, &hFinger);
            NObjectSet(NULL, &hDevice);
        }
    });
}

-(void)cancelScan:(void (^)(void))callback{
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        HNDevice hDevice = NULL;
        @try {
            NRESULT_CHECK(NDeviceManagerGetDevice(hDeviceManager, 0, &hDevice));
            NRESULT_CHECK(NBiometricDeviceCancel(hDevice));
        }
        @finally {
            NObjectSet(NULL, &hDevice);
        }
        callback();
    });
}

@end
