function [ CCor ] = ccdis( CCor, CCDis, BGMask )
% max distance of core and core distance
% 
% Input: 
% BGMask - bacdground
% CCor - core coordinates
% CCDis - predefined core and core distance
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

if pdist2(CCor(1,:), CCor(2,:),'euclidean') > CCDis
    CCor(CDis==min(CDis),:) = 0;
    CCor(all(CCor==0,2),:) = [];
end

end

