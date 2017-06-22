function [Core, Delta] = poincare_index(Theta, BGMask, Blocsize)
% compute mean and std of areas around SPs
% 
% Input: 
% Theta - orientation fields
% Blocksize - size of curves
% 
% Output: 
% Core - detected core coordinates
% Delta - detected deltas coordinates
% 
% 2013 Jinghua Wang, jinghua.wang@cased.de

Offset = zeros(Blocsize, Blocsize);
Offset(1,:) = 1:1:Blocsize;
Offset(:,end) = Blocsize:1:Blocsize*2-1;
Offset(end,:) = Blocsize*3-2:-1:Blocsize*2-1;
Offset(2:end,1) = Blocsize*4-4:-1:Blocsize*3-2;

WL = floor(Blocsize/2);
WR = Blocsize - WL - 1;

[Height, Width] = size(Theta);

Core = [];
Delta = [];

for j=WL+1:1:Height-WR
    for k=WL+1:1:Width-WR
        ThetaMat =  Theta(j-WL:j+WR,k-WL:k+WR);
        Sum = 0;
        for l=1:1:Blocsize^2-(Blocsize-2)^2
            if l ~= Blocsize^2-(Blocsize-2)^2;
                if abs(ThetaMat(Offset==l+1) - ThetaMat(Offset==l)) <= pi/2;
                   Sum = Sum + ThetaMat(Offset==l+1) - ThetaMat(Offset==l);
                elseif ThetaMat(Offset==l+1) - ThetaMat(Offset==l) <= -pi/2;
                   Sum = Sum + ThetaMat(Offset==l+1) - ThetaMat(Offset==l) + pi; % Opposite direction
                else
                   Sum = Sum + (ThetaMat(Offset==l+1) - ThetaMat(Offset==l)) - pi;%Diff with paper.
                end
            else
                if abs(ThetaMat(Offset==1) - ThetaMat(Offset==l)) <= pi/2;
                   Sum = Sum + ThetaMat(Offset==1) - ThetaMat(Offset==l);
                elseif ThetaMat(Offset==1) - ThetaMat(Offset==l) <= -pi/2;
                   Sum = Sum + ThetaMat(Offset==1) - ThetaMat(Offset==l) + pi;
                else
                   Sum = Sum + (ThetaMat(Offset==1) - ThetaMat(Offset==l)) - pi;
                end
            end
        end
        if(Sum> 3.1 && Sum<3.2 && BGMask(j,k)==1) % If close to pi
            Core = [Core; [k, j]];
        elseif(Sum< -3.1 && Sum > -3.2 && BGMask(j,k)==1)
            Delta = [Delta; [k, j]];
        end
    end
end