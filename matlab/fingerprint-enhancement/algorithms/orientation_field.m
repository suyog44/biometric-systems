function [ Theta, GSquSqu ] = orientation_field( bImage, WM, WN)
%ORIENTATIONFIELD Summary of this function goes here
%   Detailed explanation goes here
[bHeight, bWidth] = size(bImage);
[Gx, Gy] = gradient(bImage);

G = Gx + 1i*Gy;
GSqu = G.^2;

WMLeft = floor(WM/2);
WMRight = WM - WMLeft - 1;

WNLeft = floor(WN/2);
WNRight = WN - WNLeft - 1;

GSquSqu = zeros(bHeight, bWidth);

tic
%Up
for j=1:1:WNLeft  
    for k=1:1:WMLeft
        GSquSqu(j,k) = sum(sum(GSqu(1:j+WNRight,1:k+WMRight)));
    end
    
    for k=WMLeft+1:1:bWidth-WMRight
        GSquSqu(j,k) = sum(sum(GSqu(1:j+WNRight,k-WMLeft:k+WMRight)));
    end
    for k=bWidth-WMRight+1:1:bWidth
        GSquSqu(j,k) = sum(sum(GSqu(1:j+WNRight,k-WMLeft:end)));
    end
end
%Central
for j=WNLeft+1:1:bHeight-WNRight
    for k=1:1:WMLeft
        GSquSqu(j,k) = sum(sum(GSqu(j-WNLeft:j+WNRight,1:k+WMRight)));
    end
    for k=WMLeft+1:1:bWidth-WMRight
        GSquSqu(j,k) = sum(sum(GSqu(j-WNLeft:j+WNRight,k-WMLeft:k+WMRight)));
    end
    for k=bWidth-WMRight+1:1:bWidth
        GSquSqu(j,k) = sum(sum(GSqu(j-WNLeft:j+WNRight,k-WMLeft:end)));
    end
end

%Down
for j=bHeight-WNRight+1:1:bHeight
    for k=1:1:WMLeft
        GSquSqu(j,k) = sum(sum(GSqu(j-WNRight:end,1:k+WMRight)));
    end
    for k=WMLeft+1:1:bWidth-WMRight
        GSquSqu(j,k) = sum(sum(GSqu(j-WNRight:end,k-WMLeft:k+WMRight)));
    end
    for k=bWidth-WMRight+1:1:bWidth
        GSquSqu(j,k) = sum(sum(GSqu(j-WNRight:end,k-WMLeft:end)));
    end
end
toc

%}
%average gradient direction, Phi
Phi = atan2(imag(GSquSqu),real(GSquSqu)) /2; %GsyAvg > 0

%average ridge-vallley direction, Theta
Theta = zeros(bHeight, bWidth);
Theta(Phi<=0) = Phi(Phi<=0) + pi/2;
Theta(Phi>0) = Phi(Phi>0) - pi/2;
end

