function [ mid_point, x1, y1, x2, y2 ] = find_triangle( x, y, sklt, dist )
%FIND_TRIANGLE Summary of this function goes here
%   Detailed explanation goes here

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

    plot(contour7(:,1), contour7(:,2), 'Color', 'g', 'LineWidth', 1)
    plot(contour3(:,1), contour3(:,2), 'Color', 'b', 'LineWidth', 1)
    
    x1=0;
    y1=0;
    x2=0;
    y2=0;
    if(size(unique_contours,1)>1)
        y1 = unique_contours(1,1);
        x1 = unique_contours(1,2);
        y2 = unique_contours(2,1);
        x2 = unique_contours(2,2);
    end
    
    mid_point = [(x1+x2)/2, (y1+y2)/2];
end

