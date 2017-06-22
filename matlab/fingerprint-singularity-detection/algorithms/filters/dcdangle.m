function [ DCor ] = dcdangle( CCor, DCor, Min, Max, BGMask)
% angle core1-delta-core2 should be in a certain interval
% 
% Input: 
% BGMask - bacdground
% CCor - core coordinates
% deltas - deltas coordinates
% CCOriDiff - predefined core and core distance
% Min, Max - predefined interval
% 
% Output: 
% DCor - validated deltas coordinates
% 
% 2013 Jinghua Wang, jinghua.wang@cased.de

    BGMask(1,:)=0;
    BGMask(end,:)=0;
    BGMask(:,1)=0;
    BGMask(:,end)=0;
    [xBG, yBG] = find(BGMask == 0);

    if (size(CCor) == 2)
        CCor = (CCor(1,:) + CCor(2,:))./2;
    end
    
    DDis = zeros(size(DCor,1),1);
    for j=1:1:size(DCor,1)
        DDis(j) = min(pdist2(DCor(j,:), [yBG, xBG],'euclidean'));
    end
    
    Slope1 = angle(1i*(CCor(1,1) - DCor(1,1)) + (CCor(1,2) - DCor(1,2)));
    Slope2 = angle(1i*(CCor(1,1) - DCor(2,1)) + (CCor(1,2) - DCor(2,2)));
    SlopeDiff = min(abs(Slope1 - Slope2),2*pi-abs(Slope1-Slope2));
    
    if( SlopeDiff < Min || SlopeDiff > Max)
        DCor(DDis==min(DDis),:) = 0;
        DCor(all(DCor==0,2),:) = [];
    end

end

