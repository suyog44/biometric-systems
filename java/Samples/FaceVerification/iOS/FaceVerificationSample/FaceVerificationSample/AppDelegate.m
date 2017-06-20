#import "AppDelegate.h"
#import "Utils.h"

@interface AppDelegate ()

@end

@implementation AppDelegate

- (void)initFaceVerification {
    NSLog(@"Initializing");

    { // init
        NSString *appSupportDir = [Utils getAppSupportDir];
        NSString *dbFilename = @"database.db";
        NSString *dbPath = [appSupportDir stringByAppendingPathComponent:dbFilename];

        // removes database on every init
        //[Utils removeFileAtPath:dbPath];

        const NChar * szDb = [dbPath UTF8String];
        NChar * szPassword = N_T("password");

        NRESULT_CHECK(NFaceVerificationInitialize(szDb, szPassword));
    }

    { // list cameras and set second if avail
        HNString *arhNames;
        NInt nameCount;
        NRESULT_CHECK(NFaceVerificationGetAvailableCameraNamesN(&arhNames, &nameCount));
        for (NInt i=0; i<nameCount; i++) {
            NSLog(@"Camera %d: %@", i, [Utils nsStringFromHNString:arhNames[i]]);
        }
        if (nameCount > 1) {
            NRESULT_CHECK(NFaceVerificationSetCameraN(arhNames[1])); // using second as it usually is front cam
        }
        NStringFreeArray(arhNames, nameCount);
    }
}

- (void)uninitFaceVerification {
    NSLog(@"Uninitializing");

    NRESULT_CHECK(NFaceVerificationCancel());
    NRESULT_CHECK(NFaceVerificationUninitialize());
}

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    // Override point for customization after application launch.

    [self initFaceVerification];

    return YES;
}

- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.

    [self uninitFaceVerification];
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.

    [self initFaceVerification];
}

- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

@end
