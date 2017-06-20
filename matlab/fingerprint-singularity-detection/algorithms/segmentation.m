function [ bgmask ] = segmentation( image, blksize, segThreshold )
%SEGMENTATION Summary of this function goes here
% Gaussian filter
Gau = fspecial('gaussian', [2, 2], 5);
image = imfilter(image,Gau,'replicate');

% Segmentation
[~, bgmask, ~] = ridgesegment(image, blksize, segThreshold);
CC = bwconncomp(bgmask);
numOfPixels = cellfun(@numel,CC.PixelIdxList);
[~,idx] = max(numOfPixels);
bgmask(CC.PixelIdxList{idx}) = 1;
bgmask = imfill(bgmask,'holes');
se = strel('disk',64);
% bgmask = imerode(BGMask,se);
bgmask = imopen(bgmask,se);

end

