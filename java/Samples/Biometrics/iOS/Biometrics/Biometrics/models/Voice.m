#import "Voice.h"

@implementation Voice

-(NSString*)getAudioFilePath{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsPath = [paths objectAtIndex:0]; //Get the docs directory
    return [documentsPath stringByAppendingPathComponent:[NSString stringWithFormat:@"%@%@.wav", self.namePrefix, self.name]]; //Add the file name
}

@end
