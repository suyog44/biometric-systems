#import "AppDelegate.h"
#import "SettingsHandler.h"

@implementation AppDelegate
@synthesize navigationController, window;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    [[UIApplication sharedApplication] setStatusBarStyle:UIStatusBarStyleLightContent];
    navigationController = [[NavigationController alloc] initWithRootViewController:[[MenuController alloc] initWithNibName:@"MenuController" bundle:nil]];
    window = [[UIWindow alloc] initWithFrame:[[UIScreen mainScreen] bounds]];
    window.rootViewController = navigationController;
    [window makeKeyAndVisible];
    
    if (![[BiometricsLayer sharedInstance] activateBiometrics]) return NO;
    
    SettingHandler *dublicatesHandler = [SettingsHandler getCheckForDublicatesHandler];
    if (![dublicatesHandler isPropertySet]) [dublicatesHandler setBoolProperty:true];
    
    [[BiometricsLayer sharedInstance] setupSettings];
    [self saveInitialImagesToLibrary];
    // Override point for customization after application launch.
    return YES;
}

-(void)saveInitialImagesToLibrary{
    
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    if (![[defaults objectForKey:INITIAL_GALLERY_KEY] boolValue]){
         ALAssetsLibrary *lib = [[ALAssetsLibrary alloc] init];
        
        NSString * resourcePath = [[NSBundle mainBundle] resourcePath];
        NSError * error = nil;
        NSArray * directoryContents = [[NSFileManager defaultManager] contentsOfDirectoryAtPath:resourcePath error:&error];
        if (error == nil){
            for (NSString *name in directoryContents){
                if ([name hasPrefix:INITIAL_IMAGE_PREFIX]){
                    NSLog(@"%@", name);
                    UIImage *img = [UIImage imageNamed:name];
                    if (img != nil){
                        [lib writeImageToSavedPhotosAlbum:img.CGImage metadata:nil completionBlock:^(NSURL *assetURL, NSError *error) {
                            if (error == nil){
                                NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
                                [defaults setObject:[NSNumber numberWithBool:true] forKey:INITIAL_GALLERY_KEY];
                                [defaults synchronize];
                            }
                        }];
                    }
                }
            }
        }
    }
}
							
- (void)applicationWillResignActive:(UIApplication *)application
{
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application
{
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    NCoreOnExitEx(NFalse);
}

@end
