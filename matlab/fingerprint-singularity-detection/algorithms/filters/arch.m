function [ Core ] = arch( gThetaSqu, BGMask, EdgeDis )
% detetct arch
% 
% Input: 
% Res - gradient of squared orientation fields
% BGMask - bacdground
% EdgeDis - distance to reject detected cores
% 
% Output: 
% Core - core coordinateds
% Res - gradient of squared orientation fields
% 
% 2013 Jinghua Wang, jinghua.wang@cased.de
    BGMask(1,:)=0;
    BGMask(end,:)=0;
    BGMask(:,1)=0;
    BGMask(:,end)=0;
    
    [xCore,yCore] = find(gThetaSqu>0.00001);
    for j=1:1:size(xCore,1)
        k = 1;
        while(j+k <= size(xCore,1) && xCore(j) ~= 0 && yCore(j) ~= 0)
            if(abs(xCore(j+k)-xCore(j))<=15 && abs(yCore(j+k)-yCore(j))<=15 && xCore(j+k) ~= 0 && yCore(j+k) ~= 0)
                if (gThetaSqu(xCore(j+k),yCore(j+k))> gThetaSqu(xCore(j),yCore(j)))
                    xCore(j)=0;yCore(j)=0;
                    break;
                else
                    xCore(j+k)=0;yCore(j+k)=0;
                end
            end
            k = k+1;
        end
    end
    xCore(all(xCore==0,2),:) = [];
    yCore(all(yCore==0,2),:) = [];
    
    [xBG, yBG] = find(BGMask == 0);
    [xT, yT] = find(BGMask~=0);
    xCenter = mean(xT);
    yCenter = mean(yT);
 
    CDis = zeros(size(xCore,1),1);
    for j=1:1:size(xCore,1)
        CDis(j) = min(pdist2([yCore(j),xCore(j)], [yBG, xBG],'euclidean'));
    end
    
    [CDis,in] = sort(CDis);
    for j=1:1:size(xCore,1)
        if (size(xCore(xCore~=0),1))>2
            if CDis(j)< EdgeDis
                CDis(j) = 0;
                xCore(in(j)) = 0;
                yCore(in(j)) = 0;
            end
        else
            break;
        end
    end
    xCore(all(xCore==0,2),:) = [];
    yCore(all(yCore==0,2),:) = [];
    
    
    CDis = zeros(size(xCore,1),1);
    for j=1:1:size(xCore,1)
        CDis(j) = pdist2([yCore(j),xCore(j)], [yCenter, xCenter],'euclidean');
    end
    if size(xCore,1)>1
        PI = zeros(size(xCore));
        for j=1:1:size(xCore,1)
            PI(j) = gThetaSqu(xCore(j),yCore(j)); %* (1 - cdf('unif',CDis(j),0,mdis));
        end
        xCore(PI<max(PI))=0;
        yCore(PI<max(PI))=0;
        xCore(all(xCore==0,2),:) = [];
        yCore(all(yCore==0,2),:) = [];
    end  
    %}
    Core = [yCore, xCore];
end

