#import <UIKit/UIKit.h>
#import "EWFView.h"

@interface DBCategoryCell : UICollectionViewCell

@property (nonatomic, retain) IBOutlet UILabel *title;
@property (nonatomic, retain) IBOutlet UIView *topLine;
@property (nonatomic, retain) IBOutlet UIView *bottomLine;
@property (nonatomic, retain) IBOutlet UIView *middleLine;
@property (nonatomic, retain) IBOutlet UIImageView *image;
@property (nonatomic, retain) IBOutlet EWFView *waveform;
@property (nonatomic, retain) IBOutlet UIButton *deleteButton;

+(float)heightForCell;
-(void)setupCellWithTitle:(NSString*)titleString andImage:(UIImage*)img andType:(UICollectionViewCellType)type canDelete:(BOOL)canDelete;
-(void)setupCellWithTitle:(NSString *)titleString andAudioPath:(NSString*)audioPath andType:(UICollectionViewCellType)type  canDelete:(BOOL)canDelete;

@end
