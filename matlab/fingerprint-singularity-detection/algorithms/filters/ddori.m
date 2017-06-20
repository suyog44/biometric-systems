function [ DCor ] = ddori( DCor,GAvgAvg, DDOriDiff, BGMask )
% orientation difference of two deltas
% 
% Input: 
% BGMask - bacdground
% GAvgAvg - squared orientation field
% DCor - deltas coordinates
% DDOriDiff - predefined delta and delta distance
% 
% Output: 
% DCor - validated delta coordinates
% 
% 2013 Jinghua Wang, jinghua.wang@cased.de
    [xBG, yBG] = find(BGMask == 0);

    DDis = zeros(size(DCor,1),1);
    for j=1:1:size(DCor,1)
        DDis(j) = min(pdist2(DCor(j,:), [yBG, xBG],'euclidean'));
    end

    [X,Y] = meshgrid(-3:1:3,-3:1:3);

    Ref = zeros(7,7);
    for j=1:1:7
        for k=1:1:7
            Ref(j,k) = -Y(j,k)/sqrt(Y(j,k)^2+X(j,k)^2) - 1i.*X(j,k)/sqrt(Y(j,k)^2+X(j,k)^2) ;
        end
    end

    An = GAvgAvg(DCor(1,2)-3:DCor(1,2)+3,DCor(1,1)-3:DCor(1,1)+3).*conj(Ref);
    An(4,4) = 0;
    a = sum(sum(An));
    A1 = angle(a)/3;

    An = GAvgAvg(DCor(2,2)-3:DCor(2,2)+3,DCor(2,1)-3:DCor(2,1)+3).*conj(Ref);
    An(4,4) = 0;
    a = sum(sum(An));
    A2 = angle(a)/3;

    if abs(A1-A2) > DDOriDiff
        DCor(DDis==min(DDis),:) = 0;
        DDis(DDis==min(DDis),:) = 0;
        DCor(all(DCor==0,2),:) = [];
        DDis(all(DDis==0,2),:) = [];     
    end 

end

