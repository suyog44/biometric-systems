#import "DatabaseController.h"
#import "DBCategoryCell.h"
#import "FacesCollectionViewDelegate.h"
#import "FingersCollectionViewDelegate.h"
#import "VoicesCollectionViewDelegate.h"
#import "EyesCollectionViewDelegate.h"

@interface DatabaseController ()

@end

@implementation DatabaseController
@synthesize collectionView;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil andDelegate:(CollectionDelegate*)collectionDelegateArg andTitle:(NSString*)titleArg
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
        collectionDelegate = collectionDelegateArg;
        self.title = titleArg;
    }
    return self;
}
- (void)viewWillAppear:(BOOL)animated{
    [super viewWillAppear:animated];
    if (self.navigationController.viewControllers.count == 1){
        self.navigationItem.rightBarButtonItem = [[UIBarButtonItem alloc] initWithImage:[UIImage imageNamed:@"close"] style:UIBarButtonItemStylePlain target:self action:@selector(closeClicked:)];
    } else {
        self.navigationItem.rightBarButtonItem = nil;
    }
}
-(IBAction)closeClicked:(id)sender{
    [self dismissViewControllerAnimated:true completion:^{
        
    }];
}
- (void)viewDidLoad
{
    [super viewDidLoad];
    self.view.backgroundColor = [UIColor getViewControllerBgColor];
    [collectionView registerNib:[UINib nibWithNibName:@"DBCategoryCell" bundle:nil] forCellWithReuseIdentifier:@"DBCategoryCell"];
    [collectionView registerNib:[UINib nibWithNibName:@"DBEmptyCell" bundle:nil] forCellWithReuseIdentifier:@"DBEmptyCell"];
    if (collectionDelegate == nil){
        collectionView.delegate = self;
        collectionView.dataSource = self;
    } else {
        [collectionDelegate setupDelegateWithViewController:self];
        collectionView.delegate = collectionDelegate;
        collectionView.dataSource = collectionDelegate;
    }
    // Do any additional setup after loading the view from its nib.
}

-(IBAction)deleteSubjectClicked:(UIButton*)arg{
    UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:NSLocalizedString(@"deleteSubjectAlertTitle", nil) message:NSLocalizedString(@"deleteSubjectAlertMessage", nil) delegate:self cancelButtonTitle:NSLocalizedString(@"deleteSubjectAlertCancel", nil) otherButtonTitles:NSLocalizedString(@"deleteSubjectAlertAccept", nil), nil];
    alertView.tag = arg.tag;
    [alertView show];
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

//CollectionView

- (NSInteger)numberOfSectionsInCollectionView:(UICollectionView *)collectionView{
    return 1;
}
- (NSInteger)collectionView:(UICollectionView *)collectionView numberOfItemsInSection:(NSInteger)section{
    return 4;
}
- (UIEdgeInsets)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout *)collectionViewLayout insetForSectionAtIndex:(NSInteger)section{
    return UIEdgeInsetsMake(30, 0, 0, 0);
}
- (CGFloat)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout *)collectionViewLayout minimumInteritemSpacingForSectionAtIndex:(NSInteger)section{
    return 0;
}
- (CGFloat)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout *)collectionViewLayout minimumLineSpacingForSectionAtIndex:(NSInteger)section{
    return 0;
}
- (CGSize)collectionView:(UICollectionView *)collectionViewArg layout:(UICollectionViewLayout *)collectionViewLayout sizeForItemAtIndexPath:(NSIndexPath *)indexPath{
    return CGSizeMake(collectionViewArg.frame.size.width, [DBCategoryCell heightForCell]);
}
-(UICollectionViewCell *)collectionView:(UICollectionView *)collectionViewArg cellForItemAtIndexPath:(NSIndexPath *)indexPath{
    DBCategoryCell *cell = [collectionViewArg dequeueReusableCellWithReuseIdentifier:@"DBCategoryCell" forIndexPath:indexPath];
    UICollectionViewCellType cellType = UICollectionViewCellTypeMiddle;
    int itemsCount = (int)[self collectionView:collectionViewArg numberOfItemsInSection:indexPath.section];
    if (itemsCount == 1) cellType = UICollectionViewCellTypeFirstAndLast;
    else if (indexPath.row == itemsCount-1) cellType = UICollectionViewCellTypeLast;
    else if (indexPath.row == 0) cellType = UICollectionViewCellTypeFirst;
    switch (indexPath.row) {
        case 0:
        {
            [cell setupCellWithTitle:NSLocalizedString(@"dbFinger", nil) andImage:[UIImage imageNamed:@"db_finger.png"] andType:cellType canDelete:false];
        }
            break;
        case 1:
        {
            [cell setupCellWithTitle:NSLocalizedString(@"dbFace", nil) andImage:[UIImage imageNamed:@"db_face.png"] andType:cellType canDelete:false];
        }
            break;
        case 2:
        {
            [cell setupCellWithTitle:NSLocalizedString(@"dbEye", nil) andImage:[UIImage imageNamed:@"db_eye.png"] andType:cellType canDelete:false];
        }
            break;
        case 3:
        {
            [cell setupCellWithTitle:NSLocalizedString(@"dbVoice", nil) andImage:[UIImage imageNamed:@"db_voice.png"] andType:cellType canDelete:false];
        }
            break;
            
        default:
            break;
    }
    return cell;
}
- (void)collectionView:(UICollectionView *)collectionView didSelectItemAtIndexPath:(NSIndexPath *)indexPath{
    switch (indexPath.row) {
        case 0:
        {
            UIViewController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[FingersCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbFinger", nil)];
            [self.navigationController pushViewController:viewController animated:true];
        }
            break;
        case 1:
        {
            UIViewController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[FacesCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbFace", nil)];
            [self.navigationController pushViewController:viewController animated:true];
        }
            break;
        case 2:
        {
            UIViewController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[EyesCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbEye", nil)];
            [self.navigationController pushViewController:viewController animated:true];
        }
            break;
        case 3:
        {
            UIViewController *viewController = [[DatabaseController alloc] initWithNibName:@"DatabaseController" bundle:nil andDelegate:[[VoicesCollectionViewDelegate alloc] init] andTitle:NSLocalizedString(@"dbVoice", nil)];
            [self.navigationController pushViewController:viewController animated:true];
        }
            break;
            
        default:
            break;
    }
}

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex{
    if (buttonIndex == 1){
        HNSubject subject = [collectionDelegate getSubjectAtIndex:(int)alertView.tag];
        [[BiometricsLayer sharedInstance] removeSubject:subject];
        [collectionDelegate reloadData];
    }
}

@end
