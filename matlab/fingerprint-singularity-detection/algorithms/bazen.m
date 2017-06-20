function [coreCor, deltaCor, gThetaSqu] = bazen(Theta, BGMask, SN, CTH, DTH)

% compute pi values
% 
% Input: 
% BImage - blockwise image
% BGMask - background
% ThetaSqu - squared orientation field
% SN - size of summing window
% CTH - threshold of cores
% DTH - threshold of deltas
% 
% Output: 
% Res - gradients of squared orientat fileds
% PI - summing results
% CCor - detected cores
% DCor - detected deltas
% 
% 2013 Jinghua Wang, jinghua.wang@cased.de
[BHeight, BWidth] = size(Theta);
ThetaSqu = Theta .* 2;

%Bazen's method
SinThetaSqu = sin(ThetaSqu);
CosThetaSqu = cos(ThetaSqu);

[SinThetadx, SinThetady] = gradient(SinThetaSqu);
[CosThetadx, CosThetady] = gradient(CosThetaSqu);

Jx = CosThetaSqu .* SinThetadx - SinThetaSqu.* CosThetadx;
Jy = CosThetaSqu .* SinThetady - SinThetaSqu.* CosThetady;

[~, Jxdy] = gradient(Jx);
[Jydx, ~] = gradient(Jy);

gThetaSqu = Jydx - Jxdy;
gThetaSqu(BGMask ~=1) = 0;

%sum
PI = zeros(BHeight, BWidth);
for j=SN+1:1:BHeight-SN
    for k=SN+1:1:BWidth-SN
        PI(j,k)= sum(sum(gThetaSqu(j-SN:j+SN,k-SN:k+SN)));
    end
end

PI(PI>2*pi) = 0;
PI(PI<-2*pi) = 0;
PI(BGMask==0) = 0;

coreCor = [];
CC = bwconncomp(PI > CTH);
for k = 1:CC.NumObjects
    [yCore, xCore] = find(PI == max(PI(CC.PixelIdxList{k})));
    coreCor = [coreCor; [xCore, yCore]];
end

deltaCor = [];
CC = bwconncomp(PI < (-1)*DTH);
for k = 1:CC.NumObjects
    [yDelta, xDelta] = find(PI == max(PI(CC.PixelIdxList{k})));
    deltaCor = [deltaCor; [xDelta, yDelta]];
end

end

