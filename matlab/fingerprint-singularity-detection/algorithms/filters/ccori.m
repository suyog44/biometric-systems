function [ CCor ] = ccori( CCor, gSquSqu, CCOriDiff, BGMask )
% orientation difference of two cores
% 
% Input: 
% BGMask - bacdground
% gSquSqu - squared orientation field
% CCor - core coordinates
% CCOriDiff - predefined core and core distance
% 
% Output: 
% CCor - validated core coordinates
% 
% 2013 Jinghua Wang, jinghua.wang@cased.de

    BGMask(1,:)=0;
    BGMask(end,:)=0;
    BGMask(:,1)=0;
    BGMask(:,end)=0;
    [xBG, yBG] = find(BGMask == 0);

    CDis = zeros(size(CCor,1),1);
    for j=1:1:size(CCor,1)
        CDis(j) = min(pdist2(CCor(j,:), [yBG, xBG],'euclidean'));
    end

    [X,Y] = meshgrid(-3:1:3,-3:1:3);

    Ref = zeros(7,7);
    for j=1:1:7
        for k=1:1:7
            Ref(j,k) = Y(j,k)/sqrt(Y(j,k)^2+X(j,k)^2) - 1i.*X(j,k)/sqrt(Y(j,k)^2+X(j,k)^2) ;
        end
    end

        An = gSquSqu(CCor(1,2)-3:CCor(1,2)+3,CCor(1,1)-3:CCor(1,1)+3).*conj(Ref);
        An(4,4) = 0;
        a = sum(sum(An));
        A1 = angle(a);

        An = gSquSqu(CCor(2,2)-3:CCor(2,2)+3,CCor(2,1)-3:CCor(2,1)+3).*conj(Ref);
        An(4,4) = 0;
        a = sum(sum(An));
        A2 = angle(a);

        if min(2*pi-abs(A1-A2), abs(A1-A2)) < CCOriDiff
            CCor(CDis==min(CDis),:) = 0;
            CDis(CDis==min(CDis),:) = 0;
            CCor(all(CCor==0,2),:) = [];
            CDis(all(CDis==0,2),:) = [];
        end

end

