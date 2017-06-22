#import "BiometricsResource.h"

@interface Eye: BiometricsResource{
    NSMutableArray *innerBoundaryPoints;
    NSMutableArray *outerBoundaryPoints;
}
-(UIImage*)getImageWithAttributesOnImage:(UIImage*)originalImage;
@end
