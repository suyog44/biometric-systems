#import "DBCategoryCell.h"

@implementation DBCategoryCell
@synthesize title, image, topLine, middleLine, bottomLine, waveform, deleteButton;

- (void)awakeFromNib{
    [super awakeFromNib];
    title.textColor = [UIColor getSettingsDarkTextColor];
    title.font = [UIFont appMediumFontOfSize:14];
    topLine.backgroundColor = middleLine.backgroundColor = bottomLine.backgroundColor = [UIColor getDBLineColor];
    waveform.getWavesColor = [UIColor getAppBlueColor];
    waveform.hidden = image.hidden = true;
    deleteButton.hidden = true;
}
-(void)prepareForReuse{
    [super prepareForReuse];
    image.image = nil;
    image.hidden = true;
    waveform.hidden = true;
    title.text = @"";
    deleteButton.hidden = true;
    [deleteButton removeTarget:nil action:nil forControlEvents:UIControlEventAllEvents];
//    [self didDeselect];
}
- (void)setHighlighted:(BOOL)highlighted{
    [super setHighlighted:highlighted];
    if (highlighted){
        self.backgroundColor = [UIColor getDBLineColor];
    } else {
        self.backgroundColor = [UIColor whiteColor];
    }
}

+(float)heightForCell{
    return 44;
}
-(void)setupCellWithTitle:(NSString *)titleString andType:(UICollectionViewCellType)type{
    title.text = titleString;
    switch (type) {
        case UICollectionViewCellTypeFirst:
        {
            topLine.hidden = false;
            bottomLine.hidden = true;
            middleLine.hidden = false;
        }
            break;
        case UICollectionViewCellTypeFirstAndLast:
        {
            topLine.hidden = false;
            bottomLine.hidden = false;
            middleLine.hidden = true;
        }
            break;
        case UICollectionViewCellTypeLast:
        {
            topLine.hidden = true;
            bottomLine.hidden = false;
            middleLine.hidden = true;
        }
            break;
        case UICollectionViewCellTypeMiddle:
        {
            topLine.hidden = true;
            bottomLine.hidden = true;
            middleLine.hidden = false;
        }
            break;
            
        default:
            break;
    }

}
-(void)setupCellWithTitle:(NSString *)titleString andAudioPath:(NSString*)audioPath andType:(UICollectionViewCellType)type canDelete:(BOOL)canDelete{
    deleteButton.hidden = !canDelete;
    waveform.hidden = false;
    waveform.audioURL = [[NSURL alloc] initFileURLWithPath:audioPath];
    [self setupCellWithTitle:titleString andType:type];
}
-(void)setupCellWithTitle:(NSString*)titleString andImage:(UIImage*)img andType:(UICollectionViewCellType)type canDelete:(BOOL)canDelete{
    deleteButton.hidden = !canDelete;
    image.hidden = false;
    image.image = img;
    [self setupCellWithTitle:titleString andType:type];
}
@end
