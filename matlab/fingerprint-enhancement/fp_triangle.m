figure
for f_idx=1:8
    path = strcat('data/DB1_B/104_',int2str(f_idx),'.tif');
    im = imread(path);
    sklt = skelet(im);
    [pCore, pDelta, bImage, bgmask] = core_detection(path);
    %imshow(bImage.*bgmask)

    pCore
    pCore = pCore(1,:);

    %%

    %Finding start index for triangle top by finding nearest non-zero pixel
    %from core point
    [D,IDX] = bwdist(sklt~=0);
    ridge_idx = IDX(pCore(1),pCore(2));
    [i, j] = ind2sub(size(sklt), ridge_idx);
    [x, y] = ind2sub(size(sklt), IDX(pCore(2),pCore(1)));
    
    subplot(3,3,f_idx);
    imshow(sklt);

    axis on
    grid on

    hold on;
    plot(i,j,'r.','MarkerSize',20)

    [mid_point, x1, y1, x2, y2] = find_triangle(x, y, sklt, 15);

    %hold on;
    %plot(contour1(:,2), contour1(:,1), 'g', 'LineWidth', 1)
    hold on;
    plot(x1,y1,'g.','MarkerSize',20)
    plot(x2,y2,'b.','MarkerSize',20)
    plot(mid_point(1),mid_point(2),'y.','MarkerSize',20)

    line1 = [[x2,y2];[x1,y1];];
    line2 = [[mid_point(1),mid_point(2)]; [i,j]];

    l = polyfit([j,mid_point(2)],[i,mid_point(1)],1);
    f = @(x) l(1)*x+l(2);

    % Ugly way of creating new vector
    line3 = [];
    for ld = 1:size(sklt,1)
       line3 = [line3; [f(ld), ld]]; 
    end

    %% All dem triangles
    [x, y] = arrayfun(@(x) ind2sub(size(sklt), x), find(sklt > 0));
    points = [x, y];
    
    intersects = [];
    for idx = 1:size(points, 1)
        point = points(idx,:);
        x = point(1,1);
        y = point(1,2);
        if (y < j) % we only care about intersects above the pCore
            for idx_line = 1:size(line3, 1)
                l3 = line3(idx_line,:);
                l_x = int16(l3(1,1));
                l_y = int16(l3(1,2));
                if (l_x == x && l_y == y)
                   intersects = [intersects; [x, y]];
                end
            end
        end
    end
    
    size(intersects)
    
    for idx = 1:size(intersects, 1)
        point = intersects(idx,:);
        x = point(1,1);
        y = point(1,2);
        [mid_point, x1, y1, x2, y2] = find_triangle(x, y, sklt, 15);
        
        % plot intersect triangle
        plot(x, y, 'r.', 'MarkerSize', 20)
        plot(y1, x1, 'g.', 'MarkerSize', 20)
        plot(y2, x2, 'b.', 'MarkerSize', 20)
        plot(mid_point(2), mid_point(1), 'y.', 'MarkerSize', 20)
    end
    
    %% Dat angle tho'
    
    line4 = []
    for ld =1:size(sklt,1)
        line4 = [line4; [ld, 200]];
    end
    
    plot(line3(:,1),line3(:,2),'Color','r','LineWidth',1)
    plot(line4(:,1),line4(:,2),'Color','w','LineWidth',1)
    plot([line1(1,1),line1(2,1)],[line1(1,2),line1(2,2)],'Color','r','LineWidth',2)
    plot([line2(1,1),line2(2,1)],[line2(1,2),line2(2,2)],'Color','r','LineWidth',2)
    
    grid on;
    drawnow;
    
    x1 = line3(:,1);
    y1 = line3(:,2);
    x2 = line4(:,1);
    y2 = line4(:,2);
    
    DirVector1 = [[x1(1),y1(1)]-[x1(end),y1(end)]];
    DirVector2 = [[x2(1),y2(1)]-[x2(end),y2(end)]];
    v1 = DirVector1;
    v2 = DirVector2;
    
    ang = atan2(v1(1)*v2(2)-v2(1)*v1(2),v1(1)*v2(1)+v1(2)*v2(2))
    Angle = 180-mod(-180/pi * ang, 360)
end

%%