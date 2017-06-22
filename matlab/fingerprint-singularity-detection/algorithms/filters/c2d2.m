function [ CCor, DCor ] = c2d2( CCor, DCor, BGMask )
% at most 2 core and 2 delats
% 
% Input: 
% BGMask - bacdground
% CCor - core coordinates
% DCor - delta coordinates
% 
% Output: 
% CCor - validated core coordinates
% DCor - validated delta coordinates
% 
% 2013 Jinghua Wang, jinghua.wang@cased.de
BGMask(1,:)=0;
BGMask(end,:)=0;
BGMask(:,1)=0;
BGMask(:,end)=0;
[xBG, yBG] = find(BGMask == 0);

DDis = zeros(size(DCor,1),1);
for j=1:1:size(DCor,1)
    DDis(j) = min(pdist2(DCor(j,:), [yBG, xBG],'euclidean'));
end

CDis = zeros(size(CCor,1),1);
for j=1:1:size(CCor,1)
    CDis(j) = min(pdist2(CCor(j,:), [yBG, xBG],'euclidean'));
end



while(size(CCor,1) > 2)
    CCor(CDis==min(CDis),:) = 0;
    CDis(CDis==min(CDis),:) = 0;
    CCor(all(CCor==0,2),:) = [];
    CDis(all(CDis==0,2),:) = [];
end

while(size(DCor,1) > 2)
    DCor(DDis==min(DDis),:) = 0;
    DDis(DDis==min(DDis),:) = 0;
    DCor(all(DCor==0,2),:) = [];
    DDis(all(DDis==0,2),:) = [];
end

end

