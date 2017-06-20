#import "SettingsController.h"

@interface SettingsController ()

@end

@implementation SettingsController
@synthesize collectionView;
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil andSettingsHandlers:(NSArray*)settingsHandlersArg andTitle:(NSString*)title;
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.title = title;
        settingsHandlers = settingsHandlersArg;
        // Custom initialization
    }
    return self;
}

- (void)viewDidLoad
{
    [super viewDidLoad];
    self.view.backgroundColor = [UIColor getViewControllerBgColor];
    collectionView.delegate = self;
    collectionView.dataSource = self;
    collectionView.alwaysBounceVertical = true;
    [collectionView registerNib:[UINib nibWithNibName:@"SettingsSectionHeader" bundle:nil] forSupplementaryViewOfKind:UICollectionElementKindSectionHeader withReuseIdentifier:@"SettingsSectionHeader"];
    [collectionView registerNib:[UINib nibWithNibName:@"SettingsCheckboxCell" bundle:nil] forCellWithReuseIdentifier:@"SettingsCheckboxCell"];
    [collectionView registerNib:[UINib nibWithNibName:@"SettingsSliderCell" bundle:nil] forCellWithReuseIdentifier:@"SettingsSliderCell"];
}
- (void)viewWillAppear:(BOOL)animated{
    [super viewWillAppear:animated];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender
{
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/
//UICollectionViewDelegate
- (NSInteger)numberOfSectionsInCollectionView:(UICollectionView *)collectionView{
    return settingsHandlers.count;
}
- (NSInteger)collectionView:(UICollectionView *)collectionView numberOfItemsInSection:(NSInteger)section{
    SettingsHandler *settingsHandler = [settingsHandlers objectAtIndex:section];
    return settingsHandler.settingHandlers.count;
}
- (UIEdgeInsets)collectionView:(UICollectionView *)collectionViewArg layout:(UICollectionViewLayout *)collectionViewLayout insetForSectionAtIndex:(NSInteger)section{
    if (section == [self numberOfSectionsInCollectionView:collectionViewArg]-1){
        return UIEdgeInsetsMake(0, 0, 40, 0);
    }
    return UIEdgeInsetsZero;
}
- (CGFloat)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout *)collectionViewLayout minimumInteritemSpacingForSectionAtIndex:(NSInteger)section{
    return 0;
}
- (CGFloat)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout *)collectionViewLayout minimumLineSpacingForSectionAtIndex:(NSInteger)section{
    return 0;
}
- (CGSize)collectionView:(UICollectionView *)collectionViewArg layout:(UICollectionViewLayout *)collectionViewLayout referenceSizeForHeaderInSection:(NSInteger)section{
    return CGSizeMake(collectionViewArg.frame.size.width, [SettingsSectionHeader heightForView]);
}
- (CGSize)collectionView:(UICollectionView *)collectionViewArg layout:(UICollectionViewLayout *)collectionViewLayout sizeForItemAtIndexPath:(NSIndexPath *)indexPath{
    SettingsHandler *settingsHandler = [settingsHandlers objectAtIndex:indexPath.section];
    SettingHandler *settingHandler = [settingsHandler.settingHandlers objectAtIndex:indexPath.row];
    if (settingHandler.type == N_TYPE_OF(NBoolean)){
        return CGSizeMake(collectionViewArg.frame.size.width, [SettingsCheckboxCell heightForCell]);
    } else {
        return CGSizeMake(collectionViewArg.frame.size.width, [SettingsSliderCell heightForCell]);
    }
}
- (UICollectionReusableView *)collectionView:(UICollectionView *)collectionViewArg viewForSupplementaryElementOfKind:(NSString *)kind atIndexPath:(NSIndexPath *)indexPath{
    SettingsHandler *settingsHandler = [settingsHandlers objectAtIndex:indexPath.section];
    SettingsSectionHeader *view = [collectionViewArg dequeueReusableSupplementaryViewOfKind:kind withReuseIdentifier:@"SettingsSectionHeader" forIndexPath:indexPath];
    [view setupViewWithLabel:settingsHandler.title];
    return view;
}
- (UICollectionViewCell *)collectionView:(UICollectionView *)collectionViewArg cellForItemAtIndexPath:(NSIndexPath *)indexPath{
    SettingsHandler *settingsHandler = [settingsHandlers objectAtIndex:indexPath.section];
    SettingHandler *settingHandler = [settingsHandler.settingHandlers objectAtIndex:indexPath.row];
    UICollectionViewCellType cellType = UICollectionViewCellTypeMiddle;
    int itemsCount = (int)[self collectionView:collectionViewArg numberOfItemsInSection:indexPath.section];
    if (itemsCount == 1) cellType = UICollectionViewCellTypeFirstAndLast;
    else if (indexPath.row == itemsCount-1) cellType = UICollectionViewCellTypeLast;
    else if (indexPath.row == 0) cellType = UICollectionViewCellTypeFirst;
    if (settingHandler.type == N_TYPE_OF(NBoolean)){
        SettingsCheckboxCell *cell = [collectionViewArg dequeueReusableCellWithReuseIdentifier:@"SettingsCheckboxCell" forIndexPath:indexPath];
        [cell setupWithSettingHandler:settingHandler andType:cellType];
        return cell;
    } else {
        SettingsSliderCell *cell = [collectionViewArg dequeueReusableCellWithReuseIdentifier:@"SettingsSliderCell" forIndexPath:indexPath];
        [cell setupWithSettingHandler:settingHandler andType:cellType];
        return cell;
    }
}

@end
