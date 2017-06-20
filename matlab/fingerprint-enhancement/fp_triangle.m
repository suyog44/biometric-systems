figure
path = 'images/DB/101_8.tif';
im = imread(path);
sklt = skelet(im);
[pCore, pDelta, bImage, bgmask] = core_detection(path);
pCore = pCore(1,:);
pCore

%Finding start index for triangle top by finding nearest non-zero pixel
%from core point
[D,IDX] = bwdist(sklt~=0);
ridge_idx = IDX(pCore(1),pCore(2));
[i, j] = ind2sub(size(sklt), ridge_idx);
[x, y] = ind2sub(size(sklt), IDX(pCore(2),pCore(1)));

imshow(sklt);

hold on;
plot(i,j,'r.','MarkerSize',20)

%Finding initial tracking direction
P1 = sum(sum(sklt(i-2:i-1,j+1:j+2)));
P2 = sum(sum(sklt(i-2:i-1,j-2:j-1)));
P3 = sum(sum(sklt(i+1:i+2,j-2:j-1)));
P4 = sum(sum(sklt(i+2:i+1,j+1:j+2)));
[v, Pk] = min([P1,P2,P3,P4]);

dist = 20;
contour1 = bwtraceboundary(sklt, [x, y], 'N', 8, dist);
contour2 = bwtraceboundary(sklt, [x, y], 'NE', 8, dist);
contour3 = bwtraceboundary(sklt, [x, y], 'E', 8, dist);
contour4 = bwtraceboundary(sklt, [x, y], 'SE', 8, dist);
contour5 = bwtraceboundary(sklt, [x, y], 'S', 8, dist);
contour6 = bwtraceboundary(sklt, [x, y], 'SW', 8, dist);
contour7 = bwtraceboundary(sklt, [x, y], 'W', 8, dist);
contour8 = bwtraceboundary(sklt, [x, y], 'NW', 8, dist);

contours = [contour1(end,:); contour2(end,:); contour3(end,:); contour4(end,:); contour5(end,:); contour6(end,:); contour7(end,:); contour8(end,:)];
unique_contours = unique(contours, 'rows');

y1 = unique_contours(1,1);
x1 = unique_contours(1,2);
y2 = unique_contours(2,1);
x2 = unique_contours(2,2);

mid_point = [(x1+x2)/2, (y1+y2)/2];

%hold on;
%plot(contour1(:,2), contour1(:,1), 'g', 'LineWidth', 1)
hold on;
plot(x1,y1,'g.','MarkerSize',20)
plot(x2,y2,'b.','MarkerSize',20)
plot(mid_point(1),mid_point(2),'y.','MarkerSize',20)
plot(contour2(:,2), contour2(:,1), 'b', 'LineWidth', 1)

